using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CISOFTDataOperations.Database;

namespace CISOFTNEW
{
    public enum TipoBusquedaDireccion
    {
        Colonia,
        CodigoPostal
    }

    public partial class FrmBuscarDireccion : Form
    {
        private readonly TipoBusquedaDireccion _tipoBusqueda;

        public string ColoniaSeleccionada { get; private set; }
        public string CodigoPostalSeleccionado { get; private set; }

        // Constructor que usa el diseñador.
        public FrmBuscarDireccion()
            : this(TipoBusquedaDireccion.Colonia)
        {
        }

        // Constructor que usarán los botones.
        public FrmBuscarDireccion(TipoBusquedaDireccion tipoBusqueda)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;

            ConfigurarDataGrid();

            _tipoBusqueda = tipoBusqueda;

            Text = _tipoBusqueda == TipoBusquedaDireccion.Colonia
                ? "Buscar colonia"
                : "Buscar código postal";

            dgvResultados.AutoGenerateColumns = true;
            dgvResultados.ReadOnly = true;
            dgvResultados.MultiSelect = false;
            dgvResultados.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvResultados.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            txtBuscador.TextChanged += txtBuscador_TextChanged;
            tmrBuscar.Tick += tmrBuscar_Tick;
            btnSeleccionar.Click += btnSeleccionar_Click;
            btnCancelar.Click += btnCancelar_Click;
            dgvResultados.CellDoubleClick += dgvResultados_CellDoubleClick;
        }

        private void ConfigurarDataGrid ()
        {
            dgvResultados.ColumnHeadersVisible = true;
            dgvResultados.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvResultados.ColumnHeadersHeight = 36;
            dgvResultados.ThemeStyle.HeaderStyle.Height = 36;

            dgvResultados.EnableHeadersVisualStyles = false;
            dgvResultados.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(19, 64, 116);
            dgvResultados.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvResultados.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);
        }

        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {
            // Espera 300 ms desde la última tecla antes de consultar.
            tmrBuscar.Stop();
            tmrBuscar.Start();
        }

        private void tmrBuscar_Tick(object sender, EventArgs e)
        {
            tmrBuscar.Stop();
            CargarResultados(txtBuscador.Text.Trim());
        }

        private void CargarResultados(string filtro)
        {
            if (filtro.Length < 2)
            {
                dgvResultados.DataSource = null;
                return;
            }

            // Evita que una comilla ingresada rompa la consulta.
            string filtroSeguro = filtro.Replace("'", "''");

            string sql;

            if (_tipoBusqueda == TipoBusquedaDireccion.Colonia)
            {
                sql = $@"
            SELECT TOP (100)
                NombreDelAsentamiento AS Colonia,
                CONVERT(VARCHAR(10), SATcCodigoPostal) AS CodigoPostal
            FROM dbo.SAT_Colonia
            WHERE NombreDelAsentamiento LIKE N'{filtroSeguro}%'
               OR CONVERT(VARCHAR(10), SATcCodigoPostal)
                    LIKE N'{filtroSeguro}%'
            ORDER BY NombreDelAsentamiento;";
            }
            else
            {
                sql = $@"
            SELECT DISTINCT TOP (100)
                CONVERT(VARCHAR(10), SATcCodigoPostal) AS CodigoPostal
            FROM dbo.SAT_Colonia
            WHERE CONVERT(VARCHAR(10), SATcCodigoPostal)
                  LIKE N'{filtroSeguro}%'
            ORDER BY CodigoPostal;";
            }

            try
            {
                DataSet ds = CISOFTDataOperations.Database.DataOperations.GetData(CISOFTDataOperations.Database.Connection.NewCiSoft(), sql);

                if (ds != null && ds.Tables.Count > 0)
                    dgvResultados.DataSource = ds.Tables[0];
                else
                    dgvResultados.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al buscar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            SeleccionarFila();
        }

        private void dgvResultados_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                SeleccionarFila();
        }

        private void SeleccionarFila()
        {
            if (dgvResultados.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un resultado.");
                return;
            }

            DataGridViewRow fila = dgvResultados.CurrentRow;

            if (dgvResultados.Columns.Contains("Colonia"))
                ColoniaSeleccionada =
                    Convert.ToString(fila.Cells["Colonia"].Value);

            if (dgvResultados.Columns.Contains("CodigoPostal"))
                CodigoPostalSeleccionado =
                    Convert.ToString(fila.Cells["CodigoPostal"].Value);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
