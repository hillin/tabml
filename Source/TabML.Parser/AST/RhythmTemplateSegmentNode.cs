using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class RhythmTemplateSegmentNode : RhythmSegmentNodeBase, IValueEquatable<RhythmTemplateSegmentNode>
    {
        public bool ValueEquals(RhythmTemplateSegmentNode other)
        {
            return base.ValueEquals(other);
        }

        public override bool ToDocumentElement(TablatureContext context, IReporter reporter, out RhythmSegment rhythmSegment)
        {
            var result = base.ToDocumentElement(context, reporter, out rhythmSegment);
            if (result)
            {
                rhythmSegment.ClearRange();
            }

            return result;
        }
    }
}
