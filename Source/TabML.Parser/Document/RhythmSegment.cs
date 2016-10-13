using System.Collections.Generic;
using System.Linq;

namespace TabML.Parser.Document
{
    public class RhythmSegment : RhythmSegmentBase
    {
        public Chord Chord { get; set; }
        public bool IsOmittedByTemplate { get; set; }

        public RhythmSegment Clone()
        {
            var clone = new RhythmSegment
            {
                Range = this.Range,
                Chord = this.Chord.Clone(),
                IsOmittedByTemplate = this.IsOmittedByTemplate,
                BassVoice = this.BassVoice?.Clone(),
                TrebleVoice = this.TrebleVoice?.Clone()
            };

            return clone;
        }
    }
}
