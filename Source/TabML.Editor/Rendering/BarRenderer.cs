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
    class BarRenderer
    {
        public PrimitiveRenderer PrimitiveRenderer { get; }
        public TablatureStyle Style { get; }
        public Bar Bar { get; }

        private double? _minSize;

        public BarRenderer(PrimitiveRenderer primitiveRenderer, TablatureStyle style, Bar bar)
        {
            this.PrimitiveRenderer = primitiveRenderer;
            this.Style = style;
            this.Bar = bar;
        }

        public double MeasureMinSize()
        {
            return _minSize ?? (_minSize = this.Bar.GetMinWidth(this.Style)).Value;
        }

        public void Render(RowDrawingContext context, Point location, Size availableSize)
        {
            var drawingContext = new BarDrawingContext(location, availableSize, context);

            this.Draw(drawingContext, availableSize.Width);
        }


        private void Draw(BarDrawingContext drawingContext, double width)
        {
            if (this.Bar.OpenLine != null)
                drawingContext.DrawBarLine(this.Bar.OpenLine.Value, 0.0);

            var minDuration = this.Bar.Columns.Min(c => c.GetDuration());
            var widthRatio = (width - drawingContext.Style.BarHorizontalPadding * 2) / this.Bar.GetMinWidth(this.Style);

            var position = drawingContext.Style.BarHorizontalPadding;

            drawingContext.ColumnRenderingInfos = new BarColumnRenderingInfo[this.Bar.Columns.Count];

            for (var i = 0; i < this.Bar.Columns.Count; i++)
            {
                var column = this.Bar.Columns[i];

                var columnWidth = this.Bar.GetColumnMinWidthInBar(column, this.Style, minDuration) * widthRatio;
                drawingContext.ColumnRenderingInfos[i] = new BarColumnRenderingInfo(column, position, columnWidth);
                new BarColumnRenderer().Render(drawingContext, drawingContext.ColumnRenderingInfos[i]);
                position += columnWidth;
            }

            if (this.Bar.BassVoice != null)
                new BarVoiceRenderer().Render(drawingContext, this.Bar.BassVoice);

            if (this.Bar.TrebleVoice != null)
                new BarVoiceRenderer().Render(drawingContext, this.Bar.TrebleVoice);

            if (this.Bar.CloseLine != null)
                drawingContext.DrawBarLine(this.Bar.CloseLine.Value, width);
        }
    }
}
