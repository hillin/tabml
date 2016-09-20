using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
{
    class RhythmVoiceNode : Node
    {
        public List<RhythmUnitNode> Units { get; }

        public RhythmVoiceNode()
        {
            this.Units = new List<RhythmUnitNode>();
        }
    }
}
