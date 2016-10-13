using System.Collections.Generic;
using TabML.Parser.Document;

namespace TabML.Editor.Tablature.Layout
{
    class ArrangedBarColumn
    {
        public List<Beat> VoiceBeats { get; }
        public Chord Chord { get; set; }

        public ArrangedBarColumn()
        {
            this.VoiceBeats = new List<Beat>();
        }
    }
}
