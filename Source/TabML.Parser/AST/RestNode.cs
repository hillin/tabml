using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser.AST
{
    class RestNode : Node, IValueEquatable<RestNode>
    {
        public override IEnumerable<Node> Children => null;
        public bool ValueEquals(RestNode other)
        {
            return other != null;
        }
    }
}
