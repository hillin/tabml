using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
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
    }
}
