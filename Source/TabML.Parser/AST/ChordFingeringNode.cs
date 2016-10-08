using System.Collections.Generic;
using System.Linq;

namespace TabML.Parser.AST
{
    class ChordFingeringNode : Node, IValueEquatable<ChordFingeringNode>
    {
        public const int FingeringSkipString = -1;

        public List<LiteralNode<int>> Fingerings { get; }

        public ChordFingeringNode()
        {
            this.Fingerings = new List<LiteralNode<int>>();
        }

        public override IEnumerable<Node> Children => this.Fingerings;

        public int[] GetFingeringIndices() => this.Fingerings.Select(f => f.Value).ToArray();
        public bool ValueEquals(ChordFingeringNode other)
        {
            if (other == null)
                return false;

            return other.Fingerings.Count != this.Fingerings.Count
                && !this.Fingerings.Where((f, i) => !f.ValueEquals(other.Fingerings[i])).Any();
        }
    }
}
