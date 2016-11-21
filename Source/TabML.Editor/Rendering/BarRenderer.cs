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

        public BarRenderer(PrimitiveRenderer primitiveRenderer, TablatureStyle style)
        {
            this.PrimitiveRenderer = primitiveRenderer;
            this.Style = style;
        }

        public void Render(Bar bar, Point location, Size availableSize)
        {
            var drawingContext = new BarDrawingContext(location, availableSize, this.PrimitiveRenderer, this.Style);

            this.Draw(bar, drawingContext, availableSize.Width);
        }

        
        private void Draw(Bar bar, BarDrawingContext drawingContext, double width)
        {
            if (bar.OpenLine != null)
                drawingContext.DrawBarLine(bar.OpenLine.Value, 0.0);

            switch (drawingContext.Style.BeatLayout)
            {
                case BeatLayout.SizeByNoteValue:
                    if (drawingContext.Style.FlexibleBeatSize)
                        throw new NotImplementedException(); //todo
                    else
                    {
                        this.Draw_FixedBeatSizeByNoteValue(bar, drawingContext, width);
                    }

                    break;
                case BeatLayout.DivideInBeats:
                    throw new NotImplementedException();    //todo
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (bar.CloseLine != null)
                drawingContext.DrawBarLine(bar.CloseLine.Value, width);
        }

        private void Draw_FixedBeatSizeByNoteValue(Bar bar, BarDrawingContext drawingContext, double width)
        {
            // size is equally and explicitly divided by NoteValue

            // determine under which duration a column should be assigned with a minimum size
            // and the size of unit duration
            var durationWidth = drawingContext.Style.MinimumBeatSize;
            var minWidthDuration = PreciseDuration.Zero;
            var sumDuration = bar.Duration;
            var remainingWidth = width - drawingContext.Style.BarHorizontalPadding * 2;
            foreach (var column in bar.Columns.OrderBy(c => c.GetDuration()))
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

            drawingContext.ColumnRenderingInfos = new BarColumnRenderingInfo[bar.Columns.Count];

            for (var i = 0; i < bar.Columns.Count; i++)
            {
                var column = bar.Columns[i];
                var columnDuration = column.GetDuration();
                var columnWidth = columnDuration < minWidthDuration
                    ? drawingContext.Style.MinimumBeatSize
                    : durationWidth * columnDuration;
                drawingContext.ColumnRenderingInfos[i] = new BarColumnRenderingInfo(column, position, columnWidth);
                new BarColumnRenderer().Render(drawingContext, drawingContext.ColumnRenderingInfos[i]);
                position += columnWidth;
            }

            if (bar.BassVoice != null)
                new BarVoiceRenderer().Render(drawingContext, bar.BassVoice);

            if (bar.TrebleVoice != null)
                new BarVoiceRenderer().Render(drawingContext, bar.TrebleVoice);

            drawingContext.FinishHorizontalBarLines(width);
        }

    }
}
