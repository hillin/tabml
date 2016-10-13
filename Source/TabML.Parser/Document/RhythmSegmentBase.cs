using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Parser.Document
{
    public abstract class RhythmSegmentBase : Element
    {
        public Voice TrebleVoice { get; set; }
        public Voice BassVoice { get; set; }

        public Voice FirstVoice => this.TrebleVoice ?? this.BassVoice;
        
        public PreciseDuration GetDuration()
        {
            return
                new PreciseDuration(Math.Max(this.BassVoice?.GetDuration().FixedPointValue ?? 0,
                                             this.TrebleVoice?.GetDuration().FixedPointValue ?? 0));
        }

    }
}
