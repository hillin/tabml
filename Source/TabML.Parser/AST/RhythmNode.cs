using System.Collections.Generic;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class RhythmNode : Node
    {
        public List<RhythmSegmentNode> Segments { get; }

        public RhythmNode()
        {
            this.Segments = new List<RhythmSegmentNode>();
        }

        public override IEnumerable<Node> Children => this.Segments;

        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out Rhythm rhythm)
        {
            rhythm = new Rhythm
            {
                Range = this.Range
            };

            // todo: check voice count consistency
            // todo: check duration consistency

            foreach (var segment in this.Segments)
            {
                RhythmSegment rhythmSegment;
                if (!segment.ToDocumentElement(context, reporter, out rhythmSegment))
                    return false;

                rhythm.Segments.Add(rhythmSegment);
            }

            return true;
        }
    }
}
