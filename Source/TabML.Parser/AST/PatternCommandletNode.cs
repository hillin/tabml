using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser.AST
{
    partial class PatternCommandletNode : CommandletNode
    {
        public TemplateBarsNode TemplateBars { get; set; }
        public InstanceBarsNode InstanceBars { get; set; }
        

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get
            {
                yield return this.TemplateBars;
                yield return this.InstanceBars;
            }
        }
    }
}
