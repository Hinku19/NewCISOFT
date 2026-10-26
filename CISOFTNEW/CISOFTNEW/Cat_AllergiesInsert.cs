using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace CISOFTNEW
{
    public partial class Cat_AllergiesInsert : UserControl
    {
        public Cat_AllergiesInsert()
        {
            InitializeComponent();
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

        private void Cat_AllergiesInsert_Load(object sender, EventArgs e)
        {
            txtTipoAlergia.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTipoAlergia.Text))
            {
                MessageBox.Show("Por favor, ingrese el tipo de alergia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AgregaAlergia(sender, e);
        }
        private void AgregaAlergia(object sender, EventArgs e)
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
                    "NewCiSoft_AllergiesInsert", new SqlParameter[]
                    {
                new SqlParameter("@IsActive", SqlDbType.Int)
                    { Value = isActive },

                new SqlParameter("@AllergiesType", SqlDbType.VarChar, 25)
                    { Value = txtTipoAlergia.Text.Trim() },

                new SqlParameter("@AllergiesDescription", SqlDbType.VarChar, 35)
                    { Value = txtAlergiaDescripcion.Text.Trim() } });

                MessageBox.Show("Registro guardado correctamente.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (MessageBox.Show("¿Desea agregar más registros?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtTipoAlergia.Clear();
                    txtAlergiaDescripcion.Clear();
                    cmbEstatus.SelectedIndex = -1;
                    txtTipoAlergia.Focus();
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close();
        }
    }
}
