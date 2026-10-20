using System;
using System.Drawing;
using System.Windows.Forms;
using CISOFTDataOperations.Objects.Menu;
using CISOFTDataOperations.Security;
using CISOFTNEW.Controles;

namespace CISOFTNEW
{
    public partial class FrmMain : Form
    {

        private UserSession _session;

        // ---------- Barra lateral ----------
        private const int AnchoBarraAbierta = 220;
        private const int AnchoBarraContraida = 72;

        private bool _barraContraida;
        private bool _catalogosAbiertos;

        private Image? _logoCompleto;
        private Image? _logoEmblema;

        // Todos los botones del menú con su texto (al contraer se les quita el texto y queda solo el ícono)
        private readonly List<(BotonRedondeado Boton, string Texto)> _botonesMenu = new();
        private readonly List<Label> _titulosSeccion = new();
        private readonly List<BotonRedondeado> _botonesCatalogos = new();
        private readonly ToolTip _tooltipMenu = new ToolTip();

        private BotonRedondeado btnContraer = null!;
        private BotonRedondeado btnCatalogos = null!;
        private ContextMenuStrip menuCatalogos = null!;

        public FrmMain(UserSession session)
        {

            InitializeComponent();

            _session = session;

            CargarLogo();

            ConfigurarSidebar();

            AplicarEstadoBarra();

            ConfigurarPantallaCompleta();

            MostrarDashboard();
        }

        private void CargarLogo()
        {
            // Logo horizontal (barra abierta). La imagen original es enorme
            // (14221 x 3884 px, ~220 MB en memoria): se hace una copia pequeña y se libera la original.
            using (Bitmap original = Resource.Resources.EnglishCenter_Horizontal)
            {
                int ancho = 400;
                int alto = original.Height * ancho / original.Width;

                _logoCompleto = new Bitmap(original, ancho, alto);
            }

            // Emblema sin letras (barra contraída)
            using (Bitmap original = Resource.Resources.EnglishCenter_Emblema)
            {
                int alto = 120;
                int ancho = original.Width * alto / original.Height;

                Bitmap emblema = new Bitmap(original, ancho, alto);

                QuitarCuadritosDeFondo(emblema);

                _logoEmblema = emblema;
            }
        }

     //función que quita el fondo para añadir despues un fondo blanco 
        private static void QuitarCuadritosDeFondo(Bitmap imagen)
        {
            for (int y = 0; y < imagen.Height; y++)
            {
                for (int x = 0; x < imagen.Width; x++)
                {
                    Color c = imagen.GetPixel(x, y);

                    int mayor = Math.Max(c.R, Math.Max(c.G, c.B));
                    int menor = Math.Min(c.R, Math.Min(c.G, c.B));

                    // Gris claro: los tres canales parecidos y altos
                    if (mayor - menor < 24 && menor > 170)
                    {
                        imagen.SetPixel(x, y, Color.White);
                    }
                }
            }
        }

        private void ConfigurarSidebar()
        {
            flpMenu.Controls.Clear();

            btnContraer = CrearBotonMenu(
                "MENÚ",
                "E700");

            btnContraer.Click += (sender, e) =>
                AlternarBarra();

            flpMenu.Controls.Add(btnContraer);

            BotonRedondeado btnHome = CrearBotonMenu(
                "Inicio",
                "E80F");

            btnHome.Click += (sender, e) =>
                MostrarDashboard();

            flpMenu.Controls.Add(btnHome);

            // Académico
            flpMenu.Controls.Add(CrearTituloSeccion("ACADÉMICO"));

            BotonRedondeado btnStudents = CrearBotonMenu(
                "Alumnos",
                "E716");

            btnStudents.Click += (sender, e) =>
                AbrirStudents();

            BotonRedondeado btnTeachers = CrearBotonMenu(
                "Maestros",
                "E77B");

            btnTeachers.Click += (sender, e) =>
                AbrirTeachers();

            BotonRedondeado btnGroups = CrearBotonMenu(
                "Grupos",
                "E7BE");

            flpMenu.Controls.Add(btnStudents);
            flpMenu.Controls.Add(btnTeachers);
            flpMenu.Controls.Add(btnGroups);

            // Catálogos (desplegable)
            ConfigurarCatalogos();

            // Administración
            flpMenu.Controls.Add(CrearTituloSeccion("ADMINISTRACIÓN"));

            BotonRedondeado btnPayments = CrearBotonMenu(
                "Pagos",
                "E8C7");

            BotonRedondeado btnReports = CrearBotonMenu(
                "Reportes",
                "E9F9");

            flpMenu.Controls.Add(btnPayments);
            flpMenu.Controls.Add(btnReports);
        }

        private void ConfigurarCatalogos()
        {
            // Para agregar un catálogo nuevo basta con agregar una línea aquí:
            // no hay bronca si se respeta la estructura original
            var catalogos = new (string Texto, string Icono, Action? Abrir)[]
            {
                ("Niveles", "E9D2", null),
                // ("Etapas", "E7C1", null),
                ("Libros", "E8F1", AbrirBooks),
                ("Tipos de estudiante", "E8EC", AbrirTiposEstudiante),
                ("Tipos de sangre", "E95E", AbrirTiposSangre),
            };

            btnCatalogos = CrearBotonMenu(
                "Catálogos",
                "E8FD");

            btnCatalogos.Margin = new Padding(0, 10, 0, 2);

            btnCatalogos.Click += (sender, e) =>
                AlternarCatalogos();

            flpMenu.Controls.Add(btnCatalogos);

            // Menú flotante para cuando la barra está contraída
            menuCatalogos = new ContextMenuStrip();
            menuCatalogos.Renderer = new MenuFlotanteRenderer();
            menuCatalogos.ShowImageMargin = false;
            menuCatalogos.Font = new Font("Segoe UI", 10.5F);
            menuCatalogos.Padding = new Padding(0, 4, 0, 4);

            ToolStripLabel tituloFlotante = new ToolStripLabel("CATÁLOGOS");
            tituloFlotante.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            tituloFlotante.ForeColor = Color.FromArgb(141, 169, 196);
            tituloFlotante.Padding = new Padding(4, 2, 0, 4);

            menuCatalogos.Items.Add(tituloFlotante);

            foreach (var catalogo in catalogos)
            {
                // Botón dentro del desplegable
                BotonRedondeado boton = CrearBotonMenu(
                    catalogo.Texto,
                    catalogo.Icono);

                boton.Height = 38;
                boton.Padding = new Padding(18, 0, 0, 0);
                boton.Font = new Font("Segoe UI", 10.5F);
                boton.Visible = false;

                // Opción del menú flotante
                ToolStripMenuItem opcion = new ToolStripMenuItem(catalogo.Texto);
                opcion.Padding = new Padding(8, 6, 24, 6);

                if (catalogo.Abrir is not null)
                {
                    Action abrir = catalogo.Abrir;

                    boton.Click += (sender, e) => abrir();
                    opcion.Click += (sender, e) => abrir();
                }

                _botonesCatalogos.Add(boton);
                flpMenu.Controls.Add(boton);
                menuCatalogos.Items.Add(opcion);
            }
        }

        private void AlternarCatalogos()
        {
            if (_barraContraida)
            {
                // Barra contraída: el menú aparece flotando a la derecha del botón
                menuCatalogos.Show(
                    btnCatalogos,
                    new Point(btnCatalogos.Width + 6, 0));

                return;
            }

            _catalogosAbiertos = !_catalogosAbiertos;

            AplicarEstadoBarra();
        }

        private void AlternarBarra()
        {
            _barraContraida = !_barraContraida;

            AplicarEstadoBarra();
        }

        /// <summary>Acomoda toda la barra según esté abierta o contraída
        /// y según el desplegable de Catálogos esté abierto o cerrado.</summary>
        private void AplicarEstadoBarra()
        {
            bool contraida = _barraContraida;

            pnlSidebar.SuspendLayout();
            flpMenu.SuspendLayout();

            pnlSidebar.Width = contraida ? AnchoBarraContraida : AnchoBarraAbierta;

            // Logo: horizontal completo o solo el emblema
            picLogo.Image = contraida ? _logoEmblema : _logoCompleto;
            pnlLogo.Padding = contraida ? new Padding(8, 14, 8, 14) : new Padding(12, 14, 12, 14);
            pnlLogoFondo.Padding = contraida ? new Padding(4) : new Padding(10, 6, 10, 6);

            // Botones: con texto (abierta) o solo ícono con tooltip (contraída)
            foreach (var (boton, texto) in _botonesMenu)
            {
                boton.Text = contraida ? "" : texto;
                boton.Width = contraida ? AnchoBarraContraida - 16 : 200;
                boton.Alineacion = contraida ? HorizontalAlignment.Center : HorizontalAlignment.Left;

                _tooltipMenu.SetToolTip(boton, contraida ? texto : null);
            }

            _tooltipMenu.SetToolTip(btnContraer, contraida ? "Expandir menú" : null);

            // Títulos de sección: solo con la barra abierta
            foreach (Label titulo in _titulosSeccion)
            {
                titulo.Visible = !contraida;
            }

            // Desplegable de Catálogos: flecha ▾/▴ y sus opciones (solo con la barra abierta)
            btnCatalogos.IconoDerecho = contraida ? "" : (_catalogosAbiertos ? "E70E" : "E70D");

            foreach (BotonRedondeado boton in _botonesCatalogos)
            {
                boton.Visible = !contraida && _catalogosAbiertos;
            }

            // Usuario: nombre completo o solo iniciales
            lblUsuario.Text = contraida ? Iniciales(_session.UserName) : _session.UserName;
            lblUsuario.Padding = contraida ? new Padding(0) : new Padding(20, 0, 0, 0);
            lblUsuario.TextAlign = contraida ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft;
            _tooltipMenu.SetToolTip(lblUsuario, contraida ? _session.UserName : null);

            flpMenu.ResumeLayout();
            pnlSidebar.ResumeLayout();
        }

        private static string Iniciales(string? nombre)
        {
            string[] palabras = (nombre ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (palabras.Length == 0)
            {
                return "";
            }

            if (palabras.Length == 1)
            {
                return palabras[0].Substring(0, Math.Min(2, palabras[0].Length)).ToUpper();
            }

            return (palabras[0].Substring(0, 1) + palabras[1].Substring(0, 1)).ToUpper();
        }

        private BotonRedondeado CrearBotonMenu(
            string texto,
            string icono)
        {
            BotonRedondeado boton = new BotonRedondeado();

            boton.Text = texto;
            boton.Icono = icono;
            boton.Alineacion = HorizontalAlignment.Left;
            boton.Size = new Size(200, 42);
            boton.Margin = new Padding(0, 0, 0, 2);
            boton.Radio = 8;
            boton.BackColor = Color.FromArgb(11, 37, 69);
            boton.ColorHover = Color.FromArgb(19, 64, 116);
            boton.ForeColor = Color.FromArgb(238, 244, 237);
            boton.Font = new Font("Segoe UI", 11F);

            _botonesMenu.Add((boton, texto));

            return boton;
        }

        private Label CrearTituloSeccion(string texto)
        {
            Label titulo = new Label();

            titulo.Text = texto;
            titulo.AutoSize = false;
            titulo.Size = new Size(200, 30);
            titulo.Margin = new Padding(0, 10, 0, 0);
            titulo.Padding = new Padding(14, 0, 0, 0);
            titulo.TextAlign = ContentAlignment.BottomLeft;
            titulo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            titulo.ForeColor = Color.FromArgb(141, 169, 196);

            _titulosSeccion.Add(titulo);

            return titulo;
        }

        private void AbrirStudents()
        {
            // Catálogo de alumnos en Blazor (Pantallas/Alumnos.razor).
            // Desde ahí, Alumnos.razor navega solo a "nuevo" y "editar".
            MostrarControl(
                new PantallaBlazor("/alumnos"));
        }

        private void AbrirTiposEstudiante()
        {
            // Catálogo en Blazor (Pantallas/TiposEstudiante.razor)
            MostrarControl(
                new PantallaBlazor("/tipos-estudiante"));
        }

        private void AbrirTeachers()
        {
            var teachersControl = new Tch_TeachersDirectory();

            teachersControl.BackRequested += (s, args) =>
                MostrarDashboard();

            MostrarControl(teachersControl);
        }

        private void AbrirBooks()
        {
            // Catálogo en Blazor (Pantallas/Libros.razor).
            // La versión anterior en WinForms (Books.cs) ya no se usa.
            MostrarControl(
                new PantallaBlazor("/libros"));
        }

        private void AbrirTiposSangre()
        {
            // Catálogo en Blazor (Pantallas/TiposSangre.razor)
            MostrarControl(
                new PantallaBlazor("/tipos-sangre"));
        }

        private void ConfigurarPantallaCompleta()
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            StartPosition = FormStartPosition.Manual;
            WindowState = FormWindowState.Maximized;

            Rectangle workingArea =
                Screen.PrimaryScreen!.WorkingArea;

            Location = workingArea.Location;
            Size = workingArea.Size;
        }

        // Tablero anterior de 8 tarjetas. Ya no se usa (lo reemplazó Inicio);
        // se deja mientras el nuevo inicio termina de quedar.
        private void ConfigurarDashboard()
        {
            tlpDashboard.Controls.Clear();

            // 4 columnas x 2 filas
            tlpDashboard.ColumnCount = 4;
            tlpDashboard.RowCount = 2;

            tlpDashboard.Dock = DockStyle.Fill;

            tlpDashboard.ColumnStyles.Clear();
            tlpDashboard.RowStyles.Clear();

            // Columnas
            for (int i = 0; i < 4; i++)
            {
                tlpDashboard.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 50F));
            }

            // Filas
            for (int i = 0; i < 2; i++)
            {
                tlpDashboard.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 50F));
            }

            tlpDashboard.Padding = new Padding(40);

            // Crear tarjetas
            DashboardCard cardStudents = CrearCard(
                "Students",
                "Consult, register and keep track of the student section",
                "",
                Resource.Resources.Students);

            cardStudents.Click += (sender, e) =>
                AbrirStudents();

            DashboardCard cardTeachers = CrearCard(
                "Teachers",
                "Teacher management",
                "",
                Resource.Resources.Teachers);

            cardTeachers.Click += (sender, e) =>
                AbrirTeachers();

            DashboardCard cardBooks = CrearCard(
                "Books",
                "Book management",
                "",
                Resource.Resources.Books);

            cardBooks.Click += (sender, e) =>
                AbrirBooks();

            DashboardCard cardLevels = CrearCard(
                "Levels",
                "Levels management",
                "",
                Resource.Resources.Levels);

            DashboardCard cardGroups = CrearCard(
                "Groups",
                "Groups management",
                "",
                Resource.Resources.Courses);

            DashboardCard cardStages = CrearCard(
                "Stages",
                "Stage management",
                "",
                Resource.Resources.Stages);

            DashboardCard cardPayments = CrearCard(
                "Payments",
                "Payment management",
                "",
                Resource.Resources.Payment);

            DashboardCard cardReports = CrearCard(
                "Reports",
                "System reports",
                "",
                Resource.Resources.Reports);

            // Agregar tarjetas al TableLayoutPanel
            tlpDashboard.Controls.Add(cardStudents, 0, 0);
            tlpDashboard.Controls.Add(cardTeachers, 1, 0);
            tlpDashboard.Controls.Add(cardBooks, 2, 0);
            tlpDashboard.Controls.Add(cardLevels, 3, 0);

            tlpDashboard.Controls.Add(cardGroups, 0, 1);
            tlpDashboard.Controls.Add(cardStages, 1, 1);
            tlpDashboard.Controls.Add(cardPayments, 2, 1);
            tlpDashboard.Controls.Add(cardReports, 3, 1);

            // Centrar las tarjetas dentro de cada celda
            cardStudents.Anchor = AnchorStyles.None;
            cardTeachers.Anchor = AnchorStyles.None;
            cardBooks.Anchor = AnchorStyles.None;
            cardLevels.Anchor = AnchorStyles.None;

            cardGroups.Anchor = AnchorStyles.None;
            cardStages.Anchor = AnchorStyles.None;
            cardPayments.Anchor = AnchorStyles.None;
            cardReports.Anchor = AnchorStyles.None;
        }

        private DashboardCard CrearCard(
            string titulo,
            string descripcion,
            string valor,
            Image imagen)
        {
            DashboardCard card = new DashboardCard();

            card.CardTitle = titulo;
            card.CardDescription = descripcion;
            card.CardValue = valor;
            card.CardImage = imagen;

            return card;
        }

        private void MostrarControl(UserControl control)
        {
            // Libera la pantalla anterior (importante para las de Blazor,
            // que abren un navegador interno que se queda en memoria si no se libera)
            Control[] anteriores = new Control[pnlContent.Controls.Count];
            pnlContent.Controls.CopyTo(anteriores, 0);
            pnlContent.Controls.Clear();

            foreach (Control anterior in anteriores)
            {
                anterior.Dispose();
            }

            control.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(control);
        }

        private void MostrarDashboard()
        {
            MostrarControl(
                new Inicio(_session.UserName));
        }
    }
}