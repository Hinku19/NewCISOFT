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
