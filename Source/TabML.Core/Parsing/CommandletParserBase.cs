using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing
{
    abstract class CommandletParserBase : ParserBase<CommandletNode>
    {
        public LiteralNode<string> CommandletNameNode { get; set; }
    }
}
