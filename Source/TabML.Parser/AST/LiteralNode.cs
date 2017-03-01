using System.Collections.Generic;
using System.Diagnostics;
using TabML.Core;
using TabML.Core.Document;
using TabML.Core.Parsing;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{

    [DebuggerDisplay("literal: {Value}")]
    class LiteralNode<T> : Node
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
        
    }
}
