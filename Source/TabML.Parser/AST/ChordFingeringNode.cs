using System.Collections.Generic;
using System.Linq;

namespace TabML.Parser.AST
{
    class ChordFingeringNode : Node
    {
        public const int FingeringSkipString = -1;

        public List<LiteralNode<int>> Fingerings { get; }

        public ChordFingeringNode()
        {
            this.Fingerings = new List<LiteralNode<int>>();
        }

        public override IEnumerable<Node> Children => this.Fingerings;

        public int[] GetFingeringIndices() => this.Fingerings.Select(f => f.Value).ToArray();
    }
}
