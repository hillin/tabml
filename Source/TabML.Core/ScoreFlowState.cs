using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public class ScoreFlowState
    {
        public NoteName Key { get; set; }
        public TimeSignature TimeSignature { get; set; }
        public int Tempo { get; set; }
        public int Capo { get; set; }

    }
}
