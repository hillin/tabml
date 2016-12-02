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
    class RowRenderer
    {
        public PrimitiveRenderer PrimitiveRenderer { get; }
        public TablatureStyle Style { get; }
        public List<BarRenderer> BarRenderers { get; }
        public bool IsFirstRow { get; set; }

        private RowRenderer(PrimitiveRenderer primitiveRenderer, TablatureStyle style)
        {
            this.PrimitiveRenderer = primitiveRenderer;
            this.Style = style;
            this.BarRenderers = new List<BarRenderer>();
        }

        public RowRenderer(PrimitiveRenderer primitiveRenderer, TablatureStyle style, bool isFirstRow)
            : this(primitiveRenderer, style)
        {
            this.IsFirstRow = isFirstRow;
        }


        public void Render(Point location, Size availableSize)
        {
            var drawingContext = new RowDrawingContext(location, availableSize, this.PrimitiveRenderer, this.Style);

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
                bar.Render(drawingContext, location, size);
                location.X += size.Width;
            }


            drawingContext.FinishHorizontalBarLines(availableSize.Width);
        }
    }
}
