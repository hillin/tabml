using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class RhythmSegmentVoice : Element
    {
        public List<Beat> BeatElements { get; }
        public VoicePart Part { get; }

        public RhythmSegmentVoice(VoicePart part)
        {
            this.Part = part;
            this.BeatElements = new List<Beat>();
        }
        public PreciseDuration GetDuration() => this.BeatElements.Sum(n => n.GetDuration());

        public void ClearRange()
        {
            this.Range = null;

            foreach (var beat in this.BeatElements)
                beat.ClearRange();
        }

        public RhythmSegmentVoice Clone()
        {
            var clone = new RhythmSegmentVoice(this.Part)
            {
                Range = this.Range,
            };
            clone.BeatElements.AddRange(this.BeatElements.Select(b => b.Clone()));
            return clone;
        }
    }
}
