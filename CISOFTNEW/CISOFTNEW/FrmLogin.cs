using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CISOFTDataOperations.Security;

namespace CISOFTNEW
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            
            CentrarPanelLogin();

            RedondearPanel(pnlLogin, 20);

            ConfigurarPantallaCompleta();

            Resize +=
                delegate
                {
                    CentrarPanelLogin();
                };
        }


        private void ConfigurarPantallaCompleta()
        {
            Rectangle workingArea =
                Screen.PrimaryScreen.WorkingArea;

            Location = workingArea.Location;
            Size = workingArea.Size;
        }

        private void CentrarPanelLogin()
        {
            int x =
                (ClientSize.Width - pnlLogin.Width) / 2;

            int y =
                (ClientSize.Height - pnlLogin.Height) / 2;

            if (x < 0)
            {
                x = 0;
            }

            if (y < 0)
            {
                y = 0;
            }

            pnlLogin.Location =
                new Point(x, y);
        }

        private void btnLogin_Click(
        object sender,
        EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validar usuario y contraseña
            if (string.IsNullOrEmpty(user))
            {
                MessageBox.Show(
                    "Please enter your username.",
                    "Login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtUser.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Please enter your password.",
                    "Login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                //Autenticar usuario
                UserSession session = UserAuthentication.Authenticate(user, password);

                //Usuario o contraseña incorrectos
                if (session == null)
                {
                    MessageBox.Show(
                        "Invalid username or password.",
                        "Login",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtUser.Focus();
                    return;
                }

                //Crear formulario principal y pasar la sesión de usuario
                FrmMain mainForm = new FrmMain(session);

                /* 
                * Cuando FrmMain se cierre, volvemos
                * a mostrar el formulario de Login.
                */

                mainForm.FormClosed +=
                    delegate
                    {
                        Show();
                        txtPassword.Clear();
                        txtUser.Clear();
                        txtUser.Focus();
                    };

                // Ocultar el formulario de Login
                Hide();

                // Mostrar el formulario principal
                mainForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An error occurred while trying to login.",
                    "Login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnExit_Click(
        object sender,
        EventArgs e)
        {
            Close();
        }

        private void RedondearPanel(
        Panel panel,
        int radio)
        {
            GraphicsPath path = new GraphicsPath();

            int diametro = radio * 2;

            Rectangle rect =
                new Rectangle(
                    0,
                    0,
                    panel.Width,
                    panel.Height);

            // Esquina superior izquierda
            path.AddArc(
                rect.X,
                rect.Y,
                diametro,
                diametro,
                180,
                90);

            // Esquina superior derecha
            path.AddArc(
                rect.Right - diametro,
                rect.Y,
                diametro,
                diametro,
                270,
                90);

            // Esquina inferior derecha
            path.AddArc(
                rect.Right - diametro,
                rect.Bottom - diametro,
                diametro,
                diametro,
                0,
                90);

            // Esquina inferior izquierda
            path.AddArc(
                rect.X,
                rect.Bottom - diametro,
                diametro,
                diametro,
                90,
                90);

            path.CloseFigure();

            panel.Region = new Region(path);

            path.Dispose();
        }
    }
}
