using System.Collections.Generic;
using System.Linq;
using TabML.Core.Document;
using TabML.Core.Logging;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class RhythmTemplateNode : Node, IDocumentElementFactory<RhythmTemplate>
    {
        public List<RhythmTemplateSegmentNode> Segments { get; }

        public RhythmTemplateNode()
        {
            this.Segments = new List<RhythmTemplateSegmentNode>();
        }

        public override IEnumerable<Node> Children => this.Segments;

        public bool ValueEquals(RhythmTemplate other)
        {
            if (other == null)
                return false;

            return (other.Segments.Count != this.Segments.Count)
                && !this.Segments.Where((t, i) => !t.ValueEquals(other.Segments[i])).Any();
        }

        public bool ToDocumentElement(TablatureContext context, ILogger logger, out RhythmTemplate rhythm)
        {
            rhythm = new RhythmTemplate
            {
                Range = this.Range
            };

            foreach (var segment in this.Segments)
            {
                RhythmTemplateSegment rhythmSegment;
                if (!segment.ToDocumentElement(context, logger, out rhythmSegment))
                    return false;

                rhythm.Segments.Add(rhythmSegment);
            }

            return true;
        }

    }
}
