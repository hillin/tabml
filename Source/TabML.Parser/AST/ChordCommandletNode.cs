using System.Collections.Generic;

namespace TabML.Parser.AST
{
    class ChordCommandletNode : CommandletNode, IRequireStringValidation
    {
        public LiteralNode<string> Name { get; set; }
        public LiteralNode<string> DisplayName { get; set; }
        public ChordFingeringNode Fingering { get; set; }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get
            {
                yield return this.Name;
                if (this.DisplayName != null)
                    yield return this.DisplayName;
                yield return this.Fingering;
            }
        }
    }
}
