using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser.AST
{
    class PatternCommandletNode : CommandletNode
    {
        public List<BarNode> PatternBars { get; }
        public List<BarNode> InstanceBars { get; }

        public PatternCommandletNode()
        {
            this.PatternBars = new List<BarNode>();
            this.InstanceBars = new List<BarNode>();
        }
    }
}
