using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CISOFTNEW.Controles
{
    /// <summary>Panel con esquinas redondeadas y borde opcional (sólido o punteado).
    /// Úsalo para las tarjetas blancas, la barra de filtros y el recuadro del logo.</summary>
    public class PanelRedondeado : Panel
    {
        private int _radio = 16;
        private Color _colorBorde = Color.Transparent;
        private float _grosorBorde = 0f;
        private bool _punteado = false;

        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public int Radio { get => _radio; set { _radio = value; Invalidate(); } }
        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public Color ColorBorde { get => _colorBorde; set { _colorBorde = value; Invalidate(); } }
        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public float GrosorBorde { get => _grosorBorde; set { _grosorBorde = value; Invalidate(); } }
        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public bool BordePunteado { get => _punteado; set { _punteado = value; Invalidate(); } }

        public PanelRedondeado()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Color.White;
        }

        // Relleno inicial de 20. Al declararlo así, el diseñador sí guarda
        // cualquier otro valor que pongas (incluido 0) y no se "regresa" al ejecutar.
        protected override Padding DefaultPadding => new Padding(20);

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Formas.FondoDe(Parent));
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var r = new RectangleF(_grosorBorde / 2f, _grosorBorde / 2f,
                                   Width - _grosorBorde - 1, Height - _grosorBorde - 1);
            using var ruta = Formas.Redondeado(r, _radio);
            using (var fondo = new SolidBrush(BackColor)) g.FillPath(fondo, ruta);

            if (_grosorBorde > 0 && _colorBorde.A > 0)
            {
                using var pluma = new Pen(_colorBorde, _grosorBorde);
                if (_punteado) pluma.DashPattern = new[] { 4f, 3f };
                g.DrawPath(pluma, ruta);
            }
        }
    }

    internal static class Formas
    {
        /// <summary>Busca el primer contenedor con color sólido (ignora los Transparent),
        /// para pintar correctamente el área fuera de las esquinas redondeadas.</summary>
        public static Color FondoDe(Control? c)
        {
            while (c != null && c.BackColor.A < 255) c = c.Parent;
            return c?.BackColor ?? SystemColors.Control;
        }

        public static GraphicsPath Redondeado(RectangleF r, float radio)
        {
            var p = new GraphicsPath();
            float d = System.Math.Min(radio * 2, System.Math.Min(r.Width, r.Height));
            if (d <= 0) { p.AddRectangle(r); return p; }
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }
    }
}
