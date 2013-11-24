using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System;

namespace CrystalLib.Toolset.Docking
{
    internal class VS2012LightSplitterControl : DockPane.SplitterControlBase
    {
        public VS2012LightSplitterControl(DockPane pane)
            : base(pane)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            try
            {
                using (Graphics g = e.Graphics)
                {
                    Rectangle rect = ClientRectangle;

                    if (rect.Width > 0 && rect.Height > 0)
                    {
                        if (Alignment == DockAlignment.Left || Alignment == DockAlignment.Right)
                        {
                            using (GraphicsPath path = new GraphicsPath())
                            {
                                path.AddRectangle(rect);
                                using (PathGradientBrush brush = new PathGradientBrush(path))
                                {
                                    brush.CenterColor = Color.FromArgb(0xFF, 204, 206, 219);
                                    //brush.SurroundColors = new[] { SystemColors.Control };

                                    g.FillRectangle(brush, rect.X + Measures.SplitterSize / 2 - 1, rect.Y, Measures.SplitterSize / 3, rect.Height);
                                }
                            }
                        }
                        else
                        {
                            if (Alignment == DockAlignment.Top || Alignment == DockAlignment.Bottom)
                            {
                                using (SolidBrush brush = new SolidBrush(Color.FromArgb(0xFF, 204, 206, 219)))
                                {
                                    g.FillRectangle(brush, rect.X, rect.Y, rect.Width, Measures.SplitterSize);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error rendering UI");
                return;
            }
        }
    }
}