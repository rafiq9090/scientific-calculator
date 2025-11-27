using System.Drawing;
using System.Windows.Forms;

namespace calculator
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        
        private TextBox txtDisplay;
        private TableLayoutPanel tlpButtons;
        private TableLayoutPanel mainLayout; 

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
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();

            
            this.ClientSize = new Size(460, 720);
            this.Text = "Scientific Calculator";
            this.BackColor = Color.LightGray; 
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            
            this.mainLayout = new TableLayoutPanel();
            this.mainLayout.Dock = DockStyle.Fill;
            this.mainLayout.RowCount = 2;
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.Padding = new Padding(12); 

            
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F));
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            this.Controls.Add(this.mainLayout); 
            
            this.txtDisplay = new TextBox();
            this.txtDisplay.Dock = DockStyle.Fill;
            this.txtDisplay.Font = new Font("Arial", 36F, FontStyle.Bold); 
            this.txtDisplay.TextAlign = HorizontalAlignment.Right;
            this.txtDisplay.BackColor = Color.White;
            this.txtDisplay.BorderStyle = BorderStyle.FixedSingle;
            this.txtDisplay.ReadOnly = true;
            this.txtDisplay.Margin = new Padding(0, 0, 0, 15);
            this.txtDisplay.Text = "0"; 

            this.mainLayout.Controls.Add(this.txtDisplay, 0, 0);

           
            this.tlpButtons = new TableLayoutPanel();
            this.tlpButtons.Dock = DockStyle.Fill;
            this.tlpButtons.ColumnCount = 4;
            this.tlpButtons.RowCount = 8;
            this.tlpButtons.CellBorderStyle = TableLayoutPanelCellBorderStyle.None; 

            
            for (int i = 0; i < 4; i++)
                this.tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

           
            for (int i = 0; i < 8; i++)
                this.tlpButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));

            this.mainLayout.Controls.Add(this.tlpButtons, 0, 1);

            
            AddBtn("sin", 0, 0); AddBtn("cos", 1, 0); AddBtn("tan", 2, 0); AddBtn("ln", 3, 0);

            
            AddBtn("π", 0, 1); AddBtn("e", 1, 1); AddBtn("n!", 2, 1); AddBtn("log", 3, 1);

           
            AddBtn("(", 0, 2); AddBtn(")", 1, 2); AddBtn("√", 2, 2); AddBtn("^", 3, 2);

            
            AddBtn("%", 0, 3); AddBtn("CE", 1, 3); AddBtn("C", 2, 3); AddBtn("÷", 3, 3); 

            
            this.tlpButtons.Controls.Add(CreateButton("7"), 0, 4);
            this.tlpButtons.Controls.Add(CreateButton("8"), 1, 4);
            this.tlpButtons.Controls.Add(CreateButton("9"), 2, 4);
            this.tlpButtons.Controls.Add(CreateButton("×"), 3, 4);

            this.tlpButtons.Controls.Add(CreateButton("4"), 0, 5);
            this.tlpButtons.Controls.Add(CreateButton("5"), 1, 5);
            this.tlpButtons.Controls.Add(CreateButton("6"), 2, 5);
            this.tlpButtons.Controls.Add(CreateButton("−"), 3, 5);

            this.tlpButtons.Controls.Add(CreateButton("1"), 0, 6);
            this.tlpButtons.Controls.Add(CreateButton("2"), 1, 6);
            this.tlpButtons.Controls.Add(CreateButton("3"), 2, 6);
            this.tlpButtons.Controls.Add(CreateButton("+"), 3, 6);

            
            var btn0 = CreateButton("0");
            this.tlpButtons.Controls.Add(btn0, 0, 7);
            this.tlpButtons.SetColumnSpan(btn0, 2);

            var btnDot = CreateButton(".");
            this.tlpButtons.Controls.Add(btnDot, 2, 7);

            var btnEq = CreateButton("=");
            btnEq.BackColor = Color.DarkOrange; 
            btnEq.ForeColor = Color.Black;
            btnEq.Font = new Font("Arial", 20F, FontStyle.Bold);
            this.tlpButtons.Controls.Add(btnEq, 3, 7);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        
        private Button CreateButton(string text)
        {
            Button btn = new Button(); 
            btn.Text = text;
            btn.Dock = DockStyle.Fill;
            btn.Font = new Font("Arial", 16F, FontStyle.Regular); 
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.WhiteSmoke; 
            btn.ForeColor = Color.Black;
            btn.Margin = new Padding(5);

            
            btn.Click += Btn_Click;
            return btn;
        }

        
        private void AddBtn(string text, int col, int row)
        {
           
            Button btn = CreateButton(text);
            this.tlpButtons.Controls.Add(btn, col, row);
        }

        #endregion
    }
}