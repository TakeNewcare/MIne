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

    // 처음 시작 시 레벨 선택 클래스
    public partial class Form2 : Form
    {
        Level_Button[] buttons = new Level_Button[3];

        Label start_text;

        public Form2(int size)
        {
            InitializeComponent();

            // 폼2 사이즈 속성 정의
            this.ClientSize = new System.Drawing.Size(size, size);

            // 라벨 생성 및 속성 정의
            start_text = new Label();
            start_text.AutoSize = false;
            start_text.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            start_text.Size = new System.Drawing.Size(200, 25);
            start_text.Location = new System.Drawing.Point(200 - 100, 25);
            start_text.Text = "지뢰찾기 게임!!!";
            Controls.Add(start_text);

            // 버튼 생성 및 이벤트 추가
            for (int i = 0; i < 3; i++)
            {
                // Level_Button(텍스트, x 위치, y 위치, 레벨);
                buttons[i] = new Level_Button($"{10 + i * 5} x {10 + i * 5}", 200 - (Level_Button.w / 2), 100 + (30 + Level_Button.h) * i, 10 + i * 5);
                buttons[i].Click += new System.EventHandler(this.Select_Level);

                Controls.Add(buttons[i]);

            }

        }

        // 버튼 클릭 시 폼 1에 레벨과 마인 숫자 반환 및 폼2 종료
        private void Select_Level(object sender, EventArgs e)
        {
            Level_Button b = (Level_Button)sender;

            Form1.current_row = b.level;
            Form1.current_col = b.level;
            Form1.current_mine = b.mines;

            this.Close();
        }
    }
}
