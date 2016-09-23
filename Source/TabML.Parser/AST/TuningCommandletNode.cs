using System.Collections.Generic;

namespace TabML.Parser.AST
{
    class TuningCommandletNode : CommandletNode, IRequireStringValidation
    {
        public LiteralNode<string> Name { get; set; }
        public List<PitchNode> StringTunings { get; set; }
    }
}
