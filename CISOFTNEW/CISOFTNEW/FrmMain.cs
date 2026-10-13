using System;
using System.Drawing;
using System.Windows.Forms;
using CISOFTDataOperations.Objects.Menu;
using CISOFTDataOperations.Security;

namespace CISOFTNEW
{
    public partial class FrmMain : Form
    {

        private UserSession _session;
        public FrmMain(UserSession session)
        {
            
            InitializeComponent();
            
            pnlContent.Controls.Add(tlpDashboard);
            tlpDashboard.Dock = DockStyle.Fill;
            tlpDashboard.BringToFront();

            _session = session;

            ConfigurarDashboard();

            ConfigurarPantallaCompleta();

            lblWelcome.Text = "Welcome " + _session.UserName;
        }

        private void ConfigurarPantallaCompleta()
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            StartPosition = FormStartPosition.Manual;
            WindowState = FormWindowState.Normal;

            Rectangle workingArea =
                Screen.PrimaryScreen.WorkingArea;

            Location = workingArea.Location;
            Size = workingArea.Size;
        }

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
            {
                MostrarControl(new Stu_StudentDirectory());
            };

            DashboardCard cardTeachers = CrearCard(
                "Teachers",
                "Teacher management",
                "",
                Resource.Resources.Teachers);

            DashboardCard cardBooks = CrearCard(
                "Books",
                "Book management",
                "",
                Resource.Resources.Books);

            DashboardCard cardLevels = CrearCard(
                "Levels",
                "Levels management",
                "",
                Resource.Resources.Levels);

            DashboardCard cardCourses = CrearCard(
                "Courses",
                "Course management",
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

            tlpDashboard.Controls.Add(cardCourses, 0, 1);
            tlpDashboard.Controls.Add(cardStages, 1, 1);
            tlpDashboard.Controls.Add(cardPayments, 2, 1);
            tlpDashboard.Controls.Add(cardReports, 3, 1);

            // Centrar las tarjetas dentro de cada celda
            cardStudents.Anchor = AnchorStyles.None;
            cardTeachers.Anchor = AnchorStyles.None;
            cardBooks.Anchor = AnchorStyles.None;
            cardLevels.Anchor = AnchorStyles.None;

            cardCourses.Anchor = AnchorStyles.None;
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
            pnlContent.Controls.Clear();

            control.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(control);
        }

        private void MostrarDashboard()
        {
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(tlpDashboard);
            tlpDashboard.Dock = DockStyle.Fill;
        }
    }
}