using System.Collections.Generic;
using TabML.Core.MusicTheory;
using Chord = TabML.Parser.Document.Chord;

namespace TabML.Editor.Tablature.Layout
{
    class ArrangedBarColumn
    {
        public List<ArrangedBarBeat> VoiceBeats { get; }
        public Chord Chord { get; set; }
        public PreciseDuration Position { get; set; }

        public ArrangedBarColumn()
        {
            this.VoiceBeats = new List<ArrangedBarBeat>();
        }
    }
}
