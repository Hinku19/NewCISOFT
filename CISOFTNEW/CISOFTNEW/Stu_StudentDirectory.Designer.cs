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
            campoTexto4 = new CISOFTNEW.Controles.CampoTexto();
            label5 = new Label();
            campoTexto3 = new CISOFTNEW.Controles.CampoTexto();
            campoTexto2 = new CISOFTNEW.Controles.CampoTexto();
            label4 = new Label();
            label3 = new Label();
            lblCatAlu = new Label();
            label1 = new Label();
            label2 = new Label();
            btnSalir = new CISOFTNEW.Controles.BotonRedondeado();
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
            botonRedondeado1.Location = new Point(777, 47);
            botonRedondeado1.Margin = new Padding(3, 2, 3, 2);
            botonRedondeado1.Name = "botonRedondeado1";
            botonRedondeado1.Radio = 12;
            botonRedondeado1.Size = new Size(186, 45);
            botonRedondeado1.TabIndex = 1;
            botonRedondeado1.Text = " + Nuevo alumno";
            botonRedondeado1.UseVisualStyleBackColor = false;
            // 
            // campoTexto1
            // 
            campoTexto1.Font = new Font("Segoe UI", 11F);
            campoTexto1.ForeColor = Color.FromArgb(11, 37, 69);
            campoTexto1.Location = new Point(20, 28);
            campoTexto1.Margin = new Padding(3, 2, 3, 2);
            campoTexto1.MaxCaracteres = 32767;
            campoTexto1.Mayusculas = CharacterCasing.Normal;
            campoTexto1.Name = "campoTexto1";
            campoTexto1.Padding = new Padding(12, 0, 12, 0);
            campoTexto1.Placeholder = "Nombre, matrícula, CURP o RFC";
            campoTexto1.Radio = 10;
            campoTexto1.SangriaIzquierda = 12;
            campoTexto1.Size = new Size(369, 31);
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
            panelRedondeado1.Location = new Point(37, 115);
            panelRedondeado1.Margin = new Padding(3, 2, 3, 2);
            panelRedondeado1.Name = "panelRedondeado1";
            panelRedondeado1.Padding = new Padding(18, 15, 18, 15);
            panelRedondeado1.Radio = 16;
            panelRedondeado1.Size = new Size(1027, 76);
            panelRedondeado1.TabIndex = 3;
            // 
            // campoTexto4
            // 
            campoTexto4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            campoTexto4.ForeColor = Color.FromArgb(11, 37, 69);
            campoTexto4.Location = new Point(828, 28);
            campoTexto4.Margin = new Padding(3, 2, 3, 2);
            campoTexto4.MaxCaracteres = 32767;
            campoTexto4.Mayusculas = CharacterCasing.Normal;
            campoTexto4.Name = "campoTexto4";
            campoTexto4.Padding = new Padding(12, 0, 12, 0);
            campoTexto4.Placeholder = "";
            campoTexto4.Radio = 10;
            campoTexto4.SangriaIzquierda = 12;
            campoTexto4.Size = new Size(130, 31);
            campoTexto4.SoloLectura = false;
            campoTexto4.TabIndex = 13;
            campoTexto4.Texto = "Limpiar filtros";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(663, 8);
            label5.Name = "label5";
            label5.Size = new Size(107, 19);
            label5.TabIndex = 12;
            label5.Text = "Grupo asignado";
            // 
            // campoTexto3
            // 
            campoTexto3.Font = new Font("Segoe UI", 11F);
            campoTexto3.ForeColor = Color.FromArgb(11, 37, 69);
            campoTexto3.Location = new Point(663, 28);
            campoTexto3.Margin = new Padding(3, 2, 3, 2);
            campoTexto3.MaxCaracteres = 32767;
            campoTexto3.Mayusculas = CharacterCasing.Normal;
            campoTexto3.Name = "campoTexto3";
            campoTexto3.Padding = new Padding(12, 0, 12, 0);
            campoTexto3.Placeholder = "";
            campoTexto3.Radio = 10;
            campoTexto3.SangriaIzquierda = 12;
            campoTexto3.Size = new Size(140, 31);
            campoTexto3.SoloLectura = false;
            campoTexto3.TabIndex = 11;
            campoTexto3.Texto = "";
            // 
            // campoTexto2
            // 
            campoTexto2.Font = new Font("Segoe UI", 11F);
            campoTexto2.ForeColor = Color.FromArgb(11, 37, 69);
            campoTexto2.Location = new Point(405, 28);
            campoTexto2.Margin = new Padding(3, 2, 3, 2);
            campoTexto2.MaxCaracteres = 32767;
            campoTexto2.Mayusculas = CharacterCasing.Normal;
            campoTexto2.Name = "campoTexto2";
            campoTexto2.Padding = new Padding(12, 0, 12, 0);
            campoTexto2.Placeholder = "";
            campoTexto2.Radio = 10;
            campoTexto2.SangriaIzquierda = 12;
            campoTexto2.Size = new Size(237, 31);
            campoTexto2.SoloLectura = false;
            campoTexto2.TabIndex = 10;
            campoTexto2.Texto = "";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(405, 8);
            label4.Name = "label4";
            label4.Size = new Size(82, 19);
            label4.TabIndex = 9;
            label4.Text = "Clasificación";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(20, 8);
            label3.Name = "label3";
            label3.Size = new Size(99, 19);
            label3.TabIndex = 8;
            label3.Text = "Buscar alumno";
            // 
            // lblCatAlu
            // 
            lblCatAlu.AutoSize = true;
            lblCatAlu.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCatAlu.Location = new Point(37, 22);
            lblCatAlu.Name = "lblCatAlu";
            lblCatAlu.Size = new Size(155, 21);
            lblCatAlu.TabIndex = 5;
            lblCatAlu.Text = "Catálogos / Alumnos";
            lblCatAlu.Click += lblCatAlu_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(37, 50);
            label1.Name = "label1";
            label1.Size = new Size(116, 32);
            label1.TabIndex = 6;
            label1.Text = "Alumnos";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(37, 81);
            label2.Name = "label2";
            label2.Size = new Size(423, 21);
            label2.TabIndex = 7;
            label2.Text = "Consulta, registra y actualiza la información de los alumnos.";
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
            btnSalir.Location = new Point(969, 47);
            btnSalir.Margin = new Padding(3, 2, 3, 2);
            btnSalir.Name = "btnSalir";
            btnSalir.Radio = 12;
            btnSalir.Size = new Size(95, 45);
            btnSalir.TabIndex = 8;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // Stu_StudentDirectory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnSalir);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblCatAlu);
            Controls.Add(panelRedondeado1);
            Controls.Add(botonRedondeado1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Stu_StudentDirectory";
            Size = new Size(1120, 510);
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
        private Controles.BotonRedondeado btnSalir;
    }
}
