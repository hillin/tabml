using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
{
    class StringNode : Node
    {
        public string Value { get; set; }

        public StringNode()
        {

        }

        public StringNode(string value, TextRange range)
        {
            this.Value = value;
            this.Range = range;
        }
    }
}
