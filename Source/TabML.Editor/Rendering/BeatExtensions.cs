using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    static class BeatExtensions
    {

        public static double GetStemTailPosition(this Beat beat, BarDrawingContext drawingContext)
        {
            double from, to;
            drawingContext.GetStemOffsetRange(beat.GetNearestStringIndex(), beat.VoicePart, out from, out to);
            return beat.VoicePart == VoicePart.Treble ? Math.Min(from, to) : Math.Max(from, to);
        }

        public static int GetNearestStringIndex(this Beat beat)
        {
            if (beat.IsTied)
            {
                if (beat.PreviousBeat == null)
                {
                    // todo: handle cross-bar ties
                }
                else
                    return beat.PreviousBeat.GetNearestStringIndex();
            }

            if (beat.Notes == null || beat.Notes.Length == 0)
                return beat.VoicePart == VoicePart.Bass ? 5 : 0;

            return beat.VoicePart == VoicePart.Bass
                ? beat.Notes.Max(n => n.String)
                : beat.Notes.Min(n => n.String);
        }


        public static int[] GetNoteStrings(this Beat beat)
        {
            if (beat.IsTied)
            {
                if (beat.PreviousBeat == null)
                {
                    // todo: handle cross-bar ties
                }
                else
                    return beat.PreviousBeat.GetNoteStrings();
            }

            if (beat.Notes == null || beat.Notes.Length == 0)
                return new[] { beat.VoicePart == VoicePart.Bass ? 5 : 0 };
            else
                return beat.Notes.Select(n => n.String).ToArray();
        }

        public static double GetAlternationOffset(this Beat beat, BarDrawingContext drawingContext, int? stringIndex = null)
        {
            var column = drawingContext.ColumnRenderingInfos[beat.OwnerColumn.ColumnIndex];
            var ratio = column.GetNoteAlternationOffsetRatio(stringIndex ?? beat.GetNearestStringIndex());
            return drawingContext.GetNoteAlternationOffset(ratio);
        }


    }
}
