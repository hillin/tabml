using System.Collections.Generic;

namespace TabML.Parser.AST
{
    class SectionCommandletNode : CommandletNode
    {
        public LiteralNode<string> SectionName { get; set; }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get
            {
                yield return this.SectionName;
            }
        }
    }
}
