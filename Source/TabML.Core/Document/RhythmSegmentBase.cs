using System;
using System.Collections.Generic;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public abstract class RhythmSegmentBase : Element
    {
        public RhythmSegmentVoice TrebleVoice { get; set; }
        public RhythmSegmentVoice BassVoice { get; set; }

        public RhythmSegmentVoice FirstVoice => this.TrebleVoice ?? this.BassVoice;
        
        public PreciseDuration GetDuration() => new PreciseDuration(Math.Max(this.BassVoice?.GetDuration().FixedPointValue ?? 0,
                                                                             this.TrebleVoice?.GetDuration().FixedPointValue ?? 0));

        public override IEnumerable<Element> Children
        {
            get
            {
                if (this.TrebleVoice != null)
                    yield return this.TrebleVoice;

                if (this.BassVoice != null)
                    yield return this.BassVoice;
            }
        }
    }
}
