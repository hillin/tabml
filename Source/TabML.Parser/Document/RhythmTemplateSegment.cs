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
            segment.Voices.AddRange(this.Voices.Select(RhythmTemplateSegment.InstantializeVoice));

            return segment;
        }
    }
}
