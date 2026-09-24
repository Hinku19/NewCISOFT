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
            botonRedondeado1 = new CISOFTNEW.Controles.BotonRedondeado();
            campoTexto1 = new CISOFTNEW.Controles.CampoTexto();
            panelRedondeado1 = new CISOFTNEW.Controles.PanelRedondeado();
            lblCatAlu = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            campoTexto2 = new CISOFTNEW.Controles.CampoTexto();
            campoTexto3 = new CISOFTNEW.Controles.CampoTexto();
            label5 = new Label();
            campoTexto4 = new CISOFTNEW.Controles.CampoTexto();
            panelRedondeado1.SuspendLayout();
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
            // campoTexto1
            // 
            campoTexto1.Font = new Font("Segoe UI", 11F);
            campoTexto1.ForeColor = Color.FromArgb(11, 37, 69);
            campoTexto1.Location = new Point(23, 37);
            campoTexto1.MaxCaracteres = 32767;
            campoTexto1.Mayusculas = CharacterCasing.Normal;
            campoTexto1.Name = "campoTexto1";
            campoTexto1.Padding = new Padding(14, 0, 14, 0);
            campoTexto1.Placeholder = "Nombre, matrícula, CURP o RFC";
            campoTexto1.Radio = 10;
            campoTexto1.SangriaIzquierda = 14;
            campoTexto1.Size = new Size(422, 41);
            campoTexto1.SoloLectura = false;
            campoTexto1.TabIndex = 2;
            campoTexto1.Texto = "";
            campoTexto1.TextoCambiado += campoTexto1_TextoCambiado;
            // 
            // panelRedondeado1
            // 
            panelRedondeado1.BackColor = Color.White;
            panelRedondeado1.BordePunteado = false;
            panelRedondeado1.ColorBorde = Color.Transparent;
            panelRedondeado1.Controls.Add(campoTexto4);
            panelRedondeado1.Controls.Add(label5);
            panelRedondeado1.Controls.Add(campoTexto3);
            panelRedondeado1.Controls.Add(campoTexto2);
            panelRedondeado1.Controls.Add(label4);
            panelRedondeado1.Controls.Add(label3);
            panelRedondeado1.Controls.Add(campoTexto1);
            panelRedondeado1.GrosorBorde = 0F;
            panelRedondeado1.Location = new Point(42, 153);
            panelRedondeado1.Name = "panelRedondeado1";
            panelRedondeado1.Padding = new Padding(20);
            panelRedondeado1.Radio = 16;
            panelRedondeado1.Size = new Size(1174, 101);
            panelRedondeado1.TabIndex = 3;
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
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(23, 11);
            label3.Name = "label3";
            label3.Size = new Size(123, 23);
            label3.TabIndex = 8;
            label3.Text = "Buscar alumno";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(463, 11);
            label4.Name = "label4";
            label4.Size = new Size(103, 23);
            label4.TabIndex = 9;
            label4.Text = "Clasificación";
            // 
            // campoTexto2
            // 
            campoTexto2.Font = new Font("Segoe UI", 11F);
            campoTexto2.ForeColor = Color.FromArgb(11, 37, 69);
            campoTexto2.Location = new Point(463, 37);
            campoTexto2.MaxCaracteres = 32767;
            campoTexto2.Mayusculas = CharacterCasing.Normal;
            campoTexto2.Name = "campoTexto2";
            campoTexto2.Padding = new Padding(14, 0, 14, 0);
            campoTexto2.Placeholder = "";
            campoTexto2.Radio = 10;
            campoTexto2.SangriaIzquierda = 14;
            campoTexto2.Size = new Size(271, 41);
            campoTexto2.SoloLectura = false;
            campoTexto2.TabIndex = 10;
            campoTexto2.Texto = "";
            // 
            // campoTexto3
            // 
            campoTexto3.Font = new Font("Segoe UI", 11F);
            campoTexto3.ForeColor = Color.FromArgb(11, 37, 69);
            campoTexto3.Location = new Point(758, 37);
            campoTexto3.MaxCaracteres = 32767;
            campoTexto3.Mayusculas = CharacterCasing.Normal;
            campoTexto3.Name = "campoTexto3";
            campoTexto3.Padding = new Padding(14, 0, 14, 0);
            campoTexto3.Placeholder = "";
            campoTexto3.Radio = 10;
            campoTexto3.SangriaIzquierda = 14;
            campoTexto3.Size = new Size(160, 41);
            campoTexto3.SoloLectura = false;
            campoTexto3.TabIndex = 11;
            campoTexto3.Texto = "";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(758, 11);
            label5.Name = "label5";
            label5.Size = new Size(132, 23);
            label5.TabIndex = 12;
            label5.Text = "Grupo asignado";
            // 
            // campoTexto4
            // 
            campoTexto4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            campoTexto4.ForeColor = Color.FromArgb(11, 37, 69);
            campoTexto4.Location = new Point(946, 37);
            campoTexto4.MaxCaracteres = 32767;
            campoTexto4.Mayusculas = CharacterCasing.Normal;
            campoTexto4.Name = "campoTexto4";
            campoTexto4.Padding = new Padding(14, 0, 14, 0);
            campoTexto4.Placeholder = "";
            campoTexto4.Radio = 10;
            campoTexto4.SangriaIzquierda = 14;
            campoTexto4.Size = new Size(148, 41);
            campoTexto4.SoloLectura = false;
            campoTexto4.TabIndex = 13;
            campoTexto4.Texto = "Limpiar filtros";
            // 
            // Stu_StudentDirectory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblCatAlu);
            Controls.Add(panelRedondeado1);
            Controls.Add(botonRedondeado1);
            Name = "Stu_StudentDirectory";
            Size = new Size(1280, 680);
            panelRedondeado1.ResumeLayout(false);
            panelRedondeado1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Controles.BotonRedondeado botonRedondeado1;
        private Controles.CampoTexto campoTexto1;
        private Controles.PanelRedondeado panelRedondeado1;
        private Label lblCatAlu;
        private Label label1;
        private Label label3;
        private Label label2;
        private Label label4;
        private Controles.CampoTexto campoTexto2;
        private Controles.CampoTexto campoTexto4;
        private Label label5;
        private Controles.CampoTexto campoTexto3;
    }
}
