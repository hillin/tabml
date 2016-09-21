using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
{
    class SectionCommandletNode : CommandletNode
    {
        public LiteralNode<string> SectionName { get; set; }        
    }
}
