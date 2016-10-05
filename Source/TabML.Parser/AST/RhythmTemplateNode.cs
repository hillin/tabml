using System.Collections.Generic;

namespace TabML.Parser.AST
{
    class RhythmTemplateNode : Node
    {
        public List<RhythmTemplateSegmentNode> Segments { get; }

        public RhythmTemplateNode()
        {
            this.Segments = new List<RhythmTemplateSegmentNode>();
        }

        public override IEnumerable<Node> Children => this.Segments;
    }
}
