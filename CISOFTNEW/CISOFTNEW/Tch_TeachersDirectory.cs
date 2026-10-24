using CISOFTDataOperations.Database;
using Guna.UI2.WinForms;
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
    public partial class Tch_TeachersDirectory : UserControl
    {

        int EmployeeId;
        public Tch_TeachersDirectory()
        {
            InitializeComponent();

            dgvMaestros.SelectionChanged += dgvMaestros_SelectionChanged;
            cmbSex.DropDownStyle = ComboBoxStyle.DropDownList;

            ConfigurarDgvMaestros();
            CargarOpcionesSexo();
            CargarMaestros();

            RedondearPanel(pnlTeacherList, 10, Color.FromArgb(220, 230, 238));
            RedondearPanel(pnlTeacherDetails, 10, Color.FromArgb(220, 230, 238));
            RedondearPanel(tlpTeacherDetails, 10, Color.FromArgb(220, 230, 238));
            RedondearPanel(pnlBottomAction, 10, Color.FromArgb(220, 230, 238));
            RedondearPanel(flpBottomAction, 10, Color.FromArgb(220, 230, 238));


            txtEmployeeId.KeyPress += SoloNumeros_KeyPress;
            txtZipCode.KeyPress += SoloNumeros_KeyPress;
            txtPersonalPhone.KeyPress += SoloNumeros_KeyPress;
            txtEmergencyPhone.KeyPress += SoloNumeros_KeyPress;

            txtFirstName.KeyPress += SoloLetras_KeyPress;
            txtPaternalSurName.KeyPress += SoloLetras_KeyPress;
            txtMaternalSurName.KeyPress += SoloLetras_KeyPress;
            txtBuscarMaestro.KeyPress += SoloLetras_KeyPress;
            txtCity.KeyPress += SoloLetras_KeyPress;

            txtBuscarMaestro.Focus();

        }

        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void SoloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled =
                !char.IsLetter(e.KeyChar) &&
                e.KeyChar != ' ' &&
                !char.IsControl(e.KeyChar);
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

        private DataTable _tablaMaestros;
        private DataView _vistaMaestros;

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

            _tablaMaestros = ds.Tables[0];
            _vistaMaestros = _tablaMaestros.DefaultView;

            dgvMaestros.DataSource = _vistaMaestros;

            //dgvMaestros.DataSource = ds.Tables[0];

            if (dgvMaestros.Columns.Contains("EmployeeId"))
                dgvMaestros.Columns["EmployeeId"].Visible = false;

            txtBuscarMaestro.Focus();
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
            btnGuardar.Enabled = false;

            btnEditar.Text = "Iniciar edición";

            CargarMaestros();
            txtBuscarMaestro.Focus();
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

            public override string ToString() => Texto;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCamposMaestro();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea actualizar la lista de maestros activos?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ActionPermit = 1;

                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@ActionPermit", SqlDbType.Int) { Value = ActionPermit }
                };

                DataSet ds =
                    CISOFTDataOperations.Database.DataOperations.ExecuteProcedure(
                        CISOFTDataOperations.Database.Connection.NewCiSoft(),
                        "NewCiSoft_EmployeeUpdate",
                        parametros);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow resultado = ds.Tables[0].Rows[0];

                    string mensaje = Convert.ToString(resultado["Message"]);
                    int insertados = Convert.ToInt32(resultado["InsertedRows"]);

                    MessageBox.Show($"{mensaje}\nMaestros insertados: {insertados}");
                }

                ActionPermit = 0;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int EditPermit;

            if (gbxContacto.Enabled && gbxDireccion.Enabled && gbxPersonal.Enabled)
            {
                EditPermit = 0;
                gbxContacto.Enabled = false;
                gbxDireccion.Enabled = false;
                gbxPersonal.Enabled = false;
                btnGuardar.Enabled = false;
                MessageBox.Show("La edición de registros ha sido deshabilitada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnEditar.Text = "Iniciar edición";
                return;
            }

            if (MessageBox.Show("¿Desea habilitar la edición de registros?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EditPermit = 1;
                gbxContacto.Enabled = true;
                gbxDireccion.Enabled = true;
                gbxPersonal.Enabled = true;
                btnGuardar.Enabled = true;
                btnEditar.Text = "Terminar edición";
                txtFirstName.Focus();
            }
        }

        private void txtBuscarMaestro_TextChanged(object sender, EventArgs e)
        {
            if (_vistaMaestros == null)
                return;

            string texto = txtBuscarMaestro.Text.Trim()
                .Replace("'", "''");

            _vistaMaestros.RowFilter = string.IsNullOrEmpty(texto)
                ? string.Empty
                : $"FullName LIKE '%{texto}%'";
        }

        private void btnBuscarColonia_Click(object sender, EventArgs e)
        {
            using (var ventana =
                   new FrmBuscarDireccion(TipoBusquedaDireccion.Colonia))
            {
                if (ventana.ShowDialog(this) == DialogResult.OK)
                {
                    txtColonia.Text = ventana.ColoniaSeleccionada;
                    txtZipCode.Text = ventana.CodigoPostalSeleccionado;
                }
            }
        }

        private void btnBuscarCP_Click(object sender, EventArgs e)
        {
            using (var ventana =
                   new FrmBuscarDireccion(TipoBusquedaDireccion.CodigoPostal))
            {
                if (ventana.ShowDialog(this) == DialogResult.OK)
                {
                    txtZipCode.Text = ventana.CodigoPostalSeleccionado;

                    // El código postal pudo cambiar; limpia la colonia anterior.
                    txtColonia.Clear();
                    txtColonia.Text = ventana.ColoniaSeleccionada;
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea guardar los cambios realizados?","Confirmación",MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (cmbSex.SelectedItem is not OpcionSexo opcion)
            {
                MessageBox.Show("Selecciona Masculino o Femenino.");
                return;
            }

            if (!decimal.TryParse(txtPersonalPhone.Text.Trim(), out decimal personalPhone))
            {
                MessageBox.Show("Escribe un teléfono personal válido.");
                return;
            }

            if (!int.TryParse(txtZipCode.Text.Trim(), out int zipCode))
            {
                MessageBox.Show("Escribe un código postal válido.");
                return;
            }

            // Es opcional: vacío se envía como NULL.
            object emergencyPhoneValue = DBNull.Value;
            string emergencyText = txtEmergencyPhone.Text.Trim();

            if (!string.IsNullOrWhiteSpace(emergencyText))
            {
                if (!decimal.TryParse(emergencyText, out decimal emergencyPhone))
                {
                    MessageBox.Show("El teléfono de emergencia debe ser numérico.");
                    return;
                }

                emergencyPhoneValue = emergencyPhone;
            }

            try
            {
                CISOFTDataOperations.Database.DataOperations.ExecuteProcedure(CISOFTDataOperations.Database.Connection.NewCiSoft(),
                    "NewCiSoft_DataEmployeeUpdate", new SqlParameter[]
                    {
                new SqlParameter("@EmployeeId", SqlDbType.Int)
                    { Value = EmployeeId },

                new SqlParameter("@FirstName", SqlDbType.VarChar, 25)
                    { Value = txtFirstName.Text.Trim() },

                new SqlParameter("@PaternalSurname", SqlDbType.VarChar, 35)
                    { Value = txtPaternalSurName.Text.Trim() },

                new SqlParameter("@MaternalSurname", SqlDbType.VarChar, 35)
                    { Value = txtMaternalSurName.Text.Trim() },

                new SqlParameter("@Sex", SqlDbType.Char, 1)
                    { Value = opcion.Codigo },

                new SqlParameter("@CURP", SqlDbType.VarChar, 18)
                    { Value = txtCURP.Text.Trim() },

                new SqlParameter("@Email", SqlDbType.VarChar, 35)
                    { Value = txtEmail.Text.Trim() },

                new SqlParameter("@PersonalPhone", SqlDbType.Decimal)
                    { Precision = 10, Scale = 0, Value = personalPhone },

                new SqlParameter("@EmergencyPhone", SqlDbType.Decimal)
                    { Precision = 10, Scale = 0, Value = emergencyPhoneValue },

                new SqlParameter("@Address", SqlDbType.VarChar, 100)
                    { Value = txtAddress.Text.Trim() },

                new SqlParameter("@ExtNumber", SqlDbType.VarChar, 10)
                    { Value = txtExtNum.Text.Trim() },

                new SqlParameter("@IntNumber", SqlDbType.VarChar, 10)
                    { Value = txtIntNum.Text.Trim() },

                new SqlParameter("@Neighborhood", SqlDbType.VarChar, 20)
                    { Value = txtColonia.Text.Trim() },

                new SqlParameter("@ZipCode", SqlDbType.Int)
                    { Value = zipCode },

                new SqlParameter("@City", SqlDbType.VarChar, 15)
                    { Value = txtCity.Text.Trim() },

                new SqlParameter("@BirthDate", SqlDbType.Date)
                    { Value = dtpBirthdate.Value.Date }
                    });

                MessageBox.Show("Datos actualizados correctamente.","Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(MessageBox.Show("¿Desea continuar con la edición de registros?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    gbxContacto.Enabled = false;
                    gbxDireccion.Enabled = false;
                    gbxPersonal.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnEditar.Text = "Iniciar edición";
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
    }
}
