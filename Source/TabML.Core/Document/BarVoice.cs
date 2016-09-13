using System.Collections.Generic;
using System.Linq;

namespace TabML.Core.Document
{
    public class BarVoice
    {
        public bool IsMainVoice { get; }
        public List<VoiceSegment> RhythmSegments { get; }

        public BarVoice(bool isMainVoice = true)
        {
            this.IsMainVoice = isMainVoice;
            this.RhythmSegments = new List<VoiceSegment>();
        }

        public double GetDuration()
        {
            return this.RhythmSegments.Sum(r => r.GetDuration());
        }
    }
}
