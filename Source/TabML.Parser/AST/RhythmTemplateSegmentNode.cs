using System.Linq;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class RhythmTemplateSegmentNode : RhythmSegmentNodeBase
    {
        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out RhythmTemplateSegment rhythmSegment)
        {
            rhythmSegment = new RhythmTemplateSegment
            {
                Range = this.Range
            };

            return this.FillRhythmSegmentVoices(context, reporter, rhythmSegment);
        }

        public bool ValueEquals(RhythmTemplateSegment other)
        {
            if (other == null)
                return false;

            return other.Voices.Count == this.Voices.Count
                && !this.Voices.Where((v, i) => !v.ValueEquals(other.Voices[i])).Any();
        }
    }
}
