using System;
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

        public static int GetOutmostStringIndex(this Beat beat)
        {
            beat = beat.NotesDefiner;

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
            beat = beat.NotesDefiner;

            return beat.Notes == null || beat.Notes.Length == 0
                ? new[] { beat.GetStemRenderVoicePart() == VoicePart.Bass ? 5 : 0 }
                : beat.Notes.Select(n => n.String).ToArray();
        }

        public static TiePosition GetTiePosition(this Beat beat)
        {
            return beat.TiePosition ?? beat.VoicePart.GetDefaultTiePosition();
        }
    }
}
