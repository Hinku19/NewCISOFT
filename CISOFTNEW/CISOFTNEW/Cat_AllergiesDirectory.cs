using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CISOFTNEW
{
    public partial class Cat_AllergiesDirectory : UserControl
    {
        private DataTable _todasLasAlergias;
        private int _paginaActual = 1;
        private const int RegistrosPorPagina = 7;
        private DataTable _alergiasFiltradas;
        public event EventHandler BackRequested;
        public Cat_AllergiesDirectory()
        {
            InitializeComponent();

            // Evita que las conexiones de eventos duplicadas desde el
            // diseñador y desde código causen doble ejecución.
            Load -= Cat_AllergiesDirectory_Load;
            Load += Cat_AllergiesDirectory_Load;

            dgvAlergias.DataBindingComplete -= dgvAlergias_DataBindingComplete;
            dgvAlergias.DataBindingComplete += dgvAlergias_DataBindingComplete;

            dgvAlergias.CellPainting -= dgvAlergias_CellPainting;
            dgvAlergias.CellPainting += dgvAlergias_CellPainting;

            dgvAlergias.CellClick -= dgvAlergias_CellClick;
            dgvAlergias.CellClick += dgvAlergias_CellClick;

            btnAnterior.Click -= btnAnterior_Click;
            btnAnterior.Click += btnAnterior_Click;

            btnSiguiente.Click -= btnSiguiente_Click;
            btnSiguiente.Click += btnSiguiente_Click;

            btnSalir.Click -= btnSalir_Click;
            btnSalir.Click += btnSalir_Click;
        }

        private void Cat_AllergiesDirectory_Load(object sender, EventArgs e)
        {
            if (cmbEstado.Items.Count > 0)
                cmbEstado.SelectedIndex = 0;
            CargarAlergias();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }

        private void CargarAlergias()
        {
            const string sql = @"
                SELECT
                    A.AllergiesId AS AllergyId,
                    A.AllergiesType AS Alergia,
                    A.AllergiesDescription AS Descripcion,
                    COUNT(S.StudentId) AS AlumnosAsociados,
                    A.IsActive
                FROM dbo.Allergies AS A
                LEFT JOIN dbo.Students AS S
                    ON S.AllergiesId = A.AllergiesId
                GROUP BY
                    A.AllergiesId,
                    A.AllergiesType,
                    A.AllergiesDescription,
                    A.IsActive
                ORDER BY A.AllergiesType;";

            try
            {
                DataSet ds = CISOFTDataOperations.Database.DataOperations.GetData(
                    CISOFTDataOperations.Database.Connection.NewCiSoft(),
                    sql);

                if (ds == null || ds.Tables.Count == 0)
                {
                    _todasLasAlergias = null;
                    _alergiasFiltradas = null;
                    MostrarPaginaActual();
                    return;
                }

                _todasLasAlergias = ds.Tables[0];
                _paginaActual = 1;

                //MostrarPaginaActual();
                AplicarFiltroAlergia();
            }
            catch (Exception ex)
            {
                _todasLasAlergias = null;
                _alergiasFiltradas = null;
                _paginaActual = 1;

                MostrarPaginaActual();

                MessageBox.Show(
                    "No se pudieron cargar las alergías: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void MostrarPaginaActual()
        {
            if (_alergiasFiltradas == null)
            {
                dgvAlergias.DataSource = null;
                PrepararColumnasVisuales();
                EstilizarDgvAlergias();
                ActualizarPiePagina(0, 0, 0);

                btnPaginaActual.Text = "0";
                btnAnterior.Enabled = false;
                btnSiguiente.Enabled = false;
                return;
            }

            int total = _alergiasFiltradas.Rows.Count;

            int totalPaginas = (int)Math.Ceiling(
                total / (double)RegistrosPorPagina);

            if (totalPaginas == 0)
                _paginaActual = 1;
            else
                _paginaActual = Math.Max(1, Math.Min(_paginaActual, totalPaginas));

            int inicioIndice = (_paginaActual - 1) * RegistrosPorPagina;
            int finIndice = Math.Min(
                inicioIndice + RegistrosPorPagina,
                total);

            DataTable pagina = _alergiasFiltradas.Clone();

            for (int i = inicioIndice; i < finIndice; i++)
                pagina.ImportRow(_alergiasFiltradas.Rows[i]);

            dgvAlergias.DataSource = pagina;

            PrepararColumnasVisuales();
            EstilizarDgvAlergias();

            btnPaginaActual.Text = totalPaginas == 0
                ? "0"
                : _paginaActual.ToString();

            int desde = total == 0 ? 0 : inicioIndice + 1;
            ActualizarPiePagina(desde, finIndice, total);

            btnAnterior.Enabled = _paginaActual > 1;
            btnSiguiente.Enabled = _paginaActual < totalPaginas;
        }

        private void PrepararColumnasVisuales()
        {
            if (dgvAlergias.Columns.Contains("AllergyId"))
            {
                dgvAlergias.Columns["AllergyId"].Visible = false;
            }

            // Se conserva como booleano para leerlo y actualizarlo,
            // pero se oculta el checkbox generado automáticamente.
            if (dgvAlergias.Columns.Contains("IsActive"))
            {
                dgvAlergias.Columns["IsActive"].Visible = false;
            }

            if (!dgvAlergias.Columns.Contains("Icono"))
            {
                dgvAlergias.Columns.Insert(
                    0,
                    new DataGridViewImageColumn
                    {
                        Name = "Icono",
                        HeaderText = "",
                        ImageLayout = DataGridViewImageCellLayout.Normal,
                        Width = 48,
                        ReadOnly = true,
                        SortMode = DataGridViewColumnSortMode.NotSortable
                    });
            }

            if (!dgvAlergias.Columns.Contains("EstadoBoton"))
            {
                dgvAlergias.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        Name = "EstadoBoton",
                        HeaderText = "Estado",
                        ReadOnly = true,
                        SortMode = DataGridViewColumnSortMode.NotSortable
                    });
            }

            if (dgvAlergias.Columns.Contains("Alergia"))
            {
                dgvAlergias.Columns["Alergia"].HeaderText = "Alergia";
            }

            if (dgvAlergias.Columns.Contains("Descripcion"))
            {
                dgvAlergias.Columns["Descripcion"].HeaderText = "Descripción";
            }

            if (dgvAlergias.Columns.Contains("AlumnosAsociados"))
            {
                dgvAlergias.Columns["AlumnosAsociados"].HeaderText =
                    "Alumnos asociados";
            }

            // Orden visible:
            // Icono, Estado, Alergia, Descripción, Alumnos asociados.
            if (dgvAlergias.Columns.Contains("Icono"))
                dgvAlergias.Columns["Icono"].DisplayIndex = 0;

            //if (dgvAlergias.Columns.Contains("EstadoBoton"))
            //    dgvAlergias.Columns["EstadoBoton"].DisplayIndex = 1;

            if (dgvAlergias.Columns.Contains("EstadoBoton"))
            {
                dgvAlergias.Columns["EstadoBoton"].DisplayIndex =
                    dgvAlergias.Columns.Count - 1;
            }
        }

        private void EstilizarDgvAlergias()
        {
            dgvAlergias.ReadOnly = true;
            dgvAlergias.AllowUserToAddRows = false;
            dgvAlergias.AllowUserToDeleteRows = false;
            dgvAlergias.AllowUserToResizeRows = false;
            dgvAlergias.RowHeadersVisible = false;
            dgvAlergias.MultiSelect = false;
            dgvAlergias.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvAlergias.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvAlergias.EnableHeadersVisualStyles = false;
            dgvAlergias.BackgroundColor = Color.White;
            dgvAlergias.BorderStyle = BorderStyle.None;
            dgvAlergias.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvAlergias.GridColor = dgvAlergias.BackgroundColor;
            dgvAlergias.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
            dgvAlergias.ColumnHeadersHeight = 46;
            dgvAlergias.RowTemplate.Height = 76;
            dgvAlergias.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvAlergias.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(19, 49, 84);
            dgvAlergias.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvAlergias.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgvAlergias.DefaultCellStyle.Font =
                new Font("Segoe UI", 10);
            dgvAlergias.DefaultCellStyle.ForeColor =
                Color.FromArgb(25, 48, 74);
            dgvAlergias.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(225, 237, 247);
            dgvAlergias.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(25, 48, 74);
            dgvAlergias.DefaultCellStyle.Padding =
                new Padding(10, 6, 10, 6);

            if (dgvAlergias.Columns.Contains("Icono"))
            {
                DataGridViewColumn icono = dgvAlergias.Columns["Icono"];
                icono.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                icono.Width = 54;
                icono.DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvAlergias.Columns.Contains("EstadoBoton"))
            {
                DataGridViewColumn estado =
                    dgvAlergias.Columns["EstadoBoton"];
                estado.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                estado.Width = 110;
                estado.DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvAlergias.Columns.Contains("Alergia"))
                dgvAlergias.Columns["Alergia"].FillWeight = 25;

            if (dgvAlergias.Columns.Contains("Descripcion"))
                dgvAlergias.Columns["Descripcion"].FillWeight = 45;

            if (dgvAlergias.Columns.Contains("AlumnosAsociados"))
                dgvAlergias.Columns["AlumnosAsociados"].FillWeight = 25;

            dgvAlergias.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvAlergias.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.None;
            dgvAlergias.GridColor = Color.White;
            dgvAlergias.ThemeStyle.GridColor = Color.White;

            foreach (DataGridViewColumn columna in dgvAlergias.Columns)
                columna.DividerWidth = 0;



            dgvAlergias.Refresh();
        }

        private void CargarIconosPagina()
        {
            if (!dgvAlergias.Columns.Contains("Icono") ||
                !dgvAlergias.Columns.Contains("Alergia"))
                return;

            foreach (DataGridViewRow fila in dgvAlergias.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                string alergia =
                    Convert.ToString(fila.Cells["Alergia"].Value);

                fila.Cells["Icono"].Value = ObtenerIcono(alergia);
            }
        }

        // El diseñador puede tener conectado este evento.
        // La preparación se repite después del enlace en MostrarPaginaActual.
        private void dgvAlergias_DataBindingComplete(
            object sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            PrepararColumnasVisuales();
        }

        private void ActualizarPiePagina(int desde, int hasta, int total)
        {
            lblResumen.Text = string.Format(
                "Mostrando {0}–{1} de {2} alergias",
                desde,
                hasta,
                total);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (_paginaActual > 1)
            {
                _paginaActual--;
                MostrarPaginaActual();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (_todasLasAlergias == null)
                return;

            int totalPaginas = (int)Math.Ceiling(
                _todasLasAlergias.Rows.Count /
                (double)RegistrosPorPagina);

            if (_paginaActual < totalPaginas)
            {
                _paginaActual++;
                MostrarPaginaActual();
            }
        }

        private Image ObtenerIcono(string alergia)
        {
            switch ((alergia ?? "").Trim().ToLowerInvariant())
            {
                case "aines":
                    return Resource.Resources.Alergias_Aines;
                case "alérgenos":
                    return Resource.Resources.Alergias_Alergenos;
                case "altramuces":
                    return Resource.Resources.Alergias_Altramuces;
                case "anestésicos":
                    return Resource.Resources.Alergias_Anestesicos;
                case "antibióticos":
                    return Resource.Resources.Alergias_Antibioticos;
                case "anticonvulsivos":
                    return Resource.Resources.Alergias_Anticonvulsivos;
                case "apio":
                    return Resource.Resources.Alergias_Apio;
                case "asma":
                    return Resource.Resources.Alergias_Asma;
                case "cacahuate":
                    return Resource.Resources.Alergias_Cacahuate;
                case "cereales con gluten":
                    return Resource.Resources.Alergias_Cereales_con_gluten;
                case "frutos de cáscara":
                    return Resource.Resources.Alergias_Frutos_de_cascara;
                case "huevo":
                    return Resource.Resources.Alergias_Huevo;
                case "lácteos":
                    return Resource.Resources.Alergias_Lacteos;
                case "legumbres":
                    return Resource.Resources.Alergias_Legumbres;
                case "maíz":
                    return Resource.Resources.Alergias_Maiz;
                case "mariscos":
                    return Resource.Resources.Alergias_Mariscos;
                case "medios de contraste":
                    return Resource.Resources.Alergias_Medios_de_contraste;
                case "moluscos":
                    return Resource.Resources.Alergias_Moluscos;
                case "mostaza":
                    return Resource.Resources.Alergias_Mostaza;
                case "pescado":
                    return Resource.Resources.Alergias_Pescado;
                case "picaduras":
                    return Resource.Resources.Alergias_Picaduras;
                case "quimioterapéticos":
                case "quimioterapéuticos":
                    return Resource.Resources.Alergias_Quimioterapeuticos;
                case "sésamo":
                    return Resource.Resources.Alergias_Sesamo;
                case "soya":
                    return Resource.Resources.Alergias_Soya;
                case "sulfitos":
                    return Resource.Resources.Alergias_Sulfitos;
                default:
                    return Resource.Resources.Alergias_Default;
            }
        }

        private void dgvAlergias_CellPainting(
            object sender,
            DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            bool esEstado =
                dgvAlergias.Columns.Contains("EstadoBoton") &&
                e.ColumnIndex == dgvAlergias.Columns["EstadoBoton"].Index;

            if (!esEstado)
            {
                // Pinta contenido y fondo, omitiendo los bordes de la celda.
                e.Paint(
                    e.CellBounds,
                    DataGridViewPaintParts.All &
                    ~DataGridViewPaintParts.Border);

                e.Handled = true;
                return;
            }

            // En la celda de estado pinta el fondo sin borde.
            e.Paint(
                e.CellBounds,
                DataGridViewPaintParts.Background);

            object valor = dgvAlergias.Rows[e.RowIndex]
                .Cells["IsActive"].Value;

            bool activo = valor != null &&
                          valor != DBNull.Value &&
                          Convert.ToBoolean(valor);

            string texto = activo ? "Activo" : "Inactivo";
            Color color = activo
                ? Color.FromArgb(22, 101, 52)
                : Color.FromArgb(185, 28, 28);
            Color fondo = activo
                ? Color.FromArgb(225, 245, 234)
                : Color.FromArgb(254, 226, 226);

            Rectangle pastilla = new Rectangle(
                e.CellBounds.X + (e.CellBounds.Width - 82) / 2,
                e.CellBounds.Y + (e.CellBounds.Height - 28) / 2,
                82,
                28);

            using (GraphicsPath ruta = CrearPastilla(pastilla, 14))
            using (SolidBrush pincelFondo = new SolidBrush(fondo))
            using (Pen borde = new Pen(color))
            using (SolidBrush pincelTexto = new SolidBrush(color))
            using (StringFormat formato = new StringFormat())
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(pincelFondo, ruta);
                e.Graphics.DrawPath(borde, ruta);

                formato.Alignment = StringAlignment.Center;
                formato.LineAlignment = StringAlignment.Center;

                e.Graphics.DrawString(
                    texto,
                    dgvAlergias.Font,
                    pincelTexto,
                    pastilla,
                    formato);
            }

            e.Handled = true;
        }

        private GraphicsPath CrearPastilla(Rectangle rectangulo, int radio)
        {
            int diametro = radio * 2;
            GraphicsPath ruta = new GraphicsPath();

            ruta.AddArc(
                rectangulo.X, rectangulo.Y,
                diametro, diametro, 180, 90);

            ruta.AddArc(
                rectangulo.Right - diametro, rectangulo.Y,
                diametro, diametro, 270, 90);

            ruta.AddArc(
                rectangulo.Right - diametro,
                rectangulo.Bottom - diametro,
                diametro, diametro, 0, 90);

            ruta.AddArc(
                rectangulo.X,
                rectangulo.Bottom - diametro,
                diametro, diametro, 90, 90);

            ruta.CloseFigure();
            return ruta;
        }

        private void dgvAlergias_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 ||
                !dgvAlergias.Columns.Contains("EstadoBoton") ||
                e.ColumnIndex != dgvAlergias.Columns["EstadoBoton"].Index)
                return;

            DataGridViewRow fila = dgvAlergias.Rows[e.RowIndex];

            int alumnosAsociados =
                Convert.ToInt32(fila.Cells["AlumnosAsociados"].Value);

            if (alumnosAsociados > 0)
            {
                MessageBox.Show(
                    "No se puede cambiar el estado de este registro ya que cuenta con alumnos asociados",
                    "Cambio de estado no permitido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int allergyId =
                Convert.ToInt32(fila.Cells["AllergyId"].Value);

            bool estadoActual =
                Convert.ToBoolean(fila.Cells["IsActive"].Value);

            bool nuevoEstado = !estadoActual;

            const string sql = @"
                UPDATE dbo.Allergies
                SET IsActive = @IsActive
                WHERE AllergiesId = @AllergiesId;";

            try
            {
                using (SqlConnection conexion =
                       CISOFTDataOperations.Database.Connection.NewCiSoft())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@IsActive", SqlDbType.Bit)
                        .Value = nuevoEstado;

                    comando.Parameters.Add("@AllergiesId", SqlDbType.Int)
                        .Value = allergyId;

                    if (conexion.State != ConnectionState.Open)
                        conexion.Open();

                    int afectados = comando.ExecuteNonQuery();

                    if (afectados == 0)
                    {
                        MessageBox.Show(
                            "No se encontró la alergía para actualizar.",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }

                DataRow[] registros = _todasLasAlergias.Select(
                    "AllergyId = " + allergyId);

                if (registros.Length > 0)
                    registros[0]["IsActive"] = nuevoEstado;

                AplicarFiltroAlergia();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo cambiar el estado: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvAlergias_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 ||
               !dgvAlergias.Columns.Contains("Icono") ||
               e.ColumnIndex != dgvAlergias.Columns["Icono"].Index)
                return;

            string alergia = Convert.ToString(
                dgvAlergias.Rows[e.RowIndex].Cells["Alergia"].Value);

            e.Value = ObtenerIcono(alergia);
            e.FormattingApplied = true;
        }

        private void txtBuscarAlergia_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltroAlergia();
        }

        private void AplicarFiltroAlergia()
        {
            if (_todasLasAlergias == null)
            {
                _alergiasFiltradas = null;
                MostrarPaginaActual();
                return;
            }

            string busqueda = (txtBuscarAlergia.Text ?? "").Trim();
            string estadoSeleccionado = (cmbEstado.Text ?? "TODOS")
                .Trim()
                .ToUpperInvariant();

            _alergiasFiltradas = _todasLasAlergias.Clone();

            foreach (DataRow fila in _todasLasAlergias.Rows)
            {
                string alergia = Convert.ToString(fila["Alergia"]) ?? "";
                string descripcion = Convert.ToString(fila["Descripcion"]) ?? "";

                bool coincideTexto =
                    string.IsNullOrWhiteSpace(busqueda) ||
                    alergia.IndexOf(busqueda, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    descripcion.IndexOf(busqueda, StringComparison.CurrentCultureIgnoreCase) >= 0;

                bool activo = fila["IsActive"] != DBNull.Value &&
                              Convert.ToBoolean(fila["IsActive"]);

                bool coincideEstado =
                    estadoSeleccionado == "TODOS" ||
                    (estadoSeleccionado == "ACTIVOS" && activo) ||
                    (estadoSeleccionado == "INACTIVOS" && !activo);

                if (coincideTexto && coincideEstado)
                    _alergiasFiltradas.ImportRow(fila);
            }

            _paginaActual = 1;
            MostrarPaginaActual();
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltroAlergia();
        }

        private void btnNuevaAlergia_Click(object sender, EventArgs e)
        {
            using (var ventana = new Form())
            {
                var pantalla = new Cat_AllergiesInsert
                {
                    Dock = DockStyle.Fill
                };

                ventana.Controls.Add(pantalla);
                ventana.FormBorderStyle = FormBorderStyle.None;
                ventana.ClientSize = new Size(636, 314);
                ventana.StartPosition = FormStartPosition.CenterParent;
                ventana.ShowInTaskbar = false;

                ventana.FormClosed += (s, args) => CargarAlergias();

                ventana.ShowDialog(this.FindForm());
            }
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Se limpiarán los filtros de búsqueda y estado, mostrando todas las alergías.", "Limpiar filtros", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            {
                txtBuscarAlergia.Clear();
                cmbEstado.SelectedIndex = 0;
                CargarAlergias();
            }
        }
    }
}
