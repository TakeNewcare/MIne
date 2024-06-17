using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mine
{

    // 폼2에 사용되는 버튼 객체 정의
    internal class Level_Button : Button
    {
        public int level;
        public int mines;

        public const int w = 180;
        public const int h = 50;

        public Level_Button(string text, int x, int y, int level)
        {
            this.level = level;

            this.Location = new System.Drawing.Point(x, y);
            this.Name = "Button";
            this.Size = new System.Drawing.Size(180, 50);
            this.TabIndex = 1;
            this.Text = text;
            this.UseVisualStyleBackColor = true;

            // 생성 시 전달 받은 레벨로 마인 갯수 설정
            switch (level)
            {
                case 10:
                    this.mines = 10;
                    break;
                case 15:
                    this.mines = 45;
                    break;
                case 20:
                    this.mines = 60;
                    break;
            }
        }



    }
}
