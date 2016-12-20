using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Editor.Tablature;
using TabML.Editor.Tablature.Layout;

namespace TabML.Editor.Rendering
{
    class BarRenderer : ElementRenderer<Bar, RowRenderingContext>
    {
        public TablatureStyle Style { get; }
        public Point Location { get; private set; }
        public Size RenderSize { get; private set; }

        private double? _minSize;

        private readonly List<BarColumnRenderer> _columnRenderers;
        private readonly List<BarVoiceRenderer> _voiceRenderers;

        public BarRenderer(TablatureRenderer owner, TablatureStyle style, Bar bar)
            : base(owner, bar)
        {
            this.Style = style;

            _columnRenderers = new List<BarColumnRenderer>();
            _voiceRenderers = new List<BarVoiceRenderer>();
        }

        public override void Initialize()
        {
            base.Initialize();

            _columnRenderers.AddRange(this.Element.Columns.Select(c => new BarColumnRenderer(this, c)));

            _columnRenderers.Initialize();

            if (this.Element.BassVoice != null)
                _voiceRenderers.Add(new BarVoiceRenderer(this, this.Element.BassVoice));

            if (this.Element.TrebleVoice != null)
                _voiceRenderers.Add(new BarVoiceRenderer(this, this.Element.TrebleVoice));

            _voiceRenderers.Initialize();
        }

        public double MeasureMinSize()
        {
            return _minSize ?? (_minSize = this.Element.GetMinWidth(this.Style)).Value;
        }

        public void Render(Point location, Size size)
        {
            this.Location = location;
            this.RenderSize = size;

            var renderingContext = new BarRenderingContext(this.RenderingContext, location, size);
            _columnRenderers.AssignRenderingContexts(renderingContext);
            _voiceRenderers.AssignRenderingContexts(renderingContext);

            var width = size.Width;
            if (this.Element.OpenLine != null)
                renderingContext.DrawBarLine(this.Element.OpenLine.Value, 0.0);

            var minDuration = this.Element.Columns.Min(c => c.GetDuration());
            var widthRatio = (width - renderingContext.Style.BarHorizontalPadding * 2) / this.Element.GetMinWidth(this.Style);

            var position = renderingContext.Style.BarHorizontalPadding;

            renderingContext.ColumnRenderingInfos = new BarColumnRenderingInfo[this.Element.Columns.Count];

            for (var i = 0; i < this.Element.Columns.Count; i++)
            {
                var column = this.Element.Columns[i];

                var columnWidth = this.Element.GetColumnMinWidthInBar(column, this.Style, minDuration) * widthRatio;
                renderingContext.ColumnRenderingInfos[i] = new BarColumnRenderingInfo(column, position, columnWidth);

                var barColumnRenderer = _columnRenderers[i];
                barColumnRenderer.Render(renderingContext.ColumnRenderingInfos[i]);
                position += columnWidth;
            }

            _voiceRenderers.ForEach(r => r.Render());

            if (this.Element.CloseLine != null)
                renderingContext.DrawBarLine(this.Element.CloseLine.Value, width);
        }

    }
}
