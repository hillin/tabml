using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser.AST
{
    class TiedNode : Node, IValueEquatable<TiedNode>
    {
        public override IEnumerable<Node> Children => null;
        public bool ValueEquals(TiedNode other)
        {
            return other != null;
        }
    }
}
