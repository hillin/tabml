using System.Collections.Generic;

namespace TabML.Parser.AST
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
