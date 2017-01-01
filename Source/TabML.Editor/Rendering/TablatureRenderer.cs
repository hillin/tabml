﻿using System;
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

        private readonly List<BarRenderer> _barRenderers;

        public TablatureRenderer(PrimitiveRenderer primitiveRenderer, TablatureStyle style, CoreTablature tablature)
            : base(null, tablature)
        {
            this.Tablature = tablature;
            this.PrimitiveRenderer = primitiveRenderer;
            this.Style = style;

            _rendererLookup = new Dictionary<ElementBase, ElementRenderer>();
            _barRenderers = new List<BarRenderer>();
        }

        public override void Initialize()
        {
            base.Initialize();

            _barRenderers.Clear();

            _barRenderers.AddRange(this.Element.Bars.Select(b => new BarRenderer(this, this.Style, b)));

            _barRenderers.Initialize();
        }

        public async Task Render(RenderingContext rootRc, Point location, Size size)
        {
            this.RenderingContext = rootRc;

            this.PrimitiveRenderer.Clear();
            var tablatureRc = new TablatureRenderingContext(rootRc, this.PrimitiveRenderer, this.Style);
            await this.RenderBars(tablatureRc, location, size);
        }

        private async Task RenderBars(TablatureRenderingContext renderingContext, Point location, Size availableSize)
        {
            var startY = location.Y;

            var isFirstRow = true;

            var caret = this.Style.FirstRowIndention;
            var barIndex = 0;
            var barRenders = new List<BarRenderer>();

            // ReSharper disable once AccessToModifiedClosure
            while (barIndex < this.Tablature.Bars.Length)
            {
                if (barRenders.Count < this.Style.RegularBarsPerRow)
                {
                    var barRenderer = _barRenderers[barIndex];
                    var minWidth = barRenderer.MeasureMinSize();
                    if (caret + minWidth <= availableSize.Width || barRenders.Count == 0)
                    {
                        barRenders.Add(barRenderer);
                        caret += minWidth;
                        ++barIndex;
                        continue;
                    }
                }

                await this.RenderRow(renderingContext, barRenders, location,
                                     new Size(availableSize.Width,
                                              availableSize.Height - location.Y + startY),
                                     isFirstRow);

                location.Y += 200;  //todo: replace magic number
                // todo: handle new page

                isFirstRow = false;
                barRenders.Clear();
                caret = 0;
            }

            if (barRenders.Count > 0)
            {
                await this.RenderRow(renderingContext, barRenders, location,
                                      new Size(availableSize.Width,
                                               availableSize.Height - location.Y + startY),
                                      isFirstRow);
            }
        }


        public async Task RenderRow(TablatureRenderingContext renderingContext, List<BarRenderer> barRenderers, Point location, Size availableSize, bool isFirstRow)
        {
            var rowRenderingContext = new RowRenderingContext(renderingContext, location, availableSize);

            barRenderers.AssignRenderingContexts(rowRenderingContext);

            var availableWidth = availableSize.Width;
            var count = barRenderers.Count;
            var averageWidth = availableWidth / count;
            foreach (var bar in barRenderers.OrderByDescending(b => b.MeasureMinSize()))
            {
                var minSize = bar.MeasureMinSize();
                if (minSize < averageWidth)
                    break;

                --count;
                availableWidth -= minSize;
                averageWidth = availableWidth / count;
            }

            foreach (var bar in barRenderers)
            {
                var minSize = bar.MeasureMinSize();
                var width = Math.Min(availableSize.Width, Math.Max(minSize, averageWidth));
                var size = new Size(width, availableSize.Height);

                await bar.Render(location, size);

                location.X += size.Width;
            }


            rowRenderingContext.FinishHorizontalBarLines(availableSize.Width);
            rowRenderingContext.DebugDrawHeightMaps();
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
