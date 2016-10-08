using System.Collections.Generic;
using System.Linq;

namespace TabML.Parser.Document
{
    class RhythmSegment : Element
    {
        public Chord Chord { get; set; }
        public List<Voice> Voices { get; }

        public RhythmSegment()
        {
            this.Voices = new List<Voice>();
        }
        public double GetDuration()
        {
            return this.Voices.Max(v => v.GetDuration());
        }
    }
}
