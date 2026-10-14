using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CISOFTDataOperations.Database;

namespace CISOFTNEW
{
    public partial class Tch_TeachersDirectory : UserControl
    {
        int? EmployeeId = null;

        public Tch_TeachersDirectory()
        {
            InitializeComponent();

            cmbMaestros.SelectionChangeCommitted += cmbMaestros_SelectionChangeCommitted;

            CargarMaestros();
        }

        public event EventHandler BackRequested;
        private void btnSalir_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }

        public void CargarMaestros()
        {
            string sql = "SELECT EmployeeId, LTRIM(RTRIM(ISNULL(FirstName,'') + ' ' + ISNULL(PaternalSurname,'') + ' ' + ISNULL(MaternalSurname,''))) AS FullName FROM Teachers WHERE IsActive = 1";

            DataSet ds = CISOFTDataOperations.Database.DataOperations.GetData(CISOFTDataOperations.Database.Connection.NewCiSoft(), sql);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                cmbMaestros.DataSource = null;
                EmployeeId = 0;
                return;
            }

            DataTable dt = ds.Tables[0];
            //Agregar la opción inicial
            DataRow newRow = dt.NewRow();
            newRow["EmployeeId"] = 0;
            newRow["FullName"] = "Seleccione un maestro";
            dt.Rows.InsertAt(newRow, 0);
            cmbMaestros.DataSource = dt;

            cmbMaestros.ValueMember = "EmployeeId";
            cmbMaestros.DisplayMember = "FullName";
            cmbMaestros.DataSource = dt;
            cmbMaestros.SelectedIndex = 0;
        }

        private void cmbMaestros_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!int.TryParse(cmbMaestros.SelectedValue?.ToString(), out int employeeId)
                || employeeId == 0)
            {
                LimpiarCamposMaestro();
                return;
            }

            CargarDatosMaestro(employeeId);
        }

        private void CargarDatosMaestro(int employeeId)
        {
            const string sql = @"SELECT T.FirstName, T.PaternalSurname, T.MaternalSurname, T.Sex, T.CURP, T.Email, T.PersonalPhone, T.EmergencyPhone, T.Address,
                               T.ExtNumber, T.IntNumber, SC.NombreDelAsentamiento AS 'Colonia', T.ZipCode, SM.Descripcion AS 'Municipio', T.BirthDate, T.IsActive, T.EmployeeId
                               FROM dbo.Teachers T CROSS APPLY (SELECT LTRIM(RTRIM(CONVERT(VARCHAR(50), T.Neighborhood))) AS NeighborhoodTrimmed) AS N,
	                           dbo.SAT_Colonia SC, dbo.SAT_Municipio SM, dbo.SAT_Estado SE
                               WHERE T.EmployeeId = @EmployeeId and CASE WHEN N.NeighborhoodTrimmed = '' THEN '' WHEN PATINDEX('%[^0]%', N.NeighborhoodTrimmed) = 0 THEN '0'
                               ELSE SUBSTRING(N.NeighborhoodTrimmed, PATINDEX('%[^0]%', N.NeighborhoodTrimmed), LEN(N.NeighborhoodTrimmed)) END = LTRIM(RTRIM(CONVERT(VARCHAR(50), SC.SATcColonia)))
                               and SC.SATcCodigoPostal = T.ZipCode and SM.SATcMunicipio = T.City and SM.SATcEstado = SE.SATcEstado and T.Estado = SE.SATcEstado";

            using (SqlConnection connection =
                   CISOFTDataOperations.Database.Connection.NewCiSoft())
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = employeeId;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        LimpiarCamposMaestro();
                        return;
                    }

                    txtEmployee.Texto = Convert.ToString(reader["EmployeeId"]);
                    txtFirsName.Texto = Convert.ToString(reader["FirstName"]);
                    txtPaternalName.Texto = Convert.ToString(reader["PaternalSurname"]);
                    txtMaternalName.Texto = Convert.ToString(reader["MaternalSurname"]);
                    txtSex.Texto = Convert.ToString(reader["Sex"]);
                    txtCURP.Texto = Convert.ToString(reader["CURP"]);
                    txtEmail.Texto = Convert.ToString(reader["Email"]);
                    txtPersonalPhone.Texto = Convert.ToString(reader["PersonalPhone"]);
                    txtEmergencyPhone.Texto = Convert.ToString(reader["EmergencyPhone"]);
                    txtAddress.Texto = Convert.ToString(reader["Address"]);
                    txtExtNumber.Texto = Convert.ToString(reader["ExtNumber"]);
                    txtIntNumber.Texto = Convert.ToString(reader["IntNumber"]);
                    txtNeighborhood.Texto = Convert.ToString(reader["Colonia"]);
                    txtZipCode.Texto = Convert.ToString(reader["ZipCode"]);
                    txtCity.Texto = Convert.ToString(reader["Municipio"]);
                    dtpBirthDate.Value = Convert.ToDateTime(reader["BirthDate"]);

                    bool isActive = reader["IsActive"] != DBNull.Value && Convert.ToBoolean(reader["IsActive"]);

                    lblActive.Visible = isActive;
                    lblInactive.Visible = !isActive;
                }
            }
        }

        private void LimpiarCamposMaestro()
        {
            txtEmployee.Texto = string.Empty;
            txtFirsName.Texto = string.Empty;
            txtPaternalName.Texto = string.Empty;
            txtMaternalName.Texto = string.Empty;
            txtSex.Texto = string.Empty;
            txtCURP.Texto = string.Empty;
            txtEmail.Texto = string.Empty;
            txtPersonalPhone.Texto = string.Empty;
            txtEmergencyPhone.Texto = string.Empty;
            txtAddress.Texto = string.Empty;
            txtExtNumber.Texto = string.Empty;
            txtIntNumber.Texto = string.Empty;
            txtNeighborhood.Texto = string.Empty;
            txtZipCode.Texto = string.Empty;
            txtCity.Texto = string.Empty;
            dtpBirthDate.Value = DateTime.Now;
            lblActive.Visible = false;
            lblInactive.Visible = false;

            cmbMaestros.SelectedIndex = 0;
            gbxPersonal.Enabled = false;
            gbxIsActive.Enabled = false;
            gbxContact.Enabled = false;
            gbxAddress.Enabled = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCamposMaestro();
        }
    }
}
