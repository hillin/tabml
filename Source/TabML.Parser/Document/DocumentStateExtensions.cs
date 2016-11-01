using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Parser.AST;
using Chord = TabML.Core.MusicTheory.Chord;

namespace TabML.Parser.Document
{
    static class DocumentStateExtensions
    {
        public static bool LookupChord(this DocumentState state, string chordName, out ChordFingering fingering,
                                       out Chord theoreticalChord)
        {
            var chord =
                state.DefinedChords.FirstOrDefault(
                    c => c.Name.Equals(chordName, StringComparison.InvariantCultureIgnoreCase));

            if (chord != null)
            {
                fingering = chord.Fingering;
                theoreticalChord = null;
                return true;
            }

            fingering = null;

            return new ChordParser().TryParse(chordName, out theoreticalChord);
        }
    }
}
