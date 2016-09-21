using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
{
    class ChordFingeringNode : Node
    {
        public List<LiteralNode<int>> Fingerings { get; }

        public ChordFingeringNode()
        {
            this.Fingerings = new List<LiteralNode<int>>();
        }
    }
}
