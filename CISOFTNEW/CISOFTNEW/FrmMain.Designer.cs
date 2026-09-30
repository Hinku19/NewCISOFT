namespace CISOFTNEW
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            tlpDashboard = new TableLayoutPanel();
            lblWelcome = new Label();
            SuspendLayout();
            // 
            // tlpDashboard
            // 
            tlpDashboard.AutoSize = true;
            tlpDashboard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpDashboard.BackColor = Color.Transparent;
            tlpDashboard.ColumnCount = 2;
            tlpDashboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpDashboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpDashboard.Location = new Point(603, 471);
            tlpDashboard.Name = "tlpDashboard";
            tlpDashboard.RowCount = 2;
            tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpDashboard.Size = new Size(0, 0);
            tlpDashboard.TabIndex = 0;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Dock = DockStyle.Top;
            lblWelcome.Font = new Font("Tahoma", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWelcome.Location = new Point(0, 0);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(0, 25);
            lblWelcome.TabIndex = 1;
            lblWelcome.TextAlign = ContentAlignment.TopCenter;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1822, 671);
            Controls.Add(lblWelcome);
            Controls.Add(tlpDashboard);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmMain";
            Text = "SOCOADA English Center";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tlpDashboard;
        private Label lblWelcome;
    }
}
