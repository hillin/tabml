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
    class TablatureRenderer : ElementRenderer<CoreTablature>, IRootElementRenderer
    {
        public CoreTablature Tablature { get; }
        public PrimitiveRenderer PrimitiveRenderer { get; }
        public TablatureStyle Style { get; }
        public RenderingContext RenderingContext { get; private set; }
        
        private readonly Dictionary<ElementBase, ElementRenderer> _rendererLookup;

        public TablatureRenderer(PrimitiveRenderer primitiveRenderer, TablatureStyle style, CoreTablature tablature)
            : base(null, tablature)
        {
            this.Tablature = tablature;
            this.PrimitiveRenderer = primitiveRenderer;
            this.Style = style;

            _rendererLookup = new Dictionary<ElementBase, ElementRenderer>();
        }

        public void Render(RenderingContext rootRc, Point location, Size size)
        {
            this.RenderingContext = rootRc;
            
            this.PrimitiveRenderer.Clear();
            var tablatureRc = new TablatureRenderingContext(rootRc, this.PrimitiveRenderer, this.Style);
            this.DrawBars(tablatureRc, location, size);
        }

        private void DrawBars(TablatureRenderingContext tablatureRc, Point location, Size availableSize)
        {
            Func<bool, RowRenderer> createRowRenderer = isFirstRow =>
            {
                var r = new RowRenderer(this, isFirstRow);
                tablatureRc.Root.AssignRenderingContext(r, tablatureRc);
                return r;
            };

            var startY = location.Y;

            var rowRenderer = createRowRenderer(true);

            var caret = this.Style.FirstRowIndention;

            var barIndex = 0;
            while (barIndex < this.Tablature.Bars.Length)
            {
                if (rowRenderer.BarRenderers.Count < this.Style.RegularBarsPerRow)
                {
                    var bar = this.Tablature.Bars[barIndex];
                    var barRenderer = new BarRenderer(rowRenderer, this.Style, bar);
                    var minWidth = barRenderer.MeasureMinSize();
                    if (caret + minWidth <= availableSize.Width || rowRenderer.BarRenderers.Count == 0)
                    {
                        rowRenderer.BarRenderers.Add(barRenderer);
                        caret += minWidth;
                        ++barIndex;
                        continue;
                    }
                }

                rowRenderer.Render(location, new Size(availableSize.Width, availableSize.Height - location.Y + startY));
                location.Y += 200;  //todo
                // todo: handle new page
                rowRenderer = createRowRenderer(false);
                caret = 0;
            }

            if (rowRenderer.BarRenderers.Count > 0)
            {
                rowRenderer.Render(location, new Size(availableSize.Width, availableSize.Height - location.Y + startY));
            }
        }

        TRenderer IRootElementRenderer.GetRenderer<TElement, TRenderer>(TElement element)
        {
            ElementRenderer renderer;
            if (!_rendererLookup.TryGetValue(element, out renderer))
                return null;

            return (TRenderer)renderer;
        }

        void IRootElementRenderer.RegisterRenderer<TElement>(TElement element, ElementRenderer<TElement> renderer)
        {
            // _rendererLookup will be null if we are in our own constructor
            _rendererLookup?.Add(element, renderer);
        }
    }
}
