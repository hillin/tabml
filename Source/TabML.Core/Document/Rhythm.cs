using System.Collections.Generic;

namespace TabML.Core.Document
{
    public class Rhythm
    {
        public List<RhythmSegment> Segments { get; }

        public Rhythm()
        {
            this.Segments = new List<RhythmSegment>();
        }
    }
}
