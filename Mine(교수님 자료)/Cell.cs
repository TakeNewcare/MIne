using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mine
{
    public class Cell : Button
    {
        public const int W = 20;
        public readonly int c;
        public readonly int r;
        public string hidden_text;

        public Cell(Control.ControlCollection controls, int c, int r)
        {
            this.c = c;
            this.r = r;

            this.Location = new System.Drawing.Point(c * W, Board.MenuHeight + r * W);
            this.Name = "button1";
            this.Size = new System.Drawing.Size(24, 23);
            this.TabIndex = 0;
            this.Text = "";
            this.UseVisualStyleBackColor = true;

            controls.Add(this);
        }

        public bool HasMine()
        {
            return hidden_text == "*";
        }

        public void MakeFlag()
        {
            Text = "P";
        }

        public void removeflag()
        {
            this.Text = "";
        }
        public void Open()
        {
            Text = hidden_text;
        }

    }

}
