using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Core.Parsing.AST
{
    class TuningCommandletNode : CommandletNode, IRequireStringValidation
    {
        public Tuning Tuning { get; }

        public TuningCommandletNode(Tuning tuning)
        {
            this.Tuning = tuning;
        }

    }
}
