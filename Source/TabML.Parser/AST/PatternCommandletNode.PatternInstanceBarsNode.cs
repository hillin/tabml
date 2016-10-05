using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser.AST
{
    partial class PatternCommandletNode
    {
        public class InstanceBarsNode : Node
        {
            public List<BarNode> Bars { get; }
            public override IEnumerable<Node> Children => this.Bars;

            public InstanceBarsNode()
            {
                this.Bars = new List<BarNode>();
            }
        }
    }
}