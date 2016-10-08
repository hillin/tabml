using System.Collections.Generic;
using System.Diagnostics;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{

    [DebuggerDisplay("literal: {Value}")]
    class LiteralNode<T> : Node, IValueEquatable<LiteralNode<T>>
    {
        public T Value { get; set; }

        public override IEnumerable<Node> Children => null;

        public LiteralNode()
        {

        }

        public LiteralNode(T value, TextRange range)
        {
            this.Value = value;
            this.Range = range;
        }

        public bool ValueEquals(LiteralNode<T> other)
        {
            return other != null && this.Value.Equals(other.Value);
        }
    }
}
