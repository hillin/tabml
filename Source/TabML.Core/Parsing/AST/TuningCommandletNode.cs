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
        public LiteralNode<string> Name { get; set; }
        public List<PitchNode> StringTunings { get; set; }
    }
}
