﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;

namespace TabML.Editor.Rendering
{
    static class BeatExtensions
    {

        public static double GetStemTailPosition(this Beat beat, BarRenderingContext rc)
        {
            double from, to;
            rc.GetStemOffsetRange(beat.GetNearestStringIndex(), beat.GetStemRenderVoicePart(), out from, out to);
            return beat.GetStemRenderVoicePart() == VoicePart.Treble ? Math.Min(from, to) : Math.Max(from, to);
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

            var voicePart = beat.GetStemRenderVoicePart();

            if (beat.Notes == null || beat.Notes.Length == 0)
            {
                var chord = beat.OwnerColumn.Chord.Resolve(beat.OwnerBar.DocumentState);
                if (chord != null)
                {
                    var stringIndex = voicePart == VoicePart.Bass
                        ? chord.GetMaxStringIndex()
                        : chord.GetMinStringIndex();

                    if (stringIndex >= 0)
                        return stringIndex;
                }
                return voicePart == VoicePart.Bass ? 5 : 0;
            }

            return voicePart == VoicePart.Bass
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

            return beat.Notes == null || beat.Notes.Length == 0
                ? new[] { beat.GetStemRenderVoicePart() == VoicePart.Bass ? 5 : 0 }
                : beat.Notes.Select(n => n.String).ToArray();
        }

        public static double GetAlternationOffset(this Beat beat, BarRenderingContext rc, int? stringIndex = null, Beat tieTarget = null)
        {
            var targetBeat = tieTarget ?? beat;

            var column = rc.ColumnRenderingInfos[targetBeat.OwnerColumn.ColumnIndex];

            if (column.HasBrushlikeTechnique && column.MatchesChord)    // in this case we will just draw the technique directly
                return 0;

            var hasHarmonics = beat.Notes.Any(n => n.IsHarmonics);
            var ratio = column.GetNoteAlternationOffsetRatio(stringIndex ?? beat.GetNearestStringIndex());
            return rc.GetNoteAlternationOffset(ratio, hasHarmonics) + column.BrushlikeTechniqueSize + rc.Style.BrushlikeTechniqueMargin;
        }

        public static TiePosition GetTiePosition(this Beat beat)
        {
            return beat.TiePosition ?? beat.VoicePart.GetDefaultTiePosition();
        }
    }
}
