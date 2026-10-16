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
        public FrmMain(UserSession session)
        {
            
            InitializeComponent();

            _session = session;

            CargarLogo();

            ConfigurarSidebar();

            ConfigurarPantallaCompleta();

            MostrarDashboard();

            lblUsuario.Text = _session.UserName;
        }

        private void CargarLogo()
        {
            // La imagen original es enorme (14221 x 3884 px, ~220 MB en memoria).
            // Se hace una copia pequeña para la barra lateral y se libera la original.
            using (Bitmap original = Resource.Resources.EnglishCenter_Horizontal)
            {
                int ancho = 400;
                int alto = original.Height * ancho / original.Width;

                picLogo.Image = new Bitmap(original, ancho, alto);
            }
        }

        private void ConfigurarSidebar()
        {
            flpMenu.Controls.Clear();

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

            // Catálogos
            flpMenu.Controls.Add(CrearTituloSeccion("CATÁLOGOS"));

            BotonRedondeado btnLevels = CrearBotonMenu(
                "Niveles",
                "E9D2");

            // BotonRedondeado btnStages = CrearBotonMenu(
            //     "Etapas",
            //     "E7C1");

            BotonRedondeado btnBooks = CrearBotonMenu(
                "Libros",
                "E8F1");

            btnBooks.Click += (sender, e) =>
                AbrirBooks();

            flpMenu.Controls.Add(btnLevels);
            // flpMenu.Controls.Add(btnStages);
            flpMenu.Controls.Add(btnBooks);

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

            return titulo;
        }

        private void AbrirStudents()
        {
            // Catálogo de alumnos en Blazor (Pantallas/Alumnos.razor).
            // Desde ahí, Alumnos.razor navega solo a "nuevo" y "editar".
            MostrarControl(
                new PantallaBlazor("/alumnos"));
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
            var booksControl = new Books();

            booksControl.BackRequested += (s, args) =>
                MostrarDashboard();

            MostrarControl(booksControl);
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