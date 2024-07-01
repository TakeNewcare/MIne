namespace Mine
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menu = new System.Windows.Forms.ToolStripMenuItem();
            this.새게임ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._10x10 = new System.Windows.Forms.ToolStripMenuItem();
            this._15x15 = new System.Windows.Forms.ToolStripMenuItem();
            this._20x20 = new System.Windows.Forms.ToolStripMenuItem();
            this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종료ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menu
            // 
            this.menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.새게임ToolStripMenuItem,
            this.종료ToolStripMenuItem,
            this.종료ToolStripMenuItem1});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(43, 20);
            this.menu.Text = "메뉴";
            // 
            // 새게임ToolStripMenuItem
            // 
            this.새게임ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._10x10,
            this._15x15,
            this._20x20});
            this.새게임ToolStripMenuItem.Name = "새게임ToolStripMenuItem";
            this.새게임ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.새게임ToolStripMenuItem.Text = "새 게임";
            // 
            // _10x10
            // 
            this._10x10.Name = "_10x10";
            this._10x10.Size = new System.Drawing.Size(108, 22);
            this._10x10.Text = "10x10";
            this._10x10.Click += new System.EventHandler(this.new_10x10);
            // 
            // _15x15
            // 
            this._15x15.Name = "_15x15";
            this._15x15.Size = new System.Drawing.Size(108, 22);
            this._15x15.Text = "15x15";
            this._15x15.Click += new System.EventHandler(this.new_15x15);
            // 
            // _20x20
            // 
            this._20x20.Name = "_20x20";
            this._20x20.Size = new System.Drawing.Size(108, 22);
            this._20x20.Text = "20x20";
            this._20x20.Click += new System.EventHandler(this.new_20x20);
            // 
            // 종료ToolStripMenuItem
            // 
            this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            this.종료ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.종료ToolStripMenuItem.Text = "정보";
            this.종료ToolStripMenuItem.Click += new System.EventHandler(this.Game_Info);
            // 
            // 종료ToolStripMenuItem1
            // 
            this.종료ToolStripMenuItem1.Name = "종료ToolStripMenuItem1";
            this.종료ToolStripMenuItem1.Size = new System.Drawing.Size(114, 22);
            this.종료ToolStripMenuItem1.Text = "종료";
            this.종료ToolStripMenuItem1.Click += new System.EventHandler(this.Game_End);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Game_Start);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu;
        private System.Windows.Forms.ToolStripMenuItem 새게임ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem _10x10;
        private System.Windows.Forms.ToolStripMenuItem _15x15;
        private System.Windows.Forms.ToolStripMenuItem _20x20;
        private System.Windows.Forms.Timer timer1;
    }
}

