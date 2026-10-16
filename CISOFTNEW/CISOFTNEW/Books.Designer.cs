namespace CISOFTNEW
{
    partial class Books
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btnSalir = new CISOFTNEW.Controles.BotonRedondeado();
            label2 = new Label();
            label1 = new Label();
            lblCatAlu = new Label();
            botonRedondeado1 = new CISOFTNEW.Controles.BotonRedondeado();
            panelRedondeado1 = new CISOFTNEW.Controles.PanelRedondeado();
            txtEmployeeId = new CISOFTNEW.Controles.CampoTexto();
            cmbMaestros = new Guna.UI2.WinForms.Guna2ComboBox();
            btnBuscar = new CISOFTNEW.Controles.BotonRedondeado();
            btnLimpiar = new CISOFTNEW.Controles.BotonRedondeado();
            toolTip1 = new ToolTip(components);
            txtName = new CISOFTNEW.Controles.CampoTexto();
            panelRedondeado1.SuspendLayout();
            SuspendLayout();
            // 
            // btnSalir
            // 
            btnSalir.Alineacion = HorizontalAlignment.Center;
            btnSalir.BackColor = Color.FromArgb(19, 64, 116);
            btnSalir.ColorBorde = Color.Transparent;
            btnSalir.ColorHover = Color.FromArgb(11, 37, 69);
            btnSalir.ColorTextoHover = Color.Empty;
            btnSalir.FlatAppearance.BorderSize = 0;
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSalir.ForeColor = Color.White;
            btnSalir.GrosorBorde = 0F;
            btnSalir.Icono = "";
            btnSalir.Location = new Point(950, 46);
            btnSalir.Margin = new Padding(3, 2, 3, 2);
            btnSalir.Name = "btnSalir";
            btnSalir.Radio = 12;
            btnSalir.Size = new Size(95, 45);
            btnSalir.TabIndex = 13;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(18, 80);
            label2.Name = "label2";
            label2.Size = new Size(424, 21);
            label2.TabIndex = 12;
            label2.Text = "Consulta, registra y actualiza la información de los Maestros";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(18, 49);
            label1.Name = "label1";
            label1.Size = new Size(119, 32);
            label1.TabIndex = 11;
            label1.Text = "Maestros";
            // 
            // lblCatAlu
            // 
            lblCatAlu.AutoSize = true;
            lblCatAlu.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCatAlu.Location = new Point(18, 21);
            lblCatAlu.Name = "lblCatAlu";
            lblCatAlu.Size = new Size(157, 21);
            lblCatAlu.TabIndex = 10;
            lblCatAlu.Text = "Catálogos / Maestros";
            // 
            // botonRedondeado1
            // 
            botonRedondeado1.Alineacion = HorizontalAlignment.Center;
            botonRedondeado1.BackColor = Color.FromArgb(19, 64, 116);
            botonRedondeado1.ColorBorde = Color.Transparent;
            botonRedondeado1.ColorHover = Color.FromArgb(11, 37, 69);
            botonRedondeado1.ColorTextoHover = Color.Empty;
            botonRedondeado1.FlatAppearance.BorderSize = 0;
            botonRedondeado1.FlatStyle = FlatStyle.Flat;
            botonRedondeado1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            botonRedondeado1.ForeColor = Color.White;
            botonRedondeado1.GrosorBorde = 0F;
            botonRedondeado1.Icono = "";
            botonRedondeado1.Location = new Point(758, 46);
            botonRedondeado1.Margin = new Padding(3, 2, 3, 2);
            botonRedondeado1.Name = "botonRedondeado1";
            botonRedondeado1.Radio = 12;
            botonRedondeado1.Size = new Size(186, 45);
            botonRedondeado1.TabIndex = 9;
            botonRedondeado1.Text = "Actualizar Registros";
            toolTip1.SetToolTip(botonRedondeado1, "Actualiza la información de Maestros desde Anaran");
            botonRedondeado1.UseVisualStyleBackColor = false;
            // 
            // panelRedondeado1
            // 
            panelRedondeado1.BackColor = Color.White;
            panelRedondeado1.BordePunteado = false;
            panelRedondeado1.ColorBorde = Color.Transparent;
            panelRedondeado1.Controls.Add(txtName);
            panelRedondeado1.Controls.Add(btnLimpiar);
            panelRedondeado1.Controls.Add(btnBuscar);
            panelRedondeado1.Controls.Add(cmbMaestros);
            panelRedondeado1.Controls.Add(txtEmployeeId);
            panelRedondeado1.GrosorBorde = 0F;
            panelRedondeado1.Location = new Point(18, 125);
            panelRedondeado1.Margin = new Padding(3, 2, 3, 2);
            panelRedondeado1.Name = "panelRedondeado1";
            panelRedondeado1.Padding = new Padding(18, 15, 18, 15);
            panelRedondeado1.Radio = 16;
            panelRedondeado1.Size = new Size(1027, 55);
            panelRedondeado1.TabIndex = 14;
            // 
            // txtEmployeeId
            // 
            txtEmployeeId.Font = new Font("Segoe UI", 11F);
            txtEmployeeId.ForeColor = Color.FromArgb(11, 37, 69);
            txtEmployeeId.Location = new Point(20, 10);
            txtEmployeeId.Margin = new Padding(3, 2, 3, 2);
            txtEmployeeId.MaxCaracteres = 32767;
            txtEmployeeId.Mayusculas = CharacterCasing.Normal;
            txtEmployeeId.Name = "txtEmployeeId";
            txtEmployeeId.Padding = new Padding(12, 0, 14, 0);
            txtEmployeeId.Placeholder = "Número de Empleado";
            txtEmployeeId.Radio = 10;
            txtEmployeeId.SangriaIzquierda = 12;
            txtEmployeeId.Size = new Size(179, 36);
            txtEmployeeId.SoloLectura = false;
            txtEmployeeId.TabIndex = 2;
            txtEmployeeId.Texto = "";
            // 
            // cmbMaestros
            // 
            cmbMaestros.BackColor = Color.Transparent;
            cmbMaestros.BorderColor = Color.SteelBlue;
            cmbMaestros.BorderRadius = 10;
            cmbMaestros.CustomizableEdges = customizableEdges1;
            cmbMaestros.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMaestros.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaestros.FocusedColor = Color.FromArgb(94, 148, 255);
            cmbMaestros.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cmbMaestros.Font = new Font("Segoe UI", 10F);
            cmbMaestros.ForeColor = Color.FromArgb(68, 88, 112);
            cmbMaestros.ItemHeight = 30;
            cmbMaestros.Location = new Point(448, 10);
            cmbMaestros.Name = "cmbMaestros";
            cmbMaestros.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cmbMaestros.Size = new Size(277, 36);
            cmbMaestros.TabIndex = 15;
            // 
            // btnBuscar
            // 
            btnBuscar.Alineacion = HorizontalAlignment.Center;
            btnBuscar.BackColor = Color.SteelBlue;
            btnBuscar.ColorBorde = Color.Transparent;
            btnBuscar.ColorHover = Color.FromArgb(11, 37, 69);
            btnBuscar.ColorTextoHover = Color.Empty;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.GrosorBorde = 0F;
            btnBuscar.Icono = "";
            btnBuscar.Location = new Point(740, 10);
            btnBuscar.Margin = new Padding(3, 2, 3, 2);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Radio = 12;
            btnBuscar.Size = new Size(95, 36);
            btnBuscar.TabIndex = 15;
            btnBuscar.Text = "Buscar";
            toolTip1.SetToolTip(btnBuscar, "Realiza búsqueda según el o los filtros");
            btnBuscar.UseVisualStyleBackColor = false;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Alineacion = HorizontalAlignment.Center;
            btnLimpiar.BackColor = Color.SteelBlue;
            btnLimpiar.ColorBorde = Color.Transparent;
            btnLimpiar.ColorHover = Color.FromArgb(11, 37, 69);
            btnLimpiar.ColorTextoHover = Color.Empty;
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.GrosorBorde = 0F;
            btnLimpiar.Icono = "";
            btnLimpiar.Location = new Point(841, 10);
            btnLimpiar.Margin = new Padding(3, 2, 3, 2);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Radio = 12;
            btnLimpiar.Size = new Size(128, 36);
            btnLimpiar.TabIndex = 16;
            btnLimpiar.Text = "Limpiar Filtros";
            toolTip1.SetToolTip(btnLimpiar, "Limpia los filtros");
            btnLimpiar.UseVisualStyleBackColor = false;
            // 
            // txtName
            // 
            txtName.Font = new Font("Segoe UI", 11F);
            txtName.ForeColor = Color.FromArgb(11, 37, 69);
            txtName.Location = new Point(205, 10);
            txtName.Margin = new Padding(3, 2, 3, 2);
            txtName.MaxCaracteres = 32767;
            txtName.Mayusculas = CharacterCasing.Normal;
            txtName.Name = "txtName";
            txtName.Padding = new Padding(12, 0, 14, 0);
            txtName.Placeholder = "Nombre del empleado";
            txtName.Radio = 10;
            txtName.SangriaIzquierda = 12;
            txtName.Size = new Size(237, 36);
            txtName.SoloLectura = false;
            txtName.TabIndex = 17;
            txtName.Texto = "";
            // 
            // Books
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelRedondeado1);
            Controls.Add(btnSalir);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblCatAlu);
            Controls.Add(botonRedondeado1);
            Name = "Books";
            Size = new Size(1515, 578);
            panelRedondeado1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controles.BotonRedondeado btnSalir;
        private Label label2;
        private Label label1;
        private Label lblCatAlu;
        private Controles.BotonRedondeado botonRedondeado1;
        private Controles.PanelRedondeado panelRedondeado1;
        private Controles.CampoTexto txtEmployeeId;
        private Guna.UI2.WinForms.Guna2ComboBox cmbMaestros;
        private Controles.BotonRedondeado btnLimpiar;
        private Controles.BotonRedondeado btnBuscar;
        private ToolTip toolTip1;
        private Controles.CampoTexto txtName;
    }
}
