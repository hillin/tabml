using System.Collections.Generic;
using System.Linq;
using TabML.Parser.Parsing;

namespace TabML.Parser.Document
{
    class Rhythm : Element
    {
        public List<RhythmSegment> Segments { get; }

        public bool NotMatchingTime { get; set; }

        public Rhythm()
        {
            this.Segments = new List<RhythmSegment>();
        }

        public Rhythm Clone()
        {
            var clone = new Rhythm
            {
                Range = this.Range,
                NotMatchingTime = this.NotMatchingTime
            };

            clone.Segments.AddRange(this.Segments.Select(s => s.Clone()));
            return clone;
        }
    }
}
