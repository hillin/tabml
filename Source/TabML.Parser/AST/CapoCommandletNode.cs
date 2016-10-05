using System.Collections.Generic;

namespace TabML.Parser.AST
{
    class CapoCommandletNode : CommandletNode, IRequireStringValidation
    {
        public LiteralNode<int> Position { get; set; }
        public CapoStringsSpecifierNode StringsSpecifier { get; set; }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get
            {
                if (this.StringsSpecifier != null)
                    yield return this.StringsSpecifier;

                yield return this.Position;
            }
        }
    }
}
