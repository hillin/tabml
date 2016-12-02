using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TabML.Core.Document;
using CoreTablature = TabML.Core.Document.Tablature;

namespace TabML.Editor.Rendering
{
    class TablatureRenderer
    {
        public CoreTablature Tablature { get; }
        public PrimitiveRenderer PrimitiveRenderer { get; }
        public TablatureStyle Style { get; }

        public TablatureRenderer(PrimitiveRenderer primitiveRenderer, TablatureStyle style, CoreTablature tablature)
        {
            this.Tablature = tablature;
            this.PrimitiveRenderer = primitiveRenderer;
            this.Style = style;
        }


        public void Render(Point location, Size availableSize)
        {
            this.PrimitiveRenderer.Clear();

            this.DrawBars(location, availableSize);
        }

        private void DrawBars(Point location, Size availableSize)
        {
            var startY = location.Y;

            var row = new RowRenderer(this.PrimitiveRenderer, this.Style, true);
            var caret = this.Style.FirstRowIndention;

            var barIndex = 0;
            while (barIndex < this.Tablature.Bars.Length)
            {
                if (row.BarRenderers.Count < this.Style.RegularBarsPerRow)
                {
                    var bar = this.Tablature.Bars[barIndex];
                    var barRenderer = new BarRenderer(this.PrimitiveRenderer, this.Style, bar);
                    var minWidth = barRenderer.MeasureMinSize();
                    if (caret + minWidth <= availableSize.Width || row.BarRenderers.Count == 0)
                    {
                        row.BarRenderers.Add(barRenderer);
                        caret += minWidth;
                        ++barIndex;
                        continue;
                    }
                }

                row.Render(location, new Size(availableSize.Width, availableSize.Height - location.Y + startY));
                location.Y += 200;  //todo
                // todo: handle new page
                row = new RowRenderer(this.PrimitiveRenderer, this.Style, false);
                caret = 0;
            }
            
            if (row.BarRenderers.Count > 0)
            {
                row.Render(location, new Size(availableSize.Width, availableSize.Height - location.Y + startY));
            }
        }
        
    }
}
