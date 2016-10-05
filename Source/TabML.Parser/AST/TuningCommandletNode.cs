using System.Collections.Generic;

namespace TabML.Parser.AST
{
    class TuningCommandletNode : CommandletNode, IRequireStringValidation
    {
        public LiteralNode<string> Name { get; set; }
        public List<PitchNode> StringTunings { get; }

        public TuningCommandletNode()
        {
            this.StringTunings = new List<PitchNode>();
        }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get
            {
                if (this.Name != null)
                    yield return this.Name;

                foreach (var node in this.StringTunings)
                    yield return node;
            }
        }
    }
}
