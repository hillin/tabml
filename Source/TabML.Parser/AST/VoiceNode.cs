using System.Collections.Generic;

namespace TabML.Parser.AST
{
    class VoiceNode : Node
    {
        public List<BeatNode> Units { get; }

        public override IEnumerable<Node> Children => this.Units;

        public VoiceNode()
        {
            this.Units = new List<BeatNode>();
        }

        
    }
}
