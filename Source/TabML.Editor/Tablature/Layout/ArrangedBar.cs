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
                        // size is equally and explicitly divided by NoteValue
                        var durationWidth = width / this.Duration.Duration;
                        var position = 0.0;

                        foreach (var column in this.Columns)
                        {
                            var columnWidth = durationWidth * column.GetDuration();
                            column.Draw(drawingContext, position, columnWidth);
                            position += columnWidth;
                        }

                        drawingContext.FinishHorizontalBarLines(width);
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
    }
}
