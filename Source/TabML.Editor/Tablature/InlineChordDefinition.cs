using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Tablature
{
    class InlineChordDefinition : IChordDefinition
    {
        private readonly Chord _chord;
        public string DisplayName => _chord.Name;
        public IChordFingering Fingering => _chord.Fingering;

        public InlineChordDefinition(Chord chord)
        {
            _chord = chord;
        }
    }
}
