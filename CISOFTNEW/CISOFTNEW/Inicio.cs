using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using CISOFTNEW.Controles;

namespace CISOFTNEW
{
    /// <summary>Pantalla de inicio (dashboard) que se muestra dentro de FrmMain.
    /// Por ahora es solo visual: los indicadores muestran "—" y los clics no hacen nada.</summary>
    public partial class Inicio : UserControl
    {
        // Colores del diseño (inspo/socoada_home_redesign.html)
        private static readonly Color AzulOscuro = Color.FromArgb(11, 37, 69);       // #0B2545
        private static readonly Color AzulPrimario = Color.FromArgb(19, 64, 116);    // #134074
        private static readonly Color AzulSecundario = Color.FromArgb(19, 49, 92);   // #13315C
        private static readonly Color AzulGris = Color.FromArgb(141, 169, 196);      // #8DA9C4
        private static readonly Color AzulClaro = Color.FromArgb(220, 230, 239);     // #DCE6EF
        private static readonly Color FondoClaro = Color.FromArgb(238, 244, 237);    // #EEF4ED

        // Valores de los indicadores (se llenarán desde la base de datos)
        private Label lblActiveStudents = null!;
        private Label lblGroupsInProgress = null!;
        private Label lblPendingPayments = null!;

        public Inicio(string nombreUsuario)
        {
            InitializeComponent();

            tlpInicio.Controls.Add(
                CrearEncabezado(nombreUsuario),
                0,
                0);

            tlpInicio.Controls.Add(
                CrearIndicadores(),
                0,
                1);

            tlpInicio.Controls.Add(
                CrearTituloSeccion("Académico"),
                0,
                2);

            tlpInicio.Controls.Add(
                CrearAcademic(),
                0,
                3);

            tlpInicio.Controls.Add(
                CrearCatalogosYAdministracion(),
                0,
                4);
        }

        // ---------- Encabezado: fecha, saludo y accesos rápidos ----------

        private Control CrearEncabezado(string nombreUsuario)
        {
            TableLayoutPanel encabezado = CrearTabla(2);

            encabezado.ColumnStyles.Clear();
            encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            // Fecha + saludo
            Panel textos = new Panel();
            textos.Dock = DockStyle.Fill;
            textos.Margin = new Padding(0);

            Label lblSaludo = new Label();
            lblSaludo.Dock = DockStyle.Fill;
            lblSaludo.Text = ObtenerSaludo() + ", " + nombreUsuario;
            lblSaludo.Font = new Font("Segoe UI Semibold", 20F);
            lblSaludo.ForeColor = AzulOscuro;
            lblSaludo.TextAlign = ContentAlignment.TopLeft;

            Label lblFecha = new Label();
            lblFecha.Dock = DockStyle.Top;
            lblFecha.Height = 26;
            // Fecha en español, p. ej. "Jueves, 16 de octubre"
            string fecha = DateTime.Now.ToString(
                "dddd, d 'de' MMMM",
                new CultureInfo("es-MX"));
            lblFecha.Text = char.ToUpper(fecha[0]) + fecha.Substring(1);
            lblFecha.Font = new Font("Segoe UI", 11F);
            lblFecha.ForeColor = AzulPrimario;
            lblFecha.TextAlign = ContentAlignment.BottomLeft;

            // El de Dock Fill se agrega primero para que el de Dock Top quede arriba
            textos.Controls.Add(lblSaludo);
            textos.Controls.Add(lblFecha);

            // Botones de acceso rápido
            FlowLayoutPanel acciones = new FlowLayoutPanel();
            acciones.AutoSize = true;
            acciones.WrapContents = false;
            acciones.Anchor = AnchorStyles.Right;
            acciones.Margin = new Padding(0);

            BotonRedondeado btnNewStudent = new BotonRedondeado();
            btnNewStudent.Text = "Nuevo alumno";
            btnNewStudent.Icono = "E710";
            btnNewStudent.Size = new Size(170, 46);
            btnNewStudent.Radio = 10;
            btnNewStudent.BackColor = AzulPrimario;
            btnNewStudent.ColorHover = AzulOscuro;
            btnNewStudent.ForeColor = FondoClaro;
            btnNewStudent.Font = new Font("Segoe UI Semibold", 10.5F);

            BotonRedondeado btnRegisterPayment = new BotonRedondeado();
            btnRegisterPayment.Text = "Registrar pago";
            btnRegisterPayment.Icono = "E8C7";
            btnRegisterPayment.Size = new Size(200, 46);
            btnRegisterPayment.Radio = 10;
            btnRegisterPayment.BackColor = Color.White;
            btnRegisterPayment.ColorHover = AzulClaro;
            btnRegisterPayment.ForeColor = AzulPrimario;
            btnRegisterPayment.ColorBorde = AzulPrimario;
            btnRegisterPayment.GrosorBorde = 1F;
            btnRegisterPayment.Font = new Font("Segoe UI Semibold", 10.5F);

            acciones.Controls.Add(btnNewStudent);
            acciones.Controls.Add(btnRegisterPayment);

            encabezado.Controls.Add(textos, 0, 0);
            encabezado.Controls.Add(acciones, 1, 0);

            return encabezado;
        }

        private static string ObtenerSaludo()
        {
            int hora = DateTime.Now.Hour;

            if (hora < 12)
            {
                return "Buenos días";
            }

            if (hora < 19)
            {
                return "Buenas tardes";
            }

            return "Buenas noches";
        }

        // ---------- Indicadores ----------

        private Control CrearIndicadores()
        {
            TableLayoutPanel indicadores = CrearTabla(3);

            lblActiveStudents = CrearValorIndicador();
            lblGroupsInProgress = CrearValorIndicador();
            lblPendingPayments = CrearValorIndicador();

            indicadores.Controls.Add(
                CrearTarjetaIndicador("Alumnos activos", lblActiveStudents),
                0,
                0);

            indicadores.Controls.Add(
                CrearTarjetaIndicador("Grupos Activos", lblGroupsInProgress),
                1,
                0);

            indicadores.Controls.Add(
                CrearTarjetaIndicador("Pendientes de pago", lblPendingPayments),
                2,
                0);

            AjustarSeparacion(indicadores);

            return indicadores;
        }

        private Label CrearValorIndicador()
        {
            Label valor = new Label();

            valor.Dock = DockStyle.Fill;
            valor.Text = "—";
            valor.Font = new Font("Segoe UI Semibold", 24F);
            valor.ForeColor = AzulOscuro;
            valor.BackColor = Color.Transparent;
            valor.TextAlign = ContentAlignment.MiddleLeft;

            return valor;
        }

        private PanelRedondeado CrearTarjetaIndicador(
            string titulo,
            Label valor)
        {
            PanelRedondeado tarjeta = CrearTarjeta();

            Label lblTitulo = new Label();
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Height = 30;
            lblTitulo.Text = titulo;
            lblTitulo.Font = new Font("Segoe UI", 10.5F);
            lblTitulo.ForeColor = AzulPrimario;
            lblTitulo.BackColor = Color.Transparent;

            tarjeta.Controls.Add(valor);
            tarjeta.Controls.Add(lblTitulo);

            return tarjeta;
        }

        // ---------- Academic ----------

        private Control CrearAcademic()
        {
            TableLayoutPanel academic = CrearTabla(3);

            academic.Controls.Add(
                CrearTarjetaModulo("Alumnos", "Registro y seguimiento", "E716"),
                0,
                0);

            academic.Controls.Add(
                CrearTarjetaModulo("Maestros", "Personal y horarios", "E77B"),
                1,
                0);

            academic.Controls.Add(
                CrearTarjetaModulo("Grupos", "Grupos y clases", "E7BE"),
                2,
                0);

            AjustarSeparacion(academic);

            return academic;
        }

        private PanelRedondeado CrearTarjetaModulo(
            string titulo,
            string descripcion,
            string icono)
        {
            PanelRedondeado tarjeta = CrearTarjeta();
            tarjeta.Cursor = Cursors.Hand;

            // Cuadro azul claro con el ícono
            PanelRedondeado cuadroIcono = new PanelRedondeado();
            cuadroIcono.Size = new Size(44, 44);
            cuadroIcono.Location = new Point(18, 16);
            cuadroIcono.Radio = 10;
            cuadroIcono.Padding = new Padding(0);
            cuadroIcono.BackColor = AzulClaro;

            Label lblIcono = CrearIcono(icono, 18F, AzulPrimario);
            lblIcono.Dock = DockStyle.Fill;
            lblIcono.TextAlign = ContentAlignment.MiddleCenter;
            cuadroIcono.Controls.Add(lblIcono);

            Label lblTitulo = new Label();
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(76, 14);
            lblTitulo.Text = titulo;
            lblTitulo.Font = new Font("Segoe UI Semibold", 12F);
            lblTitulo.ForeColor = AzulOscuro;
            lblTitulo.BackColor = Color.Transparent;

            Label lblDescripcion = new Label();
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(77, 38);
            lblDescripcion.Text = descripcion;
            lblDescripcion.Font = new Font("Segoe UI", 10F);
            lblDescripcion.ForeColor = AzulSecundario;
            lblDescripcion.BackColor = Color.Transparent;

            tarjeta.Controls.Add(cuadroIcono);
            tarjeta.Controls.Add(lblTitulo);
            tarjeta.Controls.Add(lblDescripcion);

            return tarjeta;
        }

        // ---------- Catalogs + Administration ----------

        private Control CrearCatalogosYAdministracion()
        {
            TableLayoutPanel inferior = CrearTabla(2);

            inferior.ColumnStyles.Clear();
            inferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            inferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));

            inferior.RowCount = 2;
            inferior.RowStyles.Clear();
            inferior.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            inferior.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            Control catalogos = CrearCatalogos();
            catalogos.Margin = new Padding(0, 0, 14, 0);

            inferior.Controls.Add(CrearTituloSeccion("Catálogos"), 0, 0);
            inferior.Controls.Add(CrearTituloSeccion("Administración"), 1, 0);
            inferior.Controls.Add(catalogos, 0, 1);
            inferior.Controls.Add(CrearAdministracion(), 1, 1);

            return inferior;
        }

        private Control CrearCatalogos()
        {
            PanelRedondeado tarjeta = CrearTarjeta();
            tarjeta.Padding = new Padding(18, 6, 18, 6);

            TableLayoutPanel lista = new TableLayoutPanel();
            lista.Dock = DockStyle.Fill;
            lista.BackColor = Color.Transparent;
            lista.ColumnCount = 1;
            lista.RowCount = 2;
            lista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            lista.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            lista.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            lista.Controls.Add(CrearFilaCatalogo("Niveles", "E9D2", true), 0, 0);
            // lista.Controls.Add(CrearFilaCatalogo("Etapas", "E7C1", true), 0, 1);
            lista.Controls.Add(CrearFilaCatalogo("Libros", "E8F1", false), 0, 1);

            tarjeta.Controls.Add(lista);

            return tarjeta;
        }

        private Panel CrearFilaCatalogo(
            string texto,
            string icono,
            bool conSeparador)
        {
            Panel fila = new Panel();
            fila.Dock = DockStyle.Fill;
            fila.Margin = new Padding(0);
            fila.BackColor = Color.Transparent;
            fila.Cursor = Cursors.Hand;

            Label lblIcono = CrearIcono(icono, 14F, AzulPrimario);
            lblIcono.Dock = DockStyle.Left;
            lblIcono.Width = 36;
            lblIcono.TextAlign = ContentAlignment.MiddleLeft;

            Label lblFlecha = CrearIcono("E76C", 10F, AzulGris);
            lblFlecha.Dock = DockStyle.Right;
            lblFlecha.Width = 24;
            lblFlecha.TextAlign = ContentAlignment.MiddleRight;

            Label lblTexto = new Label();
            lblTexto.Dock = DockStyle.Fill;
            lblTexto.Text = texto;
            lblTexto.Font = new Font("Segoe UI", 11.5F);
            lblTexto.ForeColor = AzulOscuro;
            lblTexto.BackColor = Color.Transparent;
            lblTexto.TextAlign = ContentAlignment.MiddleLeft;

            // El de Dock Fill se agrega primero para que los de los lados se acomoden antes
            fila.Controls.Add(lblTexto);
            fila.Controls.Add(lblIcono);
            fila.Controls.Add(lblFlecha);

            if (conSeparador)
            {
                Panel separador = new Panel();
                separador.Dock = DockStyle.Bottom;
                separador.Height = 1;
                separador.BackColor = AzulClaro;

                fila.Controls.Add(separador);
            }

            return fila;
        }

        private Control CrearAdministracion()
        {
            TableLayoutPanel administracion = new TableLayoutPanel();
            administracion.Dock = DockStyle.Fill;
            administracion.Margin = new Padding(0);
            administracion.BackColor = Color.Transparent;
            administracion.ColumnCount = 1;
            administracion.RowCount = 3;
            administracion.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            administracion.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            administracion.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            administracion.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            administracion.Controls.Add(
                CrearBotonAdministracion("Pagos", "E8C7", AzulPrimario),
                0,
                0);

            administracion.Controls.Add(
                CrearBotonAdministracion("Reportes", "E9F9", AzulSecundario),
                0,
                1);

            return administracion;
        }

        private BotonRedondeado CrearBotonAdministracion(
            string texto,
            string icono,
            Color color)
        {
            BotonRedondeado boton = new BotonRedondeado();

            boton.Dock = DockStyle.Fill;
            boton.Margin = new Padding(0, 0, 0, 8);
            boton.Text = texto;
            boton.Icono = icono;
            boton.Alineacion = HorizontalAlignment.Left;
            boton.Radio = 12;
            boton.BackColor = color;
            boton.ColorHover = AzulOscuro;
            boton.ForeColor = FondoClaro;
            boton.Font = new Font("Segoe UI Semibold", 11.5F);

            return boton;
        }

        // ---------- Piezas reutilizables ----------

        /// <summary>Tabla de una fila con N columnas del mismo ancho.</summary>
        private static TableLayoutPanel CrearTabla(int columnas)
        {
            TableLayoutPanel tabla = new TableLayoutPanel();

            tabla.Dock = DockStyle.Fill;
            tabla.Margin = new Padding(0, 0, 0, 14);
            tabla.BackColor = Color.Transparent;
            tabla.ColumnCount = columnas;
            tabla.RowCount = 1;
            tabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            for (int i = 0; i < columnas; i++)
            {
                tabla.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 100F / columnas));
            }

            return tabla;
        }

        /// <summary>Deja un espacio entre tarjetas de la misma fila (la última no lleva).</summary>
        private static void AjustarSeparacion(TableLayoutPanel tabla)
        {
            foreach (Control control in tabla.Controls)
            {
                bool esUltima =
                    tabla.GetColumn(control) == tabla.ColumnCount - 1;

                control.Margin = esUltima
                    ? new Padding(0)
                    : new Padding(0, 0, 14, 0);
            }
        }

        private static PanelRedondeado CrearTarjeta()
        {
            PanelRedondeado tarjeta = new PanelRedondeado();

            tarjeta.Dock = DockStyle.Fill;
            tarjeta.Radio = 14;
            tarjeta.Padding = new Padding(18, 14, 18, 14);
            tarjeta.BackColor = Color.White;

            return tarjeta;
        }

        private static Label CrearTituloSeccion(string texto)
        {
            Label titulo = new Label();

            titulo.Dock = DockStyle.Fill;
            titulo.Margin = new Padding(0);
            titulo.Text = texto;
            titulo.Font = new Font("Segoe UI Semibold", 11F);
            titulo.ForeColor = AzulPrimario;
            titulo.BackColor = Color.Transparent;
            titulo.TextAlign = ContentAlignment.MiddleLeft;

            return titulo;
        }

        /// <summary>Label que muestra un ícono de la fuente Segoe MDL2 Assets (código en hexadecimal, p. ej. E716).</summary>
        private static Label CrearIcono(
            string codigo,
            float tamano,
            Color color)
        {
            Label icono = new Label();

            icono.Text = char.ConvertFromUtf32(
                int.Parse(codigo, NumberStyles.HexNumber));
            icono.Font = new Font("Segoe MDL2 Assets", tamano);
            icono.ForeColor = color;
            icono.BackColor = Color.Transparent;

            return icono;
        }
    }
}
