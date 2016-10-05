using System.Collections.Generic;

namespace TabML.Parser.AST
{
    class ChordFingeringNode : Node
    {
        public List<LiteralNode<int>> Fingerings { get; }

        public ChordFingeringNode()
        {
            this.Fingerings = new List<LiteralNode<int>>();
        }

        public override IEnumerable<Node> Children => this.Fingerings;
    }
}
