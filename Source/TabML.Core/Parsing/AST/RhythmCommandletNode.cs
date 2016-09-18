using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
{
    class RhythmCommandletNode : CommandletNode
    {
        public RhythmNode RhythmNode { get; }

        public RhythmCommandletNode(RhythmNode rhythmNode)
        {
            this.RhythmNode = rhythmNode;
        }
    }
}
