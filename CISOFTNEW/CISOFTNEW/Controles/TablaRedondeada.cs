using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CISOFTNEW.Controles
{
    /// <summary>DataGridView con esquinas redondeadas (arriba, abajo o ambas).
    /// Colócala dentro de un PanelRedondeado con Padding 0 y el mismo Radio.</summary>
    public class TablaRedondeada : DataGridView
    {
        private int _radio = 16;
        private bool _redondearArriba = true;
        private bool _redondearAbajo = true;

        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Radio { get => _radio; set { _radio = Math.Max(0, value); ActualizarRegion(); } }

        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool RedondearArriba { get => _redondearArriba; set { _redondearArriba = value; ActualizarRegion(); } }

        /// <summary>Ponlo en False si debajo de la tabla hay otro control (por ejemplo, la barra de paginación).</summary>
        [Category("CISoft"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool RedondearAbajo { get => _redondearAbajo; set { _redondearAbajo = value; ActualizarRegion(); } }

        public TablaRedondeada()
        {
            DoubleBuffered = true; // evita parpadeo al desplazarse
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ActualizarRegion();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            ActualizarRegion();
        }

        private void ActualizarRegion()
        {
            if (Width <= 0 || Height <= 0) return;

            var r = new Rectangle(0, 0, Width, Height);
            int d = Math.Min(_radio * 2, Math.Min(r.Width, r.Height));

            using var ruta = new GraphicsPath();
            if (_redondearArriba && d > 0)
            {
                ruta.AddArc(r.X, r.Y, d, d, 180, 90);
                ruta.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            }
            else
            {
                ruta.AddLine(r.X, r.Y, r.Right, r.Y);
            }

            if (_redondearAbajo && d > 0)
            {
                ruta.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
                ruta.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            }
            else
            {
                ruta.AddLine(r.Right, r.Bottom, r.X, r.Bottom);
            }
            ruta.CloseFigure();

            var anterior = Region;
            Region = new Region(ruta);
            anterior?.Dispose();
        }
    }
}
