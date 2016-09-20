using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Document
{
    public abstract class RhythmSegmentBase
    {
        public List<RhythmVoice> Voices { get; }

        protected RhythmSegmentBase()
        {
            this.Voices = new List<RhythmVoice>();
        }
        public double GetDuration()
        {
            return this.Voices.Max(v => v.GetDuration());
        }
    }
}
