using System.Linq;
using TabML.Core.Document;
using TabML.Core.Logging;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class RhythmTemplateSegmentNode : RhythmSegmentNodeBase
    {
        public bool ToDocumentElement(TablatureContext context, ILogger logger, out RhythmTemplateSegment rhythmSegment)
        {
            rhythmSegment = new RhythmTemplateSegment
            {
                Range = this.Range
            };

            return this.FillRhythmSegmentVoices(context, logger, rhythmSegment);
        }

        public bool ValueEquals(RhythmTemplateSegment other)
        {
            if (other == null)
                return false;

            if (this.BassVoice != null && other.BassVoice != null)
                if (!this.BassVoice.ValueEquals(other.BassVoice))
                    return false;

            if (this.BassVoice != null || other.BassVoice != null)
                return false;

            if (this.TrebleVoice != null && other.TrebleVoice != null)
                if (!this.TrebleVoice.ValueEquals(other.TrebleVoice))
                    return false;

            if (this.TrebleVoice != null || other.TrebleVoice != null)
                return false;

            return true;
        }
    }
}
