using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Editor.Tablature;

namespace TabML.Editor.Rendering
{
    static class ChordExtensions
    {
        public static IChordDefinition Resolve(this Chord chord, DocumentState documentState)
        {
            return chord == null ? null : ChordLibrary.Instance.ResolveChord(documentState, chord);
        }
    }
}
