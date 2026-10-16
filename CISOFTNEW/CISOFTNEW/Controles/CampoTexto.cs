using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CISOFTNEW.Controles
{
    /// <summary>Caja de texto con borde redondeado, placeholder y borde azul al tener el foco.
    /// Úsala para todos los campos del formulario y el buscador.</summary>
    [DefaultEvent("TextoCambiado")]
    public class CampoTexto : UserControl
    {
        private readonly TextBox _caja = new();
        private int _radio = 10;
        private bool _conFoco, _encima;

        private static readonly Color Borde = Color.FromArgb(0x6E, 0x87, 0xA3);
        private static readonly Color BordeFoco = Color.FromArgb(0x13, 0x40, 0x74);
        private static readonly Color BordeHover = Color.FromArgb(0x0B, 0x25, 0x45);
        private static readonly Color FondoSoloLectura = Color.FromArgb(0xEE, 0xF4, 0xED);

        public event EventHandler? TextoCambiado;

        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public string Texto { get => _caja.Text; set => _caja.Text = value; }
        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public string Placeholder { get => _caja.PlaceholderText; set => _caja.PlaceholderText = value; }
        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public int Radio { get => _radio; set { _radio = value; Invalidate(); } }
        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public int MaxCaracteres { get => _caja.MaxLength; set => _caja.MaxLength = value; }
        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public CharacterCasing Mayusculas { get => _caja.CharacterCasing; set => _caja.CharacterCasing = value; }

        /// <summary>Margen izquierdo del texto. Súbelo a 44 si pones un ícono (lupa) a la izquierda.</summary>
        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] public int SangriaIzquierda { get => Padding.Left; set { Padding = new Padding(value, 0, 14, 0); Reubicar(); } }

        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool SoloLectura
        {
            get => _caja.ReadOnly;
            set { _caja.ReadOnly = value; _caja.TabStop = !value; _caja.BackColor = value ? FondoSoloLectura : Color.White; Invalidate(); }
        }

        /// <summary>Acceso directo al TextBox interno (por si necesitas eventos extra).</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public TextBox Caja => _caja;

        public CampoTexto()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Size = new Size(260, 44);
            Font = new Font("Segoe UI", 11f);
            ForeColor = Color.FromArgb(0x0B, 0x25, 0x45);

            _caja.BorderStyle = BorderStyle.None;
            _caja.BackColor = Color.White;
            _caja.ForeColor = ForeColor;
            _caja.Font = Font;
            Controls.Add(_caja);

            _caja.Enter += (s, e) => { _conFoco = true; Invalidate(); };
            _caja.Leave += (s, e) => { _conFoco = false; Invalidate(); };
            _caja.MouseEnter += (s, e) => { _encima = true; Invalidate(); };
            _caja.MouseLeave += (s, e) => { _encima = false; Invalidate(); };
            _caja.TextChanged += (s, e) => TextoCambiado?.Invoke(this, e);
            Click += (s, e) => _caja.Focus();
            Reubicar();
        }

        protected override Padding DefaultPadding => new Padding(14, 0, 14, 0);

        protected override void OnMouseEnter(EventArgs e) { _encima = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _encima = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnResize(EventArgs e) { base.OnResize(e); Reubicar(); }
        protected override void OnFontChanged(EventArgs e) { base.OnFontChanged(e); _caja.Font = Font; Reubicar(); }
        protected override void OnForeColorChanged(EventArgs e) { base.OnForeColorChanged(e); _caja.ForeColor = ForeColor; }

        // Para que el lector de pantalla lea la etiqueta del campo
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!string.IsNullOrEmpty(AccessibleName)) _caja.AccessibleName = AccessibleName;
        }

        private void Reubicar()
        {
            _caja.Width = Math.Max(10, Width - Padding.Left - Padding.Right);
            _caja.Location = new Point(Padding.Left, (Height - _caja.PreferredHeight) / 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Formas.FondoDe(Parent));
            g.SmoothingMode = SmoothingMode.AntiAlias;

            float grosor = _conFoco ? 2.5f : 1.5f;
            Color color = _conFoco ? BordeFoco : _encima ? BordeHover : Borde;
            var r = new RectangleF(grosor / 2, grosor / 2, Width - grosor - 1, Height - grosor - 1);

            using var ruta = Formas.Redondeado(r, _radio);
            using (var fondo = new SolidBrush(_caja.BackColor)) g.FillPath(fondo, ruta);
            using var pluma = new Pen(color, grosor);
            if (SoloLectura) pluma.DashPattern = new[] { 4f, 3f };
            g.DrawPath(pluma, ruta);
        }
    }
}
