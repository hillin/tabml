using System.Collections.Generic;
using System.Diagnostics;

namespace TabML.Parser.AST
{
    [DebuggerDisplay("Tablature")]
    class TablatureNode : Node
    {
        public List<TopLevelNode> Nodes { get; }
        public override IEnumerable<Node> Children => this.Nodes;

        public TablatureNode()
        {
            this.Nodes = new List<TopLevelNode>();
        }

    }
}
