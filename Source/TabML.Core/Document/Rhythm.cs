using System.Collections.Generic;
using System.Linq;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class Rhythm : Element
    {
        public List<RhythmSegment> Segments { get; }

        public bool NotMatchingTime { get; set; }

        public Rhythm()
        {
            this.Segments = new List<RhythmSegment>();
        }

        public PreciseDuration GetDuration() => this.Segments.Sum(s => s.GetDuration());

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
