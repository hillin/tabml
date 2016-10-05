using System.Collections.Generic;
using System.Linq;

namespace TabML.Parser.AST
{
    class CapoDiscreteStringsSpecifierNode : CapoStringsSpecifierNode
    {
        public List<LiteralNode<int>> Strings { get; }
        public CapoDiscreteStringsSpecifierNode()
        {
            this.Strings = new List<LiteralNode<int>>();
        }

        public override int[] GetStringNumbers()
        {
            return this.Strings.Select(s => s.Value).ToArray();
        }

        public override IEnumerable<Node> Children => this.Strings;
    }
}
