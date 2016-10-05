using System.Collections.Generic;

namespace TabML.Parser.AST
{
    class StringsCommandletNode : CommandletNode
    {
        public LiteralNode<int> StringCount { get; set; }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get { yield return this.StringCount; }
        }
    }
}
