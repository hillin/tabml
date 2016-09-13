using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Core.Parsing.AST
{
    class TempoCommandletNode : CommandletNode
    {
        public TempoSignature Tempo { get; }
        public TempoCommandletNode(TempoSignature tempo)
        {
            this.Tempo = tempo;
        }
    }
}
