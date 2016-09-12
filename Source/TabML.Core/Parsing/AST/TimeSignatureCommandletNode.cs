using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Core.Parsing.AST
{
    class TimeSignatureCommandletNode : CommandletNode
    {
        public TimeSignature TimeSignature { get; }
        public TimeSignatureCommandletNode(TimeSignature timeSignature)
        {
            this.TimeSignature = timeSignature;
        }
    }
}
