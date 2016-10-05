using System.Collections.Generic;

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
    }
}
