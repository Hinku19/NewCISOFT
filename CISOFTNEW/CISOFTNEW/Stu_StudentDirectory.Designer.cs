namespace CISOFTNEW
{
    partial class Stu_StudentDirectory
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            botonRedondeado1 = new CISOFTNEW.Controles.BotonRedondeado();
            panelRedondeado1 = new CISOFTNEW.Controles.PanelRedondeado();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            campoTexto1 = new CISOFTNEW.Controles.CampoTexto();
            label6 = new Label();
            campoTexto4 = new CISOFTNEW.Controles.CampoTexto();
            label5 = new Label();
            label4 = new Label();
            lblCatAlu = new Label();
            label1 = new Label();
            label2 = new Label();
            panelRedondeado2 = new CISOFTNEW.Controles.PanelRedondeado();
            dgvAlumnos = new CISOFTNEW.Controles.TablaRedondeada();
            colAlumno = new DataGridViewTextBoxColumn();
            colMatricula = new DataGridViewTextBoxColumn();
            colClasificacion = new DataGridViewTextBoxColumn();
            colGrupo = new DataGridViewTextBoxColumn();
            colCelular = new DataGridViewTextBoxColumn();
            colVer = new DataGridViewLinkColumn();
            panelRedondeado1.SuspendLayout();
            panelRedondeado2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAlumnos).BeginInit();
            SuspendLayout();
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
            botonRedondeado1.Location = new Point(1004, 62);
            botonRedondeado1.Name = "botonRedondeado1";
            botonRedondeado1.Radio = 12;
            botonRedondeado1.Size = new Size(212, 60);
            botonRedondeado1.TabIndex = 1;
            botonRedondeado1.Text = " + Nuevo alumno";
            botonRedondeado1.UseVisualStyleBackColor = false;
            // 
            // panelRedondeado1
            // 
            panelRedondeado1.BackColor = Color.White;
            panelRedondeado1.BordePunteado = false;
            panelRedondeado1.ColorBorde = Color.Transparent;
            panelRedondeado1.Controls.Add(comboBox1);
            panelRedondeado1.Controls.Add(comboBox2);
            panelRedondeado1.Controls.Add(campoTexto1);
            panelRedondeado1.Controls.Add(label6);
            panelRedondeado1.Controls.Add(campoTexto4);
            panelRedondeado1.Controls.Add(label5);
            panelRedondeado1.Controls.Add(label4);
            panelRedondeado1.GrosorBorde = 0F;
            panelRedondeado1.Location = new Point(42, 153);
            panelRedondeado1.Margin = new Padding(0, 12, 0, 12);
            panelRedondeado1.Name = "panelRedondeado1";
            panelRedondeado1.Radio = 16;
            panelRedondeado1.Size = new Size(1174, 101);
            panelRedondeado1.TabIndex = 3;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(691, 50);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(145, 28);
            comboBox1.TabIndex = 8;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Familiar", "Individual", "Pareja", "Empleado" });
            comboBox2.Location = new Point(461, 50);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(200, 28);
            comboBox2.TabIndex = 9;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // campoTexto1
            // 
            campoTexto1.Font = new Font("Segoe UI", 11F);
            campoTexto1.ForeColor = Color.FromArgb(11, 37, 69);
            campoTexto1.Location = new Point(23, 50);
            campoTexto1.MaxCaracteres = 32767;
            campoTexto1.Mayusculas = CharacterCasing.Normal;
            campoTexto1.Name = "campoTexto1";
            campoTexto1.Placeholder = "Nombre, matrícula, CURP o RFC";
            campoTexto1.Radio = 10;
            campoTexto1.SangriaIzquierda = 14;
            campoTexto1.Size = new Size(414, 28);
            campoTexto1.SoloLectura = false;
            campoTexto1.TabIndex = 14;
            campoTexto1.Texto = "";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(23, 22);
            label6.Name = "label6";
            label6.Size = new Size(128, 25);
            label6.TabIndex = 0;
            label6.Text = "&Buscar alumno";
            label6.Click += label6_Click;
            // 
            // campoTexto4
            // 
            campoTexto4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            campoTexto4.ForeColor = Color.FromArgb(11, 37, 69);
            campoTexto4.Location = new Point(874, 46);
            campoTexto4.MaxCaracteres = 32767;
            campoTexto4.Mayusculas = CharacterCasing.Normal;
            campoTexto4.Name = "campoTexto4";
            campoTexto4.Placeholder = "";
            campoTexto4.Radio = 10;
            campoTexto4.SangriaIzquierda = 14;
            campoTexto4.Size = new Size(149, 32);
            campoTexto4.SoloLectura = false;
            campoTexto4.TabIndex = 13;
            campoTexto4.Texto = "Limpiar filtros";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(691, 20);
            label5.Name = "label5";
            label5.Size = new Size(132, 23);
            label5.TabIndex = 12;
            label5.Text = "Grupo asignado";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(461, 24);
            label4.Name = "label4";
            label4.Size = new Size(103, 23);
            label4.TabIndex = 9;
            label4.Text = "Clasificación";
            // 
            // lblCatAlu
            // 
            lblCatAlu.AutoSize = true;
            lblCatAlu.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCatAlu.Location = new Point(42, 30);
            lblCatAlu.Name = "lblCatAlu";
            lblCatAlu.Size = new Size(195, 28);
            lblCatAlu.TabIndex = 5;
            lblCatAlu.Text = "Catálogos / Alumnos";
            lblCatAlu.Click += lblCatAlu_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(42, 67);
            label1.Name = "label1";
            label1.Size = new Size(142, 41);
            label1.TabIndex = 6;
            label1.Text = "Alumnos";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(42, 108);
            label2.Name = "label2";
            label2.Size = new Size(529, 28);
            label2.TabIndex = 7;
            label2.Text = "Consulta, registra y actualiza la información de los alumnos.";
            // 
            // panelRedondeado2
            // 
            panelRedondeado2.BackColor = Color.White;
            panelRedondeado2.BordePunteado = false;
            panelRedondeado2.ColorBorde = Color.Transparent;
            panelRedondeado2.Controls.Add(dgvAlumnos);
            panelRedondeado2.GrosorBorde = 0F;
            panelRedondeado2.Location = new Point(42, 290);
            panelRedondeado2.Name = "panelRedondeado2";
            panelRedondeado2.Padding = new Padding(0);
            panelRedondeado2.Radio = 16;
            panelRedondeado2.Size = new Size(1174, 345);
            panelRedondeado2.TabIndex = 8;
            // 
            // dgvAlumnos
            // 
            dgvAlumnos.AllowUserToAddRows = false;
            dgvAlumnos.AllowUserToDeleteRows = false;
            dgvAlumnos.AllowUserToResizeRows = false;
            dgvAlumnos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAlumnos.BackgroundColor = Color.White;
            dgvAlumnos.BorderStyle = BorderStyle.None;
            dgvAlumnos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvAlumnos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(19, 49, 92);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.Padding = new Padding(12, 0, 0, 0);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(19, 49, 92);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvAlumnos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvAlumnos.ColumnHeadersHeight = 48;
            dgvAlumnos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvAlumnos.Columns.AddRange(new DataGridViewColumn[] { colAlumno, colMatricula, colClasificacion, colGrupo, colCelular, colVer });
            dgvAlumnos.Dock = DockStyle.Fill;
            dgvAlumnos.EnableHeadersVisualStyles = false;
            dgvAlumnos.GridColor = Color.FromArgb(220, 230, 239);
            dgvAlumnos.Location = new Point(0, 0);
            dgvAlumnos.MultiSelect = false;
            dgvAlumnos.Name = "dgvAlumnos";
            dgvAlumnos.Radio = 16;
            dgvAlumnos.ReadOnly = true;
            dgvAlumnos.RedondearAbajo = true;
            dgvAlumnos.RedondearArriba = true;
            dgvAlumnos.RowHeadersVisible = false;
            dgvAlumnos.RowHeadersWidth = 51;
            dgvAlumnos.RowTemplate.DefaultCellStyle.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvAlumnos.RowTemplate.DefaultCellStyle.ForeColor = Color.FromArgb(11, 37, 69);
            dgvAlumnos.RowTemplate.DefaultCellStyle.Padding = new Padding(12, 0, 0, 0);
            dgvAlumnos.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 230, 239);
            dgvAlumnos.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.FromArgb(11, 37, 69);
            dgvAlumnos.RowTemplate.Height = 56;
            dgvAlumnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAlumnos.Size = new Size(1174, 345);
            dgvAlumnos.TabIndex = 0;
            dgvAlumnos.CellContentClick += dgvAlumnos_CellContentClick;
            // 
            // colAlumno
            // 
            colAlumno.FillWeight = 200F;
            colAlumno.HeaderText = "Alumno";
            colAlumno.MinimumWidth = 6;
            colAlumno.Name = "colAlumno";
            colAlumno.ReadOnly = true;
            // 
            // colMatricula
            // 
            colMatricula.HeaderText = "Matrícula";
            colMatricula.MinimumWidth = 6;
            colMatricula.Name = "colMatricula";
            colMatricula.ReadOnly = true;
            // 
            // colClasificacion
            // 
            colClasificacion.HeaderText = "Clasificación";
            colClasificacion.MinimumWidth = 6;
            colClasificacion.Name = "colClasificacion";
            colClasificacion.ReadOnly = true;
            // 
            // colGrupo
            // 
            colGrupo.HeaderText = "Grupo";
            colGrupo.MinimumWidth = 6;
            colGrupo.Name = "colGrupo";
            colGrupo.ReadOnly = true;
            // 
            // colCelular
            // 
            colCelular.HeaderText = "Celular";
            colCelular.MinimumWidth = 6;
            colCelular.Name = "colCelular";
            colCelular.ReadOnly = true;
            // 
            // colVer
            // 
            colVer.ActiveLinkColor = Color.FromArgb(11, 37, 69);
            colVer.HeaderText = "Acciones";
            colVer.LinkBehavior = LinkBehavior.HoverUnderline;
            colVer.LinkColor = Color.FromArgb(19, 64, 116);
            colVer.MinimumWidth = 6;
            colVer.Name = "colVer";
            colVer.ReadOnly = true;
            colVer.Resizable = DataGridViewTriState.True;
            colVer.SortMode = DataGridViewColumnSortMode.Automatic;
            colVer.Text = "Ver ficha";
            colVer.TrackVisitedState = false;
            colVer.UseColumnTextForLinkValue = true;
            // 
            // Stu_StudentDirectory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(238, 244, 237);
            Controls.Add(panelRedondeado2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblCatAlu);
            Controls.Add(panelRedondeado1);
            Controls.Add(botonRedondeado1);
            Name = "Stu_StudentDirectory";
            Size = new Size(1280, 680);
            Load += Stu_StudentDirectory_Load;
            panelRedondeado1.ResumeLayout(false);
            panelRedondeado1.PerformLayout();
            panelRedondeado2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAlumnos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Controles.BotonRedondeado botonRedondeado1;
        private Controles.PanelRedondeado panelRedondeado1;
        private Label lblCatAlu;
        private Label label1;
        private Label label2;
        private Label label4;
        private Controles.CampoTexto campoTexto4;
        private Label label5;
        private Controles.CampoTexto campoTexto1;
        private Label label6;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Controles.PanelRedondeado panelRedondeado2;
        private CISOFTNEW.Controles.TablaRedondeada dgvAlumnos;
        private DataGridViewTextBoxColumn colAlumno;
        private DataGridViewTextBoxColumn colMatricula;
        private DataGridViewTextBoxColumn colClasificacion;
        private DataGridViewTextBoxColumn colGrupo;
        private DataGridViewTextBoxColumn colCelular;
        private DataGridViewLinkColumn colVer;
    }
}
