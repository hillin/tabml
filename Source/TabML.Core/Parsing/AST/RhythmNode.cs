using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
{
    class RhythmNode : Node
    {
        public List<RhythmUnitNode> Units { get; }

        public RhythmNode()
        {
            this.Units = new List<RhythmUnitNode>();
        }
    }
}
