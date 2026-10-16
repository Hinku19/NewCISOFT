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

        int EmployeeId;
        public Tch_TeachersDirectory()
        {
            InitializeComponent();

            dgvMaestros.SelectionChanged += dgvMaestros_SelectionChanged;

            ConfigurarDgvMaestros();
            CargarOpcionesSexo();
            CargarMaestros();
        }

        private void ConfigurarDgvMaestros()
        {
            dgvMaestros.AutoGenerateColumns = true;
            dgvMaestros.ReadOnly = true;
            dgvMaestros.AllowUserToAddRows = false;
            dgvMaestros.AllowUserToDeleteRows = false;
            dgvMaestros.AllowUserToResizeRows = false;
            dgvMaestros.MultiSelect = false;
            dgvMaestros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMaestros.RowHeadersVisible = false;
            dgvMaestros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMaestros.BackgroundColor = Color.White;
            dgvMaestros.BorderStyle = BorderStyle.None;
            dgvMaestros.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMaestros.GridColor = Color.FromArgb(220, 230, 238);
            dgvMaestros.ColumnHeadersVisible = false;

            dgvMaestros.DefaultCellStyle.SelectionBackColor = Color.FromArgb(218, 232, 246);
            dgvMaestros.DefaultCellStyle.SelectionForeColor = Color.FromArgb(11, 37, 68);
            dgvMaestros.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvMaestros.DefaultCellStyle.Padding = new Padding(10, 8, 6, 8);

            dgvMaestros.RowTemplate.Height = 58;
        }

        public event EventHandler? BackRequested;
        private void btnSalir_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }

        public void CargarMaestros()
        {
            const string sql = @"
        SELECT EmployeeId,
               LTRIM(RTRIM(
                   ISNULL(FirstName, '') + ' ' +
                   ISNULL(PaternalSurname, '') + ' ' +
                   ISNULL(MaternalSurname, '')
               )) AS FullName
        FROM dbo.Teachers
        WHERE IsActive = 1
        ORDER BY FirstName, PaternalSurname;";

            DataSet ds = CISOFTDataOperations.Database.DataOperations.GetData(
                CISOFTDataOperations.Database.Connection.NewCiSoft(),
                sql);

            if (ds == null || ds.Tables.Count == 0)
            {
                dgvMaestros.DataSource = null;
                EmployeeId = 0;
                return;
            }

            dgvMaestros.DataSource = ds.Tables[0];

            if (dgvMaestros.Columns.Contains("EmployeeId"))
                dgvMaestros.Columns["EmployeeId"].Visible = false;
        }

        private void dgvMaestros_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMaestros.CurrentRow == null ||
                dgvMaestros.CurrentRow.Cells["EmployeeId"].Value == null)
                return;

            if (!int.TryParse(
                dgvMaestros.CurrentRow.Cells["EmployeeId"].Value.ToString(),
                out int employeeId))
                return;

            EmployeeId = employeeId;
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

                    txtEmployeeId.Text = Convert.ToString(reader["EmployeeId"]);
                    txtFirstName.Text = Convert.ToString(reader["FirstName"]);
                    txtPaternalSurName.Text = Convert.ToString(reader["PaternalSurname"]);
                    txtMaternalSurName.Text = Convert.ToString(reader["MaternalSurname"]);
                    txtCURP.Text = Convert.ToString(reader["CURP"]);
                    txtEmail.Text = Convert.ToString(reader["Email"]);
                    txtPersonalPhone.Text = Convert.ToString(reader["PersonalPhone"]);
                    txtEmergencyPhone.Text = Convert.ToString(reader["EmergencyPhone"]);
                    txtAddress.Text = Convert.ToString(reader["Address"]);
                    txtExtNum.Text = Convert.ToString(reader["ExtNumber"]);
                    txtIntNum.Text = Convert.ToString(reader["IntNumber"]);
                    txtColonia.Text = Convert.ToString(reader["Colonia"]);
                    txtZipCode.Text = Convert.ToString(reader["ZipCode"]);
                    txtCity.Text = Convert.ToString(reader["Municipio"]);
                    dtpBirthdate.Value = Convert.ToDateTime(reader["BirthDate"]);

                    string sexo = Convert.ToString(reader["Sex"]).Trim().ToUpperInvariant();

                    cmbSex.SelectedIndex = sexo == "M" ? 0 : sexo == "F" ? 1 : -1;
                }
            }
        }

        private void LimpiarCamposMaestro()
        {
            txtEmployeeId.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtPaternalSurName.Text = string.Empty;
            txtMaternalSurName.Text = string.Empty;
            cmbSex.SelectedIndex = -1;
            txtCURP.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPersonalPhone.Text = string.Empty;
            txtEmergencyPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtExtNum.Text = string.Empty;
            txtIntNum.Text = string.Empty;
            txtColonia.Text = string.Empty;
            txtZipCode.Text = string.Empty;
            txtCity.Text = string.Empty;
            dtpBirthdate.Value = DateTime.Now;
            gbxPersonal.Enabled = false;
            gbxContacto.Enabled = false;
            gbxDireccion.Enabled = false;
        }

        private void CargarOpcionesSexo()
        {
            cmbSex.Items.Clear();
            cmbSex.Items.Add(new OpcionSexo { Codigo = "M", Texto = "Masculino" });
            cmbSex.Items.Add(new OpcionSexo { Codigo = "F", Texto = "Femenino" });

            cmbSex.DisplayMember = "Texto";
            cmbSex.ValueMember = "Codigo";
        }

        private class OpcionSexo
        {
            public string Codigo { get; set; }
            public string Texto { get; set; }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCamposMaestro();
        }
    }
}
