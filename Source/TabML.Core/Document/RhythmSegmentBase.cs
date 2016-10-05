using System.Collections.Generic;
using System.Linq;

namespace TabML.Core.Document
{
    public abstract class RhythmSegmentBase
    {
        public List<Voice> Voices { get; }

        protected RhythmSegmentBase()
        {
            this.Voices = new List<Voice>();
        }
        public double GetDuration()
        {
            return this.Voices.Max(v => v.GetDuration());
        }
    }
}
