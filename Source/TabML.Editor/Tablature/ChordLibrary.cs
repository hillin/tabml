using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Parser;

namespace TabML.Editor.Tablature
{
    class ChordLibrary
    {


        public static readonly ChordLibrary Instance = new ChordLibrary();

        private readonly Dictionary<string, ChordDefinition> _chords;

        private ChordLibrary()
        {
            var chordsDocument = TabMLParser.TryParse(File.ReadAllText("Resources/standard_chords.txt"));
            _chords = chordsDocument.DocumentState.DefinedChords.ToDictionary(c => c.Name);
        }

        public IChordDefinition ResolveChord(DocumentState documentState, Chord chord)
        {
            if (chord.Fingering != null)
                return new InlineChordDefinition(chord);

            var chordDefinition =
                documentState.DefinedChords.FirstOrDefault(
                                 c => c.Name.Equals(chord.Name, StringComparison.InvariantCulture));
            if (chordDefinition != null)
                return chordDefinition;

            return _chords.TryGetValue(chord.Name, out chordDefinition) ? chordDefinition : null;
        }
    }
}
