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
            pnlContent = new Panel();
            pnlSidebar = new Panel();
            lblLogo = new Label();
            flpMenu = new FlowLayoutPanel();
            lblUsuario = new Label();
            pnlContent.SuspendLayout();
            pnlSidebar.SuspendLayout();
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
            tlpDashboard.Dock = DockStyle.Fill;
            tlpDashboard.Location = new Point(0, 0);
            tlpDashboard.Name = "tlpDashboard";
            tlpDashboard.RowCount = 2;
            tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpDashboard.Size = new Size(1822, 646);
            tlpDashboard.TabIndex = 0;
            //
            // pnlContent
            //
            pnlContent.BackColor = Color.FromArgb(238, 244, 237);
            pnlContent.Controls.Add(tlpDashboard);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(220, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1602, 671);
            pnlContent.TabIndex = 2;
            //
            // pnlSidebar
            //
            pnlSidebar.BackColor = Color.FromArgb(11, 37, 69);
            pnlSidebar.Controls.Add(flpMenu);
            pnlSidebar.Controls.Add(lblUsuario);
            pnlSidebar.Controls.Add(lblLogo);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(220, 671);
            pnlSidebar.TabIndex = 1;
            //
            // lblLogo
            //
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLogo.ForeColor = Color.FromArgb(238, 244, 237);
            lblLogo.Location = new Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Padding = new Padding(20, 0, 0, 0);
            lblLogo.Size = new Size(220, 70);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "SOCOADA";
            lblLogo.TextAlign = ContentAlignment.MiddleLeft;
            //
            // flpMenu
            //
            flpMenu.AutoScroll = true;
            flpMenu.Dock = DockStyle.Fill;
            flpMenu.FlowDirection = FlowDirection.TopDown;
            flpMenu.Location = new Point(0, 70);
            flpMenu.Name = "flpMenu";
            flpMenu.Padding = new Padding(8, 0, 0, 0);
            flpMenu.Size = new Size(220, 541);
            flpMenu.TabIndex = 1;
            flpMenu.WrapContents = false;
            //
            // lblUsuario
            //
            lblUsuario.Dock = DockStyle.Bottom;
            lblUsuario.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUsuario.ForeColor = Color.FromArgb(238, 244, 237);
            lblUsuario.Location = new Point(0, 611);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Padding = new Padding(20, 0, 0, 0);
            lblUsuario.Size = new Size(220, 60);
            lblUsuario.TabIndex = 2;
            lblUsuario.TextAlign = ContentAlignment.MiddleLeft;
            //
            // FrmMain
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1822, 671);
            Controls.Add(pnlContent);
            Controls.Add(pnlSidebar);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmMain";
            Text = "SOCOADA English Center";
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            pnlSidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpDashboard;
        private Panel pnlContent;
        private Panel pnlSidebar;
        private Label lblLogo;
        private FlowLayoutPanel flpMenu;
        private Label lblUsuario;
    }
}
