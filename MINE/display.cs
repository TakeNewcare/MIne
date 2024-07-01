using Mine.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mine
{

    // panel을 상속받아 display라는 전광판 객체 생성
    public class display : Panel
    {
        public const int HEIGHT = 55;

        public Button L1;
        public Button L2;
        public Button L3;


        // 버튼 name 속성을 배열로 정리하여 사용
        private string[] label_name = { "flag_count", "time", "smile" };

        public display()
        {

            // 버튼 객체 초기화
            L1 = new Button();
            L2 = new Button();
            L3 = new Button();

            // display 객체 속성
            this.Location = new System.Drawing.Point(0, Form1.menu_height + 5);
            this.Name = "game_panel";
            this.Size = new System.Drawing.Size(Cell.cell_size * Form1.current_mine, HEIGHT);
            this.TabIndex = 3;



            // 버튼 추가하기(객체, name, 위치, 위치)  ※ 위치 : 판넬 위치를 기준으로 한다!!
            Make_btn(L1, label_name[0], 0, 0, 80, HEIGHT);
            this.Controls.Add(L1);

            Make_btn(L2, label_name[1], Cell.cell_size * 10 - 80, 0, 80, HEIGHT);
            this.Controls.Add(L2);

            Make_btn(L3, label_name[2], (Cell.cell_size * 10) / 2 - 20, 10, 40, 40);
            this.Controls.Add(L3);


        }


        public void Make_btn(Button btn, string name, int x, int y, int w, int h)
        {
            if (name == "smile")
            {
                btn.Location = new System.Drawing.Point(x, y);
                btn.Name = name;
                btn.Size = new System.Drawing.Size(w, h);
                btn.TabIndex = 2;
                btn.UseVisualStyleBackColor = true;

                return;
            }
            btn.Location = new System.Drawing.Point(x, y);
            btn.Name = name;
            btn.Size = new System.Drawing.Size(w, h);
            btn.TabIndex = 2;
            btn.Text = "0";
            btn.UseVisualStyleBackColor = true;
            btn.BackColor = System.Drawing.Color.Black;
            btn.ForeColor = System.Drawing.Color.Red;
            btn.Font = new System.Drawing.Font("휴먼둥근헤드라인", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            

        }



    }
}
