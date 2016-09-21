using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
{
    class StringsCommandletNode : CommandletNode
    {
        public LiteralNode<int> StringCount { get; set; }
    }
}
