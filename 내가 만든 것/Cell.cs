using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mine
{
    // 각 칸을 하나하나의 버튼으로 만들기 위해 cell이라는 객체 생성
    internal class Cell : Button
    {
        public const int cell_size = 25;

        public string hidden_text;
        public int x;
        public int y;


        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
            Make_Cell(x, y);
        }


        private void Make_Cell(int x, int y)
        {
            this.Location = new System.Drawing.Point(x * cell_size, y * cell_size + Form1.menu_height + display.HEIGHT + 10);
            this.Name = "Cell";
            this.Size = new System.Drawing.Size(cell_size + 3, cell_size + 3);
            this.TabIndex = 0;
            this.Text = "";
            this.UseVisualStyleBackColor = true;

        }



    }
}
