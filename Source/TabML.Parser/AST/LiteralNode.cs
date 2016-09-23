using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class LiteralNode<T> : Node
    {
        public T Value { get; set; }

        public LiteralNode()
        {

        }

        public LiteralNode(T value, TextRange range)
        {
            this.Value = value;
            this.Range = range;
        }
    }
}
