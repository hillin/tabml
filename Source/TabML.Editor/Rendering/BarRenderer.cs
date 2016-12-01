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

        public void Render(Point location, Size availableSize)
        {
            var drawingContext = new BarDrawingContext(location, availableSize, this.PrimitiveRenderer, this.Style);

            this.Draw(drawingContext, availableSize.Width);
        }


        private void Draw(BarDrawingContext drawingContext, double width)
        {
            if (this.Bar.OpenLine != null)
                drawingContext.DrawBarLine(this.Bar.OpenLine.Value, 0.0);

            switch (this.Style.BeatLayout)
            {
                case BeatLayout.SizeByNoteValue:
                    if (this.Style.FlexibleBeatSize)
                        throw new NotImplementedException(); //todo
                    else
                    {
                        this.Draw_FixedBeatSizeByNoteValue(drawingContext, width);
                    }

                    break;
                case BeatLayout.DivideInBeats:
                    throw new NotImplementedException();    //todo
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (this.Bar.CloseLine != null)
                drawingContext.DrawBarLine(this.Bar.CloseLine.Value, width);
        }

        private void Draw_FixedBeatSizeByNoteValue(BarDrawingContext drawingContext, double width)
        {
            // size is equally and explicitly divided by NoteValue

            // determine under which duration a column should be assigned with a minimum size
            // and the size of unit duration
            var durationWidth = drawingContext.Style.MinimumBeatSize;
            var minWidthDuration = PreciseDuration.Zero;
            var sumDuration = this.Bar.Duration;
            var remainingWidth = width - drawingContext.Style.BarHorizontalPadding * 2;
            foreach (var column in this.Bar.Columns.OrderBy(c => c.GetDuration()))
            {
                durationWidth = remainingWidth / sumDuration;
                var columnDuration = column.GetDuration();
                if (durationWidth * columnDuration >= drawingContext.Style.MinimumBeatSize)
                    break;

                minWidthDuration = columnDuration;
                remainingWidth -= drawingContext.Style.MinimumBeatSize;
                sumDuration -= columnDuration;
            }

            var position = drawingContext.Style.BarHorizontalPadding;

            drawingContext.ColumnRenderingInfos = new BarColumnRenderingInfo[this.Bar.Columns.Count];

            for (var i = 0; i < this.Bar.Columns.Count; i++)
            {
                var column = this.Bar.Columns[i];
                var columnDuration = column.GetDuration();
                var columnWidth = columnDuration < minWidthDuration
                    ? drawingContext.Style.MinimumBeatSize
                    : durationWidth * columnDuration;
                drawingContext.ColumnRenderingInfos[i] = new BarColumnRenderingInfo(column, position, columnWidth);
                new BarColumnRenderer().Render(drawingContext, drawingContext.ColumnRenderingInfos[i]);
                position += columnWidth;
            }

            if (this.Bar.BassVoice != null)
                new BarVoiceRenderer().Render(drawingContext, this.Bar.BassVoice);

            if (this.Bar.TrebleVoice != null)
                new BarVoiceRenderer().Render(drawingContext, this.Bar.TrebleVoice);

            drawingContext.FinishHorizontalBarLines(width);
        }

    }
}
