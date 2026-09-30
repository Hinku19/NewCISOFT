using CISOFTNEW.Servicios;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;

namespace CISOFTNEW
{
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();

            // ---- Ventana ----
            Text = "CISoft · Alumnos";
            Size = new Size(1440, 900);
            MinimumSize = new Size(1100, 700);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(238, 244, 237);

            // ---- Servicios que pueden usar las pantallas (@inject) ----
            var servicios = new ServiceCollection();
            servicios.AddWindowsFormsBlazorWebView();
#if DEBUG
            // Permite abrir las herramientas de desarrollador (Ctrl + Shift + I) al depurar
            servicios.AddBlazorWebViewDeveloperTools();
#endif
            servicios.AddSingleton<ServicioAlumnos>();

            // ---- El "navegador" donde viven las pantallas HTML ----
            var blazor = new BlazorWebView
            {
                Dock = DockStyle.Fill,
                HostPage = @"wwwroot\index.html",
                StartPath = "/alumnos",
                Services = servicios.BuildServiceProvider()
            };
            blazor.RootComponents.Add<App>("#app");

            Controls.Add(blazor);
        }
    }
}
