using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public class BarVoice
    {
        public bool IsMainVoice { get; }
        public List<RhythmSegment> RhythmSegments { get; }

        public BarVoice(bool isMainVoice = true)
        {
            this.IsMainVoice = isMainVoice;
            this.RhythmSegments = new List<RhythmSegment>();
        }

        public double GetDuration()
        {
            return this.RhythmSegments.Sum(r => r.GetDuration());
        }
    }
}
