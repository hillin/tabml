using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
{
    class RhythmSegmentNode : RhythmSegmentNodeBase
    {
        public string ChordName { get; set; }
        public int[] Fingering { get; set; }
    }
}
