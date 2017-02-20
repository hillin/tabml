using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    static class ChordDefinitionExtensions
    {
        public static int GetMaxStringIndex(this IChordDefinition chord)
        {
            for (var i = 0; i < chord.Fingering.Notes.Count; ++i)
            {
                if (chord.Fingering.Notes[i].Fret != ChordFingeringNote.FingeringSkipString)
                    return chord.Fingering.Notes.Count - i - 1;
            }


            return -1;
        }

        public static int GetMinStringIndex(this IChordDefinition chord)
        {
            for (var i = chord.Fingering.Notes.Count - 1; i >= 0; --i)
            {
                if (chord.Fingering.Notes[i].Fret != ChordFingeringNote.FingeringSkipString)
                    return chord.Fingering.Notes.Count - i - 1;
            }

            return -1;
        }
    }
}
