using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
{
    class TablatureNode : Node
    {
        public List<TopLevelNode> Nodes { get; }

        public TablatureNode()
        {
            this.Nodes = new List<TopLevelNode>();
        }
    }
}
