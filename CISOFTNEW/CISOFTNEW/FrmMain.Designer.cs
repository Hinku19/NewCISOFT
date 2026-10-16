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
            pnlLogo = new Panel();
            pnlLogoFondo = new CISOFTNEW.Controles.PanelRedondeado();
            picLogo = new PictureBox();
            flpMenu = new FlowLayoutPanel();
            lblUsuario = new Label();
            pnlContent.SuspendLayout();
            pnlSidebar.SuspendLayout();
            pnlLogo.SuspendLayout();
            pnlLogoFondo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
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
            pnlSidebar.Controls.Add(pnlLogo);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(220, 671);
            pnlSidebar.TabIndex = 1;
            //
            // pnlLogo
            //
            pnlLogo.Controls.Add(pnlLogoFondo);
            pnlLogo.Dock = DockStyle.Top;
            pnlLogo.Location = new Point(0, 0);
            pnlLogo.Name = "pnlLogo";
            pnlLogo.Padding = new Padding(12, 14, 12, 14);
            pnlLogo.Size = new Size(220, 90);
            pnlLogo.TabIndex = 0;
            //
            // pnlLogoFondo
            //
            pnlLogoFondo.BackColor = Color.White;
            pnlLogoFondo.Controls.Add(picLogo);
            pnlLogoFondo.Dock = DockStyle.Fill;
            pnlLogoFondo.Name = "pnlLogoFondo";
            pnlLogoFondo.Padding = new Padding(10, 6, 10, 6);
            pnlLogoFondo.Radio = 10;
            pnlLogoFondo.TabIndex = 0;
            //
            // picLogo
            //
            picLogo.BackColor = Color.White;
            picLogo.Dock = DockStyle.Fill;
            picLogo.Name = "picLogo";
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            //
            // flpMenu
            //
            flpMenu.AutoScroll = true;
            flpMenu.Dock = DockStyle.Fill;
            flpMenu.FlowDirection = FlowDirection.TopDown;
            flpMenu.Location = new Point(0, 90);
            flpMenu.Name = "flpMenu";
            flpMenu.Padding = new Padding(8, 0, 0, 0);
            flpMenu.Size = new Size(220, 521);
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
            pnlLogo.ResumeLayout(false);
            pnlLogoFondo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpDashboard;
        private Panel pnlContent;
        private Panel pnlSidebar;
        private Panel pnlLogo;
        private CISOFTNEW.Controles.PanelRedondeado pnlLogoFondo;
        private PictureBox picLogo;
        private FlowLayoutPanel flpMenu;
        private Label lblUsuario;
    }
}
