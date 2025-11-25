using System.Drawing; 
using System.Windows.Forms; 

namespace calculator
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

       
        private TextBox txtDisplay;
        private TableLayoutPanel tlpButtons;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.ClientSize = new Size(460, 720);
            this.Text = "Scientific Calculator";
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                Padding = new Padding(15)
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.Controls.Add(mainLayout);

            
            txtDisplay = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 36F, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Right,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true,
                Margin = new Padding(0, 0, 0, 15),
                Text  = ""
            };

            mainLayout.Controls.Add(txtDisplay, 0, 0);

            
            tlpButtons = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 8,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };

            for (int i = 0; i < 4; i++)
                tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            for (int i = 0; i < 8; i++)
                tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));

            mainLayout.Controls.Add(tlpButtons, 0, 1);

            AddBtn("sin", 0, 0); AddBtn("cos", 1, 0); AddBtn("tan", 2, 0); AddBtn("ln", 3, 0);

           
            AddBtn("π", 0, 1); AddBtn("e", 1, 1); AddBtn("n!", 2, 1); AddBtn("log", 3, 1);

         
            AddBtn("(", 0, 2); AddBtn(")", 1, 2); AddBtn("√", 2, 2); AddBtn("^", 3, 2);

            
            AddBtn("%", 0, 3); AddBtn("CE", 1, 3); AddBtn("C", 2, 3); AddBtn("÷", 3, 3);

            
            AddBtn("7", 0, 4); AddBtn("8", 1, 4); AddBtn("9", 2, 4); AddBtn("×", 3, 4);

            
            AddBtn("4", 0, 5); AddBtn("5", 1, 5); AddBtn("6", 2, 5); AddBtn("−", 3, 5);

            
            AddBtn("1", 0, 6); AddBtn("2", 1, 6); AddBtn("3", 2, 6); AddBtn("+", 3, 6);

            
            var btn0 = CreateButton("0");
            tlpButtons.Controls.Add(btn0, 0, 7);
            tlpButtons.SetColumnSpan(btn0, 2);    

            var btnDot = CreateButton(".");
            tlpButtons.Controls.Add(btnDot, 2, 7); 

            var btnEq = CreateButton("=");
            btnEq.BackColor = Color.FromArgb(0, 122, 255);
            btnEq.ForeColor = Color.White;
            btnEq.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            tlpButtons.Controls.Add(btnEq, 3, 7); 

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Button CreateButton(string text)
        {
            var btn = new Button
            {
                Text = text,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 16F, FontStyle.Regular),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.White,
                ForeColor = Color.Black,
                Margin = new Padding(5)
            };
            
            btn.Click += Btn_Click;
            return btn;
        }

        private void AddBtn(string text, int col, int row)
        {
            var btn = CreateButton(text);
            tlpButtons.Controls.Add(btn, col, row);
        }

        #endregion
    }
}