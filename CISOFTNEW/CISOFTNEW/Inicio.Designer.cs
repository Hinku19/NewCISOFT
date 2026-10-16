namespace CISOFTNEW
{
    partial class Inicio
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

        private void InitializeComponent()
        {
            tlpInicio = new TableLayoutPanel();
            SuspendLayout();
            //
            // tlpInicio
            // Contenedor principal: una fila por cada zona del inicio (se llenan en Inicio.cs)
            //
            tlpInicio.AutoSize = true;
            tlpInicio.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpInicio.BackColor = Color.Transparent;
            tlpInicio.ColumnCount = 1;
            tlpInicio.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpInicio.Dock = DockStyle.Top;
            tlpInicio.Location = new Point(32, 24);
            tlpInicio.Name = "tlpInicio";
            tlpInicio.RowCount = 5;
            tlpInicio.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));   // Encabezado
            tlpInicio.RowStyles.Add(new RowStyle(SizeType.Absolute, 110F));  // Indicadores
            tlpInicio.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));   // Título "Academic"
            tlpInicio.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));   // Tarjetas Academic
            tlpInicio.RowStyles.Add(new RowStyle(SizeType.Absolute, 240F));  // Catalogs + Administration
            tlpInicio.Size = new Size(1000, 566);
            tlpInicio.TabIndex = 0;
            //
            // Inicio
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(238, 244, 237);
            Controls.Add(tlpInicio);
            Name = "Inicio";
            Padding = new Padding(32, 24, 32, 24);
            Size = new Size(1064, 640);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tlpInicio;
    }
}
