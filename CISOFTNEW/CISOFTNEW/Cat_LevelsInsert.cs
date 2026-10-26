using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace CISOFTNEW
{
    public partial class Cat_LevelsInsert : UserControl
    {
        public Cat_LevelsInsert()
        {
            InitializeComponent();
            CargarLibros();
            RedondearPanel(pnlMain, 10, Color.FromArgb(220, 230, 238));
        }

        private void RedondearPanel(Panel panel, int radio, Color colorBorde)
        {
            Region regionActual = null;

            EventHandler actualizarForma = (sender, e) =>
            {
                Rectangle rect = panel.ClientRectangle;

                if (rect.Width <= 0 || rect.Height <= 0)
                    return;

                int r = Math.Min(radio, Math.Min(rect.Width, rect.Height) / 2);

                using (GraphicsPath path = CrearRectanguloRedondeado(rect, r))
                {
                    Region nuevaRegion = new Region(path);
                    panel.Region = nuevaRegion;

                    regionActual?.Dispose();
                    regionActual = nuevaRegion;
                }

                panel.Invalidate();
            };

            panel.Resize += actualizarForma;

            panel.Paint += (sender, e) =>
            {
                Rectangle rect = panel.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;

                if (rect.Width <= 0 || rect.Height <= 0)
                    return;

                int r = Math.Min(radio, Math.Min(rect.Width, rect.Height) / 2);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (GraphicsPath path = CrearRectanguloRedondeado(rect, r))
                using (Pen pen = new Pen(colorBorde, 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            };

            actualizarForma(panel, EventArgs.Empty);
        }

        private GraphicsPath CrearRectanguloRedondeado(Rectangle rect, int radio)
        {
            int diametro = radio * 2;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, diametro, diametro, 180, 90);
            path.AddArc(rect.Right - diametro, rect.Y, diametro, diametro, 270, 90);
            path.AddArc(rect.Right - diametro, rect.Bottom - diametro, diametro, diametro, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diametro, diametro, diametro, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void Cat_LevelsInsert_Load(object sender, EventArgs e)
        {
            txtNivel.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNivel.Text))
            {
                MessageBox.Show("Por favor, ingrese el nivel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AgregarNivel(sender, e);
        }

        private void AgregarNivel(object sender, EventArgs e)
        {
            string estado = cmbEstatus.Text.Trim().ToUpperInvariant();

            if (estado != "ACTIVO" && estado != "INACTIVO")
            {
                MessageBox.Show("Selecciona ACTIVO o INACTIVO.");
                return;
            }

            bool isActive = estado == "ACTIVO";

            try
            {
                CISOFTDataOperations.Database.DataOperations.ExecuteProcedure(CISOFTDataOperations.Database.Connection.NewCiSoft(),
                    "NewCiSoft_LevelsInsert", new SqlParameter[]
                    {
                new SqlParameter("@IsActive", SqlDbType.Int)
                    { Value = isActive },

                new SqlParameter("@LevelCode", SqlDbType.VarChar, 30)
                    { Value = txtNivel.Text.Trim() },

                new SqlParameter("@LevelDescription", SqlDbType.VarChar, 75)
                    { Value = txtNivelDescripcion.Text.Trim() },

                new SqlParameter("@BookId", SqlDbType.Int)
                {
                    Value = Convert.ToInt32(cmbBook.SelectedValue)
                }  });

                MessageBox.Show("Registro guardado correctamente.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (MessageBox.Show("¿Desea agregar más registros?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtNivel.Clear();
                    txtNivelDescripcion.Clear();
                    cmbEstatus.SelectedIndex = -1;
                    cmbBook.SelectedIndex = -1;
                    txtNivel.Focus();
                }
                else
                {
                    this.FindForm()?.Close();
                }
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error al guardar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarLibros()
        {
            const string sql = @"
        SELECT BookId, BookDescription
        FROM dbo.Books
        ORDER BY BookId;";

            DataSet ds = CISOFTDataOperations.Database.DataOperations.GetData(
                CISOFTDataOperations.Database.Connection.NewCiSoft(),
                sql);

            if (ds == null || ds.Tables.Count == 0)
                return;

            DataTable libros = ds.Tables[0];

            // Campo visible: combina ambas columnas para mostrar, por ejemplo:
            // "38 - Wordlink 1"
            if (!libros.Columns.Contains("LibroMostrar"))
                libros.Columns.Add("LibroMostrar", typeof(string));

            foreach (DataRow fila in libros.Rows)
            {
                fila["LibroMostrar"] =
                    $"{fila["BookId"]} - {fila["BookDescription"]}";
            }

            cmbBook.DataSource = libros;
            cmbBook.DisplayMember = "LibroMostrar";
            cmbBook.ValueMember = "BookId";
            cmbBook.SelectedIndex = -1;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close();
        }
    }
}
