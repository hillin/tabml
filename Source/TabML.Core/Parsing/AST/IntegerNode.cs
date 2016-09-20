using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
{
    class IntegerNode : Node
    {
        public int Value { get; set; }

        public IntegerNode()
        {

        }

        public IntegerNode(int value, TextRange range)
        {
            this.Value = value;
            this.Range = range;
        }
    }
}
