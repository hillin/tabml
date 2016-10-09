using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Parser.Document;

namespace TabML.Parser.AST
{
    class TiedNode : Node
    {
        public override IEnumerable<Node> Children => null;
    }
}
