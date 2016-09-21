using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Core.Parsing.AST
{
    class StaffCommandletNode : CommandletNode
    {
        public LiteralNode<string> StaffName { get; set; }
        public LiteralNode<StaffType> StaffType { get; set; }
    }
}
