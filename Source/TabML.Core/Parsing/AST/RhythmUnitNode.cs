using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Core.Parsing.AST
{
    class RhythmUnitNode : Node
    {
        public RhythmUnit RhythmUnit { get; }

        public RhythmUnitNode(RhythmUnit rhythmUnit)
        {
            this.RhythmUnit = rhythmUnit;
        }
        
    }
}
