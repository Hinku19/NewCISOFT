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
    public partial class Cat_LevelsDirectory : UserControl
    {
        private DataTable _todoslosNiveles;
        private int _paginaActual = 1;
        private const int RegistrosPorPagina = 7;
        private DataTable _nivelesFiltrados;
        public event EventHandler BackRequested;
        public Cat_LevelsDirectory()
        {
            InitializeComponent();

            RedondearPanel(pnlTabla, 10, Color.FromArgb(220, 230, 238));
            RedondearPanel(pnlPaginacion, 10, Color.FromArgb(220, 230, 238));
            RedondearPanel(tlpPaginacion, 10, Color.FromArgb(220, 230, 238));

            // Evita que las conexiones de eventos duplicadas desde el
            // diseñador y desde código causen doble ejecución.
            Load -= Cat_LevelsDirectory_Load;
            Load += Cat_LevelsDirectory_Load;

            dgvNiveles.DataBindingComplete -= dgvNiveles_DataBindingComplete;
            dgvNiveles.DataBindingComplete += dgvNiveles_DataBindingComplete;

            dgvNiveles.CellPainting -= dgvNiveles_CellPainting;
            dgvNiveles.CellPainting += dgvNiveles_CellPainting;

            dgvNiveles.CellClick -= dgvNiveles_CellClick;
            dgvNiveles.CellClick += dgvNiveles_CellClick;

            btnAnterior.Click -= btnAnterior_Click;
            btnAnterior.Click += btnAnterior_Click;

            btnSiguiente.Click -= btnSiguiente_Click;
            btnSiguiente.Click += btnSiguiente_Click;

            btnSalir.Click -= btnSalir_Click;
            btnSalir.Click += btnSalir_Click;
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }

        private void Cat_LevelsDirectory_Load(object sender, EventArgs e)
        {
            if (cmbEstado.Items.Count > 0)
                cmbEstado.SelectedIndex = 0;
            CargarNiveles();
        }

        private void CargarNiveles()
        {
            const string sql = @"
                SELECT
                    L.LevelId AS LevelId,
                    L.LevelCode AS Nivel,
                    L.LevelDescription AS Descripcion,
                    COUNT(G.LevelId) AS GruposAsociados,
                    L.IsActive
                FROM dbo.Levels AS L
                LEFT JOIN dbo.Groups AS G
                    ON G.LevelId = L.LevelId
                GROUP BY
                    L.LevelId,
                    L.LevelCode,
                    L.LevelDescription,
                    L.IsActive
                ORDER BY L.LevelCode;";

            try
            {
                DataSet ds = CISOFTDataOperations.Database.DataOperations.GetData(
                    CISOFTDataOperations.Database.Connection.NewCiSoft(),
                    sql);

                if (ds == null || ds.Tables.Count == 0)
                {
                    _todoslosNiveles = null;
                    _nivelesFiltrados = null;
                    MostrarPaginaActual();
                    return;
                }

                _todoslosNiveles = ds.Tables[0];
                _paginaActual = 1;

                //MostrarPaginaActual();
                AplicarFiltroNiveles();
            }
            catch (Exception ex)
            {
                _todoslosNiveles = null;
                _nivelesFiltrados = null;
                _paginaActual = 1;

                MostrarPaginaActual();

                MessageBox.Show(
                    "No se pudieron cargar los niveles: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void MostrarPaginaActual()
        {
            if (_nivelesFiltrados == null)
            {
                dgvNiveles.DataSource = null;
                PrepararColumnasVisuales();
                EstilizarDgvNiveles();
                ActualizarPiePagina(0, 0, 0);

                btnPaginaActual.Text = "0";
                btnAnterior.Enabled = false;
                btnSiguiente.Enabled = false;
                return;
            }

            int total = _nivelesFiltrados.Rows.Count;

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

            DataTable pagina = _nivelesFiltrados.Clone();

            for (int i = inicioIndice; i < finIndice; i++)
                pagina.ImportRow(_nivelesFiltrados.Rows[i]);

            dgvNiveles.DataSource = pagina;

            PrepararColumnasVisuales();
            EstilizarDgvNiveles();

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
            if (dgvNiveles.Columns.Contains("LevelId"))
            {
                dgvNiveles.Columns["LevelId"].Visible = false;
            }

            // Se conserva como booleano para leerlo y actualizarlo,
            // pero se oculta el checkbox generado automáticamente.
            if (dgvNiveles.Columns.Contains("IsActive"))
            {
                dgvNiveles.Columns["IsActive"].Visible = false;
            }

            if (!dgvNiveles.Columns.Contains("EstadoBoton"))
            {
                dgvNiveles.Columns.Add(
                    new DataGridViewTextBoxColumn
                    {
                        Name = "EstadoBoton",
                        HeaderText = "Estado",
                        ReadOnly = true,
                        SortMode = DataGridViewColumnSortMode.NotSortable
                    });
            }

            if (dgvNiveles.Columns.Contains("Nivel"))
            {
                dgvNiveles.Columns["Nivel"].HeaderText = "Nivel";
            }

            if (dgvNiveles.Columns.Contains("Descripcion"))
            {
                dgvNiveles.Columns["Descripcion"].HeaderText = "Descripción";
            }

            if (dgvNiveles.Columns.Contains("GruposAsociados"))
            {
                dgvNiveles.Columns["GruposAsociados"].HeaderText =
                    "Grupos asociados";
            }

            if (dgvNiveles.Columns.Contains("EstadoBoton"))
            {
                dgvNiveles.Columns["EstadoBoton"].DisplayIndex =
                    dgvNiveles.Columns.Count - 1;
            }
        }

        private void EstilizarDgvNiveles()
        {
            dgvNiveles.ReadOnly = true;
            dgvNiveles.AllowUserToAddRows = false;
            dgvNiveles.AllowUserToDeleteRows = false;
            dgvNiveles.AllowUserToResizeRows = false;
            dgvNiveles.RowHeadersVisible = false;
            dgvNiveles.MultiSelect = false;
            dgvNiveles.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvNiveles.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvNiveles.EnableHeadersVisualStyles = false;
            dgvNiveles.BackgroundColor = Color.White;
            dgvNiveles.BorderStyle = BorderStyle.None;
            dgvNiveles.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvNiveles.GridColor = dgvNiveles.BackgroundColor;
            dgvNiveles.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
            dgvNiveles.ColumnHeadersHeight = 46;
            dgvNiveles.RowTemplate.Height = 76;
            dgvNiveles.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvNiveles.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(19, 49, 84);
            dgvNiveles.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvNiveles.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgvNiveles.DefaultCellStyle.Font =
                new Font("Segoe UI", 10);
            dgvNiveles.DefaultCellStyle.ForeColor =
                Color.FromArgb(25, 48, 74);
            dgvNiveles.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(225, 237, 247);
            dgvNiveles.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(25, 48, 74);
            dgvNiveles.DefaultCellStyle.Padding =
                new Padding(10, 6, 10, 6);

            if (dgvNiveles.Columns.Contains("Icono"))
            {
                DataGridViewColumn icono = dgvNiveles.Columns["Icono"];
                icono.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                icono.Width = 54;
                icono.DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvNiveles.Columns.Contains("EstadoBoton"))
            {
                DataGridViewColumn estado =
                    dgvNiveles.Columns["EstadoBoton"];
                estado.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                estado.Width = 110;
                estado.DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvNiveles.Columns.Contains("Nivel"))
                dgvNiveles.Columns["Nivel"].FillWeight = 25;

            if (dgvNiveles.Columns.Contains("Descripcion"))
                dgvNiveles.Columns["Descripcion"].FillWeight = 45;

            if (dgvNiveles.Columns.Contains("GruposAsociados"))
                dgvNiveles.Columns["GruposAsociados"].FillWeight = 25;

            dgvNiveles.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvNiveles.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.None;
            dgvNiveles.GridColor = Color.White;
            dgvNiveles.ThemeStyle.GridColor = Color.White;

            foreach (DataGridViewColumn columna in dgvNiveles.Columns)
                columna.DividerWidth = 0;



            dgvNiveles.Refresh();
        }

        // El diseñador puede tener conectado este evento.
        // La preparación se repite después del enlace en MostrarPaginaActual.
        private void dgvNiveles_DataBindingComplete(
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
            if (_todoslosNiveles == null)
                return;

            int totalPaginas = (int)Math.Ceiling(
                _todoslosNiveles.Rows.Count /
                (double)RegistrosPorPagina);

            if (_paginaActual < totalPaginas)
            {
                _paginaActual++;
                MostrarPaginaActual();
            }
        }

        private void dgvNiveles_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            bool esEstado =
                dgvNiveles.Columns.Contains("EstadoBoton") &&
                e.ColumnIndex == dgvNiveles.Columns["EstadoBoton"].Index;

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

            object valor = dgvNiveles.Rows[e.RowIndex].Cells["IsActive"].Value;

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
                    dgvNiveles.Font,
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

        private void dgvNiveles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 ||
                !dgvNiveles.Columns.Contains("EstadoBoton") ||
                e.ColumnIndex != dgvNiveles.Columns["EstadoBoton"].Index)
                return;

            DataGridViewRow fila = dgvNiveles.Rows[e.RowIndex];

            int alumnosAsociados =
                Convert.ToInt32(fila.Cells["GruposAsociados"].Value);

            if (alumnosAsociados > 0)
            {
                MessageBox.Show(
                    "No se puede cambiar el estado de este registro ya que cuenta con grupos asociados",
                    "Cambio de estado no permitido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int levelId =
                Convert.ToInt32(fila.Cells["LevelId"].Value);

            bool estadoActual =
                Convert.ToBoolean(fila.Cells["IsActive"].Value);

            bool nuevoEstado = !estadoActual;

            const string sql = @"
                UPDATE dbo.Levels
                SET IsActive = @IsActive
                WHERE LevelId = @LevelId;";

            try
            {
                using (SqlConnection conexion =
                       CISOFTDataOperations.Database.Connection.NewCiSoft())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@IsActive", SqlDbType.Bit)
                        .Value = nuevoEstado;

                    comando.Parameters.Add("@LevelId", SqlDbType.Int)
                        .Value = levelId;

                    if (conexion.State != ConnectionState.Open)
                        conexion.Open();

                    int afectados = comando.ExecuteNonQuery();

                    if (afectados == 0)
                    {
                        MessageBox.Show(
                            "No se encontró el nivel para actualizar.",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }

                DataRow[] registros = _todoslosNiveles.Select(
                    "LevelId = " + levelId);

                if (registros.Length > 0)
                    registros[0]["IsActive"] = nuevoEstado;

                AplicarFiltroNiveles();
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

        }

        private void AplicarFiltroNiveles()
        {
            if (_todoslosNiveles == null)
            {
                _nivelesFiltrados = null;
                MostrarPaginaActual();
                return;
            }

            string busqueda = (txtBuscarNivel.Text ?? "").Trim();
            string estadoSeleccionado = (cmbEstado.Text ?? "TODOS")
                .Trim()
                .ToUpperInvariant();

            _nivelesFiltrados = _todoslosNiveles.Clone();

            foreach (DataRow fila in _todoslosNiveles.Rows)
            {
                string nivel = Convert.ToString(fila["Nivel"]) ?? "";
                string descripcion = Convert.ToString(fila["Descripcion"]) ?? "";

                bool coincideTexto =
                    string.IsNullOrWhiteSpace(busqueda) ||
                    nivel.IndexOf(busqueda, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                    descripcion.IndexOf(busqueda, StringComparison.CurrentCultureIgnoreCase) >= 0;

                bool activo = fila["IsActive"] != DBNull.Value &&
                              Convert.ToBoolean(fila["IsActive"]);

                bool coincideEstado =
                    estadoSeleccionado == "TODOS" ||
                    (estadoSeleccionado == "ACTIVOS" && activo) ||
                    (estadoSeleccionado == "INACTIVOS" && !activo);

                if (coincideTexto && coincideEstado)
                    _nivelesFiltrados.ImportRow(fila);
            }

            MostrarPaginaActual();
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltroNiveles();
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Se limpiarán los filtros de búsqueda y estado, mostrando todos los niveles.", "Limpiar filtros",
                MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            {
                txtBuscarNivel.Clear();
                cmbEstado.SelectedIndex = 0;
                CargarNiveles();
            }
        }

        private void txtBuscarNivel_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltroNiveles();
        }

        private void btnNuevoNivel_Click(object sender, EventArgs e)
        {
            using (var ventana = new Form())
            {
                var pantalla = new Cat_LevelsInsert
                {
                    Dock = DockStyle.Fill
                };

                ventana.Controls.Add(pantalla);
                ventana.FormBorderStyle = FormBorderStyle.None;
                ventana.ClientSize = new Size(636, 314);
                ventana.StartPosition = FormStartPosition.CenterParent;
                ventana.ShowInTaskbar = false;

                ventana.FormClosed += (s, args) => CargarNiveles();

                ventana.ShowDialog(this.FindForm());
            }
        }
    }
}
