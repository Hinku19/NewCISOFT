using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CISOFTNEW.Controles
{
    /// <summary>Pinta un ContextMenuStrip con los colores de la barra lateral
    /// (se usa en el menú flotante de Catálogos cuando la barra está contraída).</summary>
    public class MenuFlotanteRenderer : ToolStripProfessionalRenderer
    {
        private static readonly Color Fondo = Color.FromArgb(11, 37, 69);      // #0B2545
        private static readonly Color Hover = Color.FromArgb(19, 64, 116);     // #134074
        private static readonly Color Borde = Color.FromArgb(19, 49, 92);      // #13315C
        private static readonly Color Texto = Color.FromArgb(238, 244, 237);   // #EEF4ED

        public MenuFlotanteRenderer()
        {
            RoundedEdges = false;
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using var b = new SolidBrush(Fondo);
            e.Graphics.FillRectangle(b, e.AffectedBounds);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            using var p = new Pen(Borde);
            e.Graphics.DrawRectangle(p, 0, 0, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1);
        }

        // Sin la franja gris de la izquierda (donde van las imágenes)
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e) { }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (!e.Item.Selected) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var r = new RectangleF(4, 1, e.Item.Width - 8, e.Item.Height - 2);
            using var ruta = Formas.Redondeado(r, 6);
            using var b = new SolidBrush(Hover);
            e.Graphics.FillPath(b, ruta);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = Texto;
            base.OnRenderItemText(e);
        }
    }
}
