using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mine
{

    public partial class Board : Form
    {
        // 이 후 추가된 거
        // 새게임, 사이즈별 새 게임 작성
        // 우클릭시 플레그 세우기

        // 1. 게임판(각 셀들) - 기본으로 20x20으로 만들고 안보이게 만들기
        private int cols , rows ;
        private int MAX_COLS = 20, MAX_ROWS = 20;
        Cell[,] map ;

        // 이 후
        // 메뉴판 높이
        public static int MenuHeight;

        public Board()
        {
            // Windows Forms 애플리케이션에서 디자이너가 생성한 컨트롤들을 초기화하고 배치하는 메서드로
            // 폼 클래스의 생성자에서 호출된다.
            InitializeComponent();

            // 메뉴판 높이
            MenuHeight = menuStrip1.Height;


            map = new Cell[MAX_COLS, MAX_ROWS];

            for (int i = 0; i < MAX_COLS; i++)
            {
                for (int j = 0; j < MAX_ROWS; j++)
                {

                    // Controls : 폼 또는 패널 등의 컨트롤 컬렉션을 나타내며, 생성자로 전달되어 컨트롤 컬렉션에 새로 생성된 셀을 추가하는 기능을 한다.
                    // ※ Controls 속성은 폼이나 패널과 같은 컨테이너 컨트롤에만 존재하며, 컨트롤 컬렉션은 해당 컨테이너에 존해하는 모든 컨트롤을 저장하고 관리한다.
                    // => 즉, 폼에 버튼, 레이블 등을 추가하려면 폼의 컨트롤 속성에 추가해야 화면에 보여지게 된다!!
                    map[i, j] = new Cell(Controls, i, j);

                    // 4. 클릭 이벤트 넣어주기
                    // 각 셀(버튼)의 Click 속성에 cell_Click라는 함수를 추가해준다.
                    // System.EventHandle : .net framework에서 제공되는 델리게이트
                    // => 즉, 클릭이라는 속성 이벤트가 발생했을 때 해당 함수를 실행해 준다.
                    map[i, j].Click += new System.EventHandler(this.cell_Click);

                    // 이 후
                    // 우클릭 시 이벤트
                    map[i, j].MouseUp += new System.Windows.Forms.MouseEventHandler(this.cell_MouseUp);


                }
            }
            NewGame();
        }

        // 5. 주위의 폭탄 수 세기
        private int CountMines(int c, int r)
        {
            int count = 0;
            for(int i=Math.Max(c -1,0); i<= Math.Min(c +1, cols - 1);i++)
            {
                for (int j = Math.Max(r - 1,0); j <= Math.Min(r + 1,rows-1); j++)
                {
                    if (map[i, j].HasMine())
                        count++; 
                }
            }
            return count;
        }

        // 7. 셀 오픈
        private void OpenMines(int c, int r)
        {
            
            for (int i = Math.Max(c - 1, 0); i <= Math.Min(c + 1, cols - 1); i++)
            {
                for (int j = Math.Max(r - 1, 0); j <= Math.Min(r + 1, rows-1); j++)
                {
                    if (map[i, j].Text != "")
                        continue;

                    map[i, j].Open(); 

                    if ( map[i, j].hidden_text ==".")
                    {
                        OpenMines(i, j);
                    }
                }
            }
            
        }

        // 6. 게임 오버
        private void GameOver()
        {
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    //if (map[i, j].HasMine())
                    map[i, j].Open();
                }
            }

        }

        // 2. 지뢰뿌리기
        private void NewGame(int cs =10, int rs =10,int mines = 15)
        {
            cols = cs;
            rows = rs;

            // 폼의 크기를 설정해주는 클래스
            Size=new Size(cols * Cell.W +50 , rows * Cell.W + menuStrip1.Size.Height + 50 );

            // 2-1. 일단, 각 셀의 히든 텍스트에 빈문자열을 넣어준다.(굳이 필요한가???)
            for (int i = 0; i < MAX_COLS; i++)
            {
                for (int j = 0; j < MAX_ROWS; j++)
                {

                    map[i, j].Visible = (i < cs && j < rs);
                    map[i, j].Text =  map[i, j].hidden_text =   "";
                }
            }

            // 2-2. 랜덤 값을 좌표로 잡아서 빈문자열을 지뢰로 교체한다.
            Random rnd = new Random();
            for(int i =0;i< mines; i++)
            {
               int r = rnd.Next(rows);
               int c = rnd.Next(cols);
               if (map[c, r].HasMine())
               {
                    i--;
                    continue;
               }
               map[c, r].hidden_text = "*";
               //map[c, r].Text = "*"; // 디버깅
            }

            // 5. 폭탄 주위의 셀에 폭탄 갯수 입력
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (map[i, j].HasMine())
                        continue;
                    int cnt = CountMines(i, j);
                    if (cnt > 0)
                    {
                        map[i, j].hidden_text = cnt.ToString();
                        //map[i, j].Text = cnt.ToString();
                    }
                    else
                    {
                        map[i, j].hidden_text = ".";
                    }

                }
            }
        }

        // 그 후
        // 우크릭 기능
        private void cell_MouseUp(object sender, MouseEventArgs e)
        {
            // 우클릭 말고 거르기
            if (e.Button != MouseButtons.Right)
                return;

            Cell c = (Cell)sender;
            if (c.Text == "P")
            {
                c.removeflag();
            }
            else
            {
                c.MakeFlag();

            }
        }


        // 3. 셀 클릭
        private void cell_Click(object sender, EventArgs e)
        {
            // sender 신호를 타입 캐스팅으로 해당 셀을 찾는다.
            Cell c = (Cell)sender;

            if (c.Text == "P")
            {
                return;
            }

            // 히든 텍스트 값을 텍스트로 줘서 클릭하면 화면에 표시
            c.Text = c.hidden_text;

            // 6. 폭탄 클릭 시 게임 오버
            if ( c.HasMine())
            {
                GameOver();
                MessageBox.Show("게임 오버");
            }

            // 7. 폭탄 아닌거 클릭 시 주위 오픈
            else if (c.hidden_text == ".")
            {
                // 쫙 펼쳐줘야함
                OpenMines(c.c, c.r);
            }

            //MessageBox.Show($"{c.r},{c.c}");
        }


        // 메뉴 스트립!
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("정말로 종료하시겠습니까?",
                "질문", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            Application.Exit();
        }

        private void _10x10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("정말로 게임을 새로 시작하시겠습니까?",
                "새 게임: 10x10", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            NewGame(10, 10, 15);
        }

        private void _20x20ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("정말로 게임을 새로 시작하시겠습니까?",
                "새 게임: 20x20", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            NewGame(20, 20, 60);
        }
    }
}
