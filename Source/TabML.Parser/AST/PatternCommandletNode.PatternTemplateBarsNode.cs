using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser.AST
{
    partial class PatternCommandletNode
    {
        public class TemplateBarsNode : Node
        {
            public List<BarNode> Bars { get; }
            public override IEnumerable<Node> Children => this.Bars;

            public TemplateBarsNode()
            {
                this.Bars = new List<BarNode>();
            }
        }
    }
}