namespace CISOFTNEW
{
    partial class FrmBuscarDireccion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBuscarDireccion));
            txtBuscador = new Guna.UI2.WinForms.Guna2TextBox();
            btnSeleccionar = new Guna.UI2.WinForms.Guna2Button();
            btnCancelar = new Guna.UI2.WinForms.Guna2Button();
            dgvResultados = new Guna.UI2.WinForms.Guna2DataGridView();
            tmrBuscar = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)dgvResultados).BeginInit();
            SuspendLayout();
            // 
            // txtBuscador
            // 
            txtBuscador.BorderColor = Color.FromArgb(141, 169, 196);
            txtBuscador.BorderRadius = 10;
            txtBuscador.CustomizableEdges = customizableEdges7;
            txtBuscador.DefaultText = "";
            txtBuscador.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtBuscador.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtBuscador.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtBuscador.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtBuscador.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtBuscador.Font = new Font("Segoe UI", 9F);
            txtBuscador.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtBuscador.Location = new Point(12, 21);
            txtBuscador.Name = "txtBuscador";
            txtBuscador.PlaceholderForeColor = Color.FromArgb(19, 49, 62);
            txtBuscador.PlaceholderText = "Escriba su consulta aquí...";
            txtBuscador.SelectedText = "";
            txtBuscador.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtBuscador.Size = new Size(566, 36);
            txtBuscador.TabIndex = 0;
            // 
            // btnSeleccionar
            // 
            btnSeleccionar.BackColor = Color.Transparent;
            btnSeleccionar.BorderColor = Color.FromArgb(141, 169, 196);
            btnSeleccionar.BorderRadius = 10;
            btnSeleccionar.CustomizableEdges = customizableEdges9;
            btnSeleccionar.DisabledState.BorderColor = Color.DarkGray;
            btnSeleccionar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSeleccionar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSeleccionar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSeleccionar.FillColor = Color.FromArgb(19, 64, 116);
            btnSeleccionar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSeleccionar.ForeColor = Color.White;
            btnSeleccionar.HoverState.FillColor = Color.FromArgb(19, 49, 62);
            btnSeleccionar.Location = new Point(584, 21);
            btnSeleccionar.Name = "btnSeleccionar";
            btnSeleccionar.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnSeleccionar.Size = new Size(108, 36);
            btnSeleccionar.TabIndex = 1;
            btnSeleccionar.Text = "Seleccionar";
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.Transparent;
            btnCancelar.BorderColor = Color.FromArgb(141, 169, 196);
            btnCancelar.BorderRadius = 10;
            btnCancelar.CustomizableEdges = customizableEdges11;
            btnCancelar.DisabledState.BorderColor = Color.DarkGray;
            btnCancelar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancelar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancelar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancelar.FillColor = Color.FromArgb(141, 169, 196);
            btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.HoverState.FillColor = Color.FromArgb(19, 49, 62);
            btnCancelar.Location = new Point(698, 21);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnCancelar.Size = new Size(90, 36);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "Cancelar";
            // 
            // dgvResultados
            // 
            dgvResultados.AllowUserToAddRows = false;
            dgvResultados.AllowUserToDeleteRows = false;
            dgvResultados.AllowUserToResizeColumns = false;
            dgvResultados.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = Color.White;
            dgvResultados.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvResultados.BorderStyle = BorderStyle.FixedSingle;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvResultados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvResultados.ColumnHeadersHeight = 4;
            dgvResultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvResultados.DefaultCellStyle = dataGridViewCellStyle6;
            dgvResultados.GridColor = Color.FromArgb(141, 169, 196);
            dgvResultados.Location = new Point(12, 73);
            dgvResultados.Name = "dgvResultados";
            dgvResultados.RowHeadersVisible = false;
            dgvResultados.Size = new Size(776, 365);
            dgvResultados.TabIndex = 3;
            dgvResultados.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvResultados.ThemeStyle.GridColor = Color.FromArgb(141, 169, 196);
            dgvResultados.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgvResultados.ThemeStyle.HeaderStyle.Height = 4;
            dgvResultados.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvResultados.ThemeStyle.RowsStyle.Height = 25;
            // 
            // tmrBuscar
            // 
            tmrBuscar.Interval = 300;
            // 
            // FrmBuscarDireccion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(238, 244, 237);
            ClientSize = new Size(800, 450);
            Controls.Add(dgvResultados);
            Controls.Add(btnCancelar);
            Controls.Add(btnSeleccionar);
            Controls.Add(txtBuscador);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmBuscarDireccion";
            Padding = new Padding(20);
            Text = "Buscador";
            ((System.ComponentModel.ISupportInitialize)dgvResultados).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox txtBuscador;
        private Guna.UI2.WinForms.Guna2Button btnSeleccionar;
        private Guna.UI2.WinForms.Guna2Button btnCancelar;
        private Guna.UI2.WinForms.Guna2DataGridView dgvResultados;
        private System.Windows.Forms.Timer tmrBuscar;
    }
}