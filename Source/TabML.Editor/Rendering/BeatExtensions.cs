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

        public static int GetOutmostStringIndex(this Beat beat, VoicePart? voicePart = null)
        {
            beat = beat.NotesDefiner;

            voicePart = voicePart ?? beat.GetStemRenderVoicePart();

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

        public static TiePosition GetRenderTiePosition(this Beat beat)
        {
            if (beat.TiePosition != null)
                return beat.TiePosition.Value;

            return beat.OwnerBar.HasSingularVoice()
                ? TiePosition.Above
                : beat.VoicePart.GetDefaultTiePosition();
        }

        public static VoicePart GetRenderTieVoicePart(this Beat beat)
        {
            return beat.OwnerBar.HasSingularVoice() ? VoicePart.Treble : beat.VoicePart;
        }
    }
}
