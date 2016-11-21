using System.Collections.Generic;
using System.Linq;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class Rhythm : Element
    {
        public List<RhythmSegment> Segments { get; }
        public override IEnumerable<Element> Children => this.Segments;

        public bool NotMatchingTime { get; set; }

        public Rhythm()
        {
            this.Segments = new List<RhythmSegment>();
        }

        public PreciseDuration GetDuration() => this.Segments.Sum(s => s.GetDuration());

        public Rhythm Clone()
        {
            var rhythm = new Rhythm
            {
                Range = this.Range,
                NotMatchingTime = this.NotMatchingTime
            };

            rhythm.Segments.AddRange(this.Segments.Select(s => s.Clone()));
            return rhythm;
        }
    }
}
