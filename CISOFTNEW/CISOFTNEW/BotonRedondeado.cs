using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CISOFTNEW.Controles
{
    /// <summary>Botón redondeado con hover, borde, ícono (Segoe MDL2) y anillo de foco para teclado.
    /// Sirve para: Nuevo alumno, Guardar, Cancelar, Limpiar filtros, Ver/Editar (solo ícono) y el menú lateral.</summary>
    public class BotonRedondeado : Button
    {
        private int _radio = 12;
        private Color _colorHover = Color.FromArgb(0x0B, 0x25, 0x45);
        private Color _colorTextoHover = Color.Empty;
        private Color _colorBorde = Color.Transparent;
        private float _grosorBorde = 0f;
        private string _icono = "";
        private bool _encima;

        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public int Radio { get => _radio; set { _radio = value; Invalidate(); } }
        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public Color ColorHover { get => _colorHover; set { _colorHover = value; Invalidate(); } }
        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public Color ColorTextoHover { get => _colorTextoHover; set { _colorTextoHover = value; Invalidate(); } }
        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public Color ColorBorde { get => _colorBorde; set { _colorBorde = value; Invalidate(); } }
        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public float GrosorBorde { get => _grosorBorde; set { _grosorBorde = value; Invalidate(); } }

        /// <summary>Código del ícono en Segoe MDL2 Assets, p. ej. E710 (+), E721 (lupa), E70F (lápiz), E890 (ojo).</summary>
        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public string Icono { get => _icono; set { _icono = value ?? ""; Invalidate(); } }

        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public HorizontalAlignment Alineacion { get; set; } = HorizontalAlignment.Center;

        public BotonRedondeado()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Color.FromArgb(0x13, 0x40, 0x74);
            ForeColor = Color.White;
            Cursor = Cursors.Hand;
            Size = new Size(170, 48);
            Font = new Font("Segoe UI Semibold", 11f);
        }

        protected override void OnMouseEnter(System.EventArgs e) { _encima = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(System.EventArgs e) { _encima = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnGotFocus(System.EventArgs e) { Invalidate(); base.OnGotFocus(e); }
        protected override void OnLostFocus(System.EventArgs e) { Invalidate(); base.OnLostFocus(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Parent?.BackColor ?? SystemColors.Control);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            bool hover = _encima && Enabled;
            Color fondo = hover ? _colorHover : BackColor;
            Color texto = hover && !_colorTextoHover.IsEmpty ? _colorTextoHover : ForeColor;
            if (!Enabled) { fondo = Color.FromArgb(115, fondo); texto = Color.FromArgb(115, texto); }

            // Deja 3 px de margen para dibujar el anillo de foco sin cortarse
            var r = new RectangleF(3, 3, Width - 7, Height - 7);
            using (var ruta = Formas.Redondeado(r, _radio))
            {
                if (fondo.A > 0) using (var b = new SolidBrush(fondo)) g.FillPath(b, ruta);
                if (_grosorBorde > 0 && _colorBorde.A > 0)
                    using (var p = new Pen(_colorBorde, _grosorBorde)) g.DrawPath(p, ruta);
            }

            if (Focused && ShowFocusCues)
            {
                using var anillo = Formas.Redondeado(new RectangleF(1, 1, Width - 3, Height - 3), _radio + 2);
                using var p = new Pen(Color.FromArgb(0x13, 0x40, 0x74), 2.5f);
                g.DrawPath(p, anillo);
            }

            // Ícono + texto
            using var fuenteIcono = new Font("Segoe MDL2 Assets", Font.Size);
            string glifo = ConvertirIcono(_icono);
            Size tamIcono = glifo.Length > 0 ? TextRenderer.MeasureText(g, glifo, fuenteIcono, Size.Empty, TextFormatFlags.NoPadding) : Size.Empty;
            Size tamTexto = Text.Length > 0 ? TextRenderer.MeasureText(g, Text, Font, Size.Empty, TextFormatFlags.NoPadding) : Size.Empty;
            int separacion = glifo.Length > 0 && Text.Length > 0 ? 8 : 0;
            int anchoTotal = tamIcono.Width + separacion + tamTexto.Width;

            int x = Alineacion switch
            {
                HorizontalAlignment.Left => Padding.Left + 14,
                HorizontalAlignment.Right => Width - Padding.Right - 14 - anchoTotal,
                _ => (Width - anchoTotal) / 2
            };

            if (glifo.Length > 0)
            {
                TextRenderer.DrawText(g, glifo, fuenteIcono, new Rectangle(x, 0, tamIcono.Width, Height), texto,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
                x += tamIcono.Width + separacion;
            }
            if (Text.Length > 0)
                TextRenderer.DrawText(g, Text, Font, new Rectangle(x, 0, tamTexto.Width + 2, Height), texto,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
        }

        private static string ConvertirIcono(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo)) return "";
            return int.TryParse(codigo.Trim(), System.Globalization.NumberStyles.HexNumber, null, out int n)
                ? char.ConvertFromUtf32(n) : codigo;
        }
    }
}
