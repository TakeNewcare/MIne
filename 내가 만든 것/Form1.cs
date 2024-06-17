using Mine.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Mine
{
    public partial class Form1 : Form
    {
        // 최대 게임 크기 설정
        private const int max_row = 20;
        private const int max_col = 20;

        Cell[,] cells;
        display pan;  // 디스플레이 패널


        public static int current_row; // 현재 게임 크기
        public static int current_col;
        public static int current_mine;  // 현재 마인 갯수

        private int open_cell = 0;   // 클릭된(오픈된) 셀 갯수 체크
        private int total_cell;

        private int flag_count; // 사용자가 사용한 마인 갯수

        public static int menu_height;
        public static int panel_height;

        private int sec;  // 게임 시작 시 1초마다 시간 세기

        private bool open_all = false;  // 게임을 끝난 상태에서 클릭 이벤트 막기


        private bool game_start_true = true; // 셀 클릭 시 타이머 시작하고 false로 돌린 후 두번째 클릭 부터 start 함수 막기


        public Form1()
        {

            InitializeComponent();

            // 처음 시작 시 레벨 선택 창
            Form2 sub_form = new Form2(400);
            sub_form.ShowDialog();



            menu_height = menuStrip1.Height;
            panel_height = display.HEIGHT;

            cells = new Cell[max_row, max_col]; // 최대 크기로 미리 선언

            for (int i = 0; i < max_row; i++)    // 최대 행, 열의 셀 생성한다.(20x20)
            {
                for (int j = 0; j < max_col; j++)
                {
                    cells[i, j] = new Cell(i, j);
                    Controls.Add(cells[i, j]);
                    cells[i, j].Click += new System.EventHandler(this.Cell_Click);
                    cells[i, j].MouseDown += new System.Windows.Forms.MouseEventHandler(this.Flag_btn);
                    cells[i, j].MouseUp += new System.Windows.Forms.MouseEventHandler(this.UnSign_Cell);


                }
            }

            pan = new display();  // 전광판 생성
            Controls.Add(pan);

            new_game(current_row, current_col, current_mine);
        }

        Random rnd = new Random();

        public void new_game(int r, int c, int mine)
        {
            // 새 게임 시 리셋할 변수들
            pan.L2.Text = "0";
            current_row = r;
            current_col = c;
            current_mine = flag_count = mine;
            game_start_true = true;
            open_all = false;
            open_cell = 0;
            total_cell = r * c;
            sec = 0;

            // form 사이즈 조절
            Size = new System.Drawing.Size(Cell.cell_size * current_row + 20, Cell.cell_size * current_col + menuStrip1.Height + 50 + display.HEIGHT);

            // 디스플레이 사이즈 조절
            pan.Size = new System.Drawing.Size(Cell.cell_size * current_row, 54);

            // 디스플레이 버튼2( 시간 표시판 ) 위치 조절
            pan.L2.Location = new System.Drawing.Point(Cell.cell_size * current_row - 80, 0);

            // 스마일 버튼 위치 조절
            pan.L3.Location = new System.Drawing.Point((Cell.cell_size * current_row) / 2 - 20, 10);


            // 마인 갯수 - flag 갯수를 패널에 보여주기
            pan.L1.Text = flag_count.ToString();
            pan.L3.Image = global::Mine.Properties.Resources.sta;   // 이미지 삽입.
            pan.L3.Click += new System.EventHandler(this.Re_Start);  // 전광판 중앙 버튼 클릭시 똑같은 레벨 새로 시작



            // 새 게임 시작 시 데이터 리셋(셀을 게임 시작 마다 새로 까는게 아니라서 매번 해줘야한다.)
            for (int i = 0; i < max_row; i++)
            {
                for (int j = 0; j < max_col; j++)
                {
                    cells[i, j].Text = cells[i, j].hidden_text = "";   // 텍스트와 히든텍스트 리셋

                    cells[i, j].Visible = i >= current_row || j >= current_col ? false : true;  // 최대 크기 셀에서 필요없는 부분은 가리기(레벨에 따라 안보여주는 셀 설정)

                    cells[i, j].BackColor = System.Drawing.SystemColors.ControlLight;   // 이전 게임에서 클릭으로 인해 변경된 배경색 돌리기

                }
            }



            // 난수를 이용한 폭탄 심기
            for (int i = 0; i < mine; i++)
            {
                int rnd_x = rnd.Next(r);
                int rnd_y = rnd.Next(c);

                if (cells[rnd_x, rnd_y].hidden_text == "*")  // 폭탄이 이미 있을 경우 한번 더
                {
                    i--;
                    continue;
                }

                cells[rnd_x, rnd_y].hidden_text = "*";

            }

            // 각 셀에서 주위의 폭탄 갯수 파악 후 갯수 입력
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    if (cells[i, j].hidden_text == "*")  // 폭탄이 있는 위치면 넘기기
                        continue;

                    // 주위 폭탄 세기
                    int mint_count = CountMine(i, j);

                    if (mint_count >= 1)
                        cells[i, j].hidden_text = mint_count.ToString();
                    else
                        cells[i, j].hidden_text = ".";

                }
            }


        }


        // 좌표값을 받아 주위 8칸의 폭탄을 찾는다.
        public int CountMine(int x, int y)
        {
            int count = 0;

            for (int i = Math.Max(x - 1, 0); i <= Math.Min(x + 1, current_col - 1); i++)  // 만약 x좌표가 0일 경우 -1 좌표가 없기 때문에 i의 값을 -1이 아닌 최댓값 0으로 바꾼다
            {
                for (int j = Math.Max(y - 1, 0); j <= Math.Min(y + 1, current_row - 1); j++)
                {
                    if (cells[i, j].hidden_text == "*")
                        count++;
                }
            }

            return count;
        }


        // 클릭 시
        private void Cell_Click(object sender, EventArgs e)
        {

            Cell c = (Cell)sender;

            if (open_all)  // 폭탄이 터져 게임이 진 상태에서 클릭 동작 제한
                return;
            else if (game_start_true)
            {
                game_start_true = false;
                timer1.Start();
            }
            else if (c.Text == "P" || c.Text != "") // 깃발 꼽은 셀 클릭 시 동작 x, 경계선 클릭 시 동작 x
                return;

            OpenCell(c);

            if (c.hidden_text == "*")
            {
                timer1.Stop();
                pan.L3.Image = global::Mine.Properties.Resources.lose; // 클릭 시 폭탄이면 표정 바꾸고 모든 셀 오픈
                OpenAll();
                MessageBox.Show("펑!!", "알림", MessageBoxButtons.OK);
            }
            else if (c.Text == ".")
                // 안전지대 주변 오픈
                OpenSurround(c.x, c.y);

        }


        private void OpenCell(Cell c, bool lose = true)
        {
            open_cell++;
            c.Text = c.hidden_text;

            detail_btn(c);     // 배경 색상 변경

            // 셀을 오픈하면서 텍스트 색 넣기
            if (c.hidden_text == ".")
            {
                c.ForeColor = System.Drawing.Color.Black;
                return;
            }

            if (c.hidden_text == "*")
            {
                c.ForeColor = System.Drawing.Color.Red;
                return;
            }

            InitColor(c, c.hidden_text);  // 숫자일 경우 텍스트 색상 변경

            // 전체 셀 중에서 마인 셀 갯수 제외한 칸을 모두 오픈하면 승리 => 오픈된 셀 기억하기
            // 만약, 폭탄이 터져 opencell이 실행될경우 false를 전달받아 if문 패스
            if (total_cell == open_cell + current_mine && lose)
                Game_Win();
        }

        // 경계라인 클릭 시 주변 범위 보여주기 => mousedown과 up이랑 연결된다(click 이벤트 x)
        private void Surround_Sign(int x, int y)
        {
            for (int i = Math.Max(x - 1, 0); i <= Math.Min(x + 1, current_row); i++)
            {
                for (int j = Math.Max(y - 1, 0); j <= Math.Min(y + 1, current_col); j++)
                {
                    if (cells[i, j].Text != "")
                        continue;

                    cells[i, j].BackColor = Color.FromArgb(0, 153, 153);

                }
            }
        }


        private void InitColor(Cell c, string s)
        {
            int n = int.Parse(s);

            switch (n)
            {
                case 1:
                    c.ForeColor = System.Drawing.Color.Blue;
                    break;
                case 2:
                    c.ForeColor = System.Drawing.Color.Green;
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    c.ForeColor = System.Drawing.Color.Red;
                    break;
            }

        }


        public void OpenAll()
        {
            open_all = true; // 모든 셀 오픈 후 셀 클릭 막는 플레그

            for (int i = 0; i < current_row; i++)
            {
                for (int j = 0; j < current_col; j++)
                {
                    if (cells[i, j].Text != "")
                        continue;

                    OpenCell(cells[i, j], false);  // false를 전달하여 game_win 실행 막기

                }
            }
        }

        // 클릭한 셀이 안전지대(.)이면 주면 열고 열린 지역도 안전지대이면 재귀함수로 한번 더 오픈
        public void OpenSurround(int x, int y)
        {
            // 클릭한 셀이 최소 / 최대 셀일 시 주변 8칸을 찾을 때 셀의 최소 인덱스, 최대 인덱스를 넘지 않게 찾기 위한 max, min 함수
            for (int i = Math.Max(x - 1, 0); i <= Math.Min(x + 1, current_row - 1); i++)
            {
                for (int j = Math.Max(y - 1, 0); j <= Math.Min(y + 1, current_col - 1); j++)
                {
                    if (cells[i, j].Text != "")   // 이미 오픈된 박스이면 넘기기
                        continue;

                    OpenCell(cells[i, j]);


                    if (cells[i, j].Text == ".")   // 만약 새로 오픈한 곳이 안전지대이면 해당 위치 기준으로 재귀함수 사용
                        OpenSurround(i, j);
                }
            }
        }


        public void detail_btn(Button c)
        {
            c.BackColor = Color.LightGray;
        }


        // 우클릭 시 깃발, 경계라인 클릭 시 범위를 나타내는 동작
        private void Flag_btn(object sender, MouseEventArgs e)
        {

            Cell c = (Cell)sender;  // 이벤트 발생한 객체 찾기


            // 경계라인 클릭 시 범위 알려주는 기능 => 클릭 이벤트에 걸면 작동 x
            if (c.Text != "" && c.Text != "." && c.Text != "P")
                Surround_Sign(c.x, c.y);
            else
            {
                if (e.Button != MouseButtons.Right)
                    return;

                if (game_start_true)   // 첫 클릭이 우클릭이라도 시작으로 간주하고 타이머 시작
                {
                    game_start_true = false; // 첫 클릭인 true 값을 false로 돌려 다음 클릭은 타이머 다시 시작하는거 막기
                    timer1.Start();
                }

                if (c.Text == "P")
                {
                    c.Text = "";
                    pan.L1.Text = (flag_count + 1).ToString();
                    flag_count += 1;
                }
                else if (c.Text == "")
                {
                    c.Text = "P";
                    pan.L1.Text = (flag_count - 1).ToString();
                    flag_count -= 1;
                    c.ForeColor = System.Drawing.Color.Red;
                }
            }

        }

        private void Game_End(object sender, EventArgs e)
        {
            if (MessageBox.Show("게임을 종료 하시겠습니까?", "질문", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            Application.Exit();
        }


        private void new_10x10(object sender, EventArgs e)
        {
            new_game(10, 10, 15);

        }

        private void new_15x15(object sender, EventArgs e)
        {

            new_game(15, 15, 45);

        }


        private void new_20x20(object sender, EventArgs e)
        {
            new_game(20, 20, 60);

        }




        private void Game_Start(object sender, EventArgs e)
        {
            sec++;   // 1초마다 1씩 올린다 (interval = 1000)
            pan.L2.Text = sec.ToString();  // 전광판에 흐른 시간 보여주기
        }


        // 스마일 버튼 클릭 시 현재 게임 재시작
        private void Re_Start(object sender, EventArgs e)
        {
            new_game(current_row, current_col, current_mine);
        }

        public void Game_Win()
        {
            timer1.Stop();

            pan.L3.Image = global::Mine.Properties.Resources.win;

            OpenAll();

            MessageBox.Show("승리!!", "알림", MessageBoxButtons.OK);

        }

        // mouseup 이벤트 일때 mousedown으로 나타났던 범위 사라지게 하기
        private void UnSign_Cell(object sender, MouseEventArgs e)
        {
            Cell c = (Cell)sender;

            int x = c.x;
            int y = c.y;

            for (int i = Math.Max(x - 1, 0); i <= Math.Min(x + 1, current_row); i++)
            {
                for (int j = Math.Max(y - 1, 0); j <= Math.Min(y + 1, current_col); j++)
                {
                    if (cells[i, j].Text != "")
                        continue;

                    cells[i, j].BackColor = System.Drawing.SystemColors.ControlLight;

                }
            }
        }

        private void Game_Info(object sender, EventArgs e)
        {
            MessageBox.Show("만든이 : 정진영", "정보");
        }

        // 레벨에 따라 스마일 위치 변경(해결)
        // 승리시 이모티콘 추가(해결)
        // 셀 모두 클릭 전에 폭탄 위치에 미리 깃발 세워도 성공 x => 폭탄 위치 기억 x
        // 깃발 안꼽은 상태에서 폭탄 제외한 모든 셀 클릭시 성공 o => 만약, 폭탄 갯수를 제외한 모든 셀의 갯수가 클릭된 상태이면 win 기능(해결)
        // 클릭된 셀의 경계선 클릭 시 주변 8칸 잠시 클릭된 모양으로 변해서 주변 알려주기(해결)

    }
}
