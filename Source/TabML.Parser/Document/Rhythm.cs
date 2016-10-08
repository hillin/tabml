using System.Collections.Generic;

namespace TabML.Parser.Document
{
    class Rhythm : Element
    {
        public List<RhythmSegment> Segments { get; }

        public Rhythm()
        {
            this.Segments = new List<RhythmSegment>();
        }
    }
}
