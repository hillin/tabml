using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;

namespace TabML.Editor.Tablature.Layout
{
    class ArrangedBar
    {
        public ArrangedBarVoice TrebleVoice { get; set; }
        public ArrangedBarVoice BassVoice { get; set; }
        public PreciseDuration Duration { get; set; }
        public List<ArrangedBarColumn> Columns { get; }
        public OpenBarLine? OpenLine { get; set; }
        public CloseBarLine? CloseLine { get; set; }

        public ArrangedBar()
        {
            this.Columns = new List<ArrangedBarColumn>();
        }

        public double GetMinWidth(TablatureStyle style)
        {
            switch (style.BeatLayout)
            {
                case BeatLayout.SizeByNoteValue:
                    if (style.FlexibleBeatSize)
                        throw new NotImplementedException(); //todo
                    else
                    {
                        // size is equally and explicitly divided by NoteValue
                        var minWidth = 0.0;
                        foreach (var column in this.Columns)
                        {
                            var columnMinWidth = column.GetMinWidth(style);
                            minWidth = Math.Max(minWidth,
                                                columnMinWidth / column.GetDuration() * this.Duration);
                        }

                        return minWidth;
                    }
                case BeatLayout.DivideInBeats:
                    throw new NotImplementedException();    //todo
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Draw(IBarDrawingContext drawingContext, double width)
        {
            if (this.OpenLine != null)
                drawingContext.DrawBarLine(this.OpenLine.Value, 0.0);

            switch (drawingContext.Style.BeatLayout)
            {
                case BeatLayout.SizeByNoteValue:
                    if (drawingContext.Style.FlexibleBeatSize)
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

            if (this.CloseLine != null)
                drawingContext.DrawBarLine(this.CloseLine.Value, width);
        }

        private void Draw_FixedBeatSizeByNoteValue(IBarDrawingContext drawingContext, double width)
        {
            // size is equally and explicitly divided by NoteValue

            // determine under which duration a column should be assigned with a minimum size
            // and the size of unit duration
            var durationWidth = drawingContext.Style.MinimumBeatSize;
            var minWidthDuration = PreciseDuration.Zero;
            var sumDuration = this.Duration;
            var remainingWidth = width - drawingContext.Style.BarHorizontalPadding * 2;
            foreach (var column in this.Columns.OrderBy(c => c.GetDuration()))
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

            var columnPositions = new double[this.Columns.Count];

            for (var i = 0; i < this.Columns.Count; i++)
            {
                var column = this.Columns[i];
                var columnDuration = column.GetDuration();
                var columnWidth = columnDuration < minWidthDuration
                    ? drawingContext.Style.MinimumBeatSize
                    : durationWidth * columnDuration;
                column.Position = position;
                column.Width = columnWidth;
                columnPositions[i] = position;
                column.Draw(drawingContext, position, columnWidth);
                position += columnWidth;
            }

            this.BassVoice?.Draw(drawingContext, columnPositions);
            this.TrebleVoice?.Draw(drawingContext, columnPositions);

            drawingContext.FinishHorizontalBarLines(width);
        }

    }
}
