using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser.Document
{
    public class RhythmTemplateSegment : RhythmSegmentBase
    {
        private static Voice InstantializeVoice(Voice voice)
        {
            var instance = voice.Clone();
            instance.ClearRange();
            return instance;
        }

        public RhythmSegment Instantialize()
        {
            var segment = new RhythmSegment();  // do not set Range

            if (this.TrebleVoice != null)
                segment.TrebleVoice = RhythmTemplateSegment.InstantializeVoice(this.TrebleVoice);

            if (this.BassVoice != null)
                segment.BassVoice = RhythmTemplateSegment.InstantializeVoice(this.BassVoice);

            return segment;
        }
    }
}
