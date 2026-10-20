using System;
using System.Windows.Forms;
using CISOFTNEW.Servicios;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;

namespace CISOFTNEW
{
    /// <summary>Muestra pantallas Blazor (.razor) dentro de FrmMain, como cualquier otro UserControl.
    /// Recibe la ruta con la que empieza, p. ej. "/alumnos" o "/alumnos/nuevo".</summary>
    public class PantallaBlazor : UserControl
    {
        // Los servicios se crean una sola vez y se comparten entre todas las pantallas Blazor.
        // Así ServicioAlumnos conserva sus datos aunque se salga y se vuelva a entrar.
        private static readonly IServiceProvider Servicios =
            CrearServicios();

        public PantallaBlazor(string rutaInicial)
        {
            BlazorWebView blazor = new BlazorWebView();

            blazor.Dock = DockStyle.Fill;
            blazor.HostPage = @"wwwroot\index.html";
            blazor.StartPath = rutaInicial;
            blazor.Services = Servicios;
            blazor.RootComponents.Add<App>("#app");

            Controls.Add(blazor);
        }

        private static IServiceProvider CrearServicios()
        {
            ServiceCollection servicios = new ServiceCollection();

            servicios.AddWindowsFormsBlazorWebView();
#if DEBUG
            // Permite abrir las herramientas de desarrollador (Ctrl + Shift + I) al depurar
            servicios.AddBlazorWebViewDeveloperTools();
#endif
            // Servicios que pueden usar las pantallas (@inject)
            servicios.AddSingleton<ServicioAlumnos>();
            servicios.AddSingleton<ServicioTiposEstudiante>();
            servicios.AddSingleton<ServicioLibros>();
            servicios.AddSingleton<ServicioTiposSangre>();

            return servicios.BuildServiceProvider();
        }
    }
}
