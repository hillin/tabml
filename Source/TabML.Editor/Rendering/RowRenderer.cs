using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    class RowRenderer : ElementRenderer
    {
        public List<BarRenderer> BarRenderers { get; }
        public bool IsFirstRow { get; set; }

        public RowRenderer(TablatureRenderer owner, bool isFirstRow)
            : base(owner)
        {
            this.BarRenderers = new List<BarRenderer>();
            this.IsFirstRow = isFirstRow;
        }


        public void Render(Point location, Size availableSize)
        {
            var tablatureRc = this.Root.RenderingContext.GetRenderingContext<TablatureRenderingContext>(this);
            var renderingContext = new RowRenderingContext(tablatureRc, location, availableSize);

            foreach (var barRenderer in this.BarRenderers)
            {
                barRenderer.RenderingContext = renderingContext;
            }

            var availableWidth = availableSize.Width;
            var count = this.BarRenderers.Count;
            var averageWidth = availableWidth / count;
            foreach (var bar in this.BarRenderers.OrderByDescending(b => b.MeasureMinSize()))
            {
                var minSize = bar.MeasureMinSize();
                if (minSize < averageWidth)
                    break;

                --count;
                availableWidth -= minSize;
                averageWidth = availableWidth / count;
            }

            foreach (var bar in this.BarRenderers)
            {
                var minSize = bar.MeasureMinSize();
                var width = Math.Min(availableSize.Width, Math.Max(minSize, averageWidth));
                var size = new Size(width, availableSize.Height);
                
                bar.Render(location, size);

                location.X += size.Width;
            }


            renderingContext.FinishHorizontalBarLines(availableSize.Width);
        }
    }
}
