using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
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
