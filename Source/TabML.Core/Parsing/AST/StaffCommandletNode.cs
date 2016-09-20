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
        public string StaffName { get; }
        public StaffType? StaffType { get; }

        public StaffCommandletNode(string staffName, StaffType? staffType = null)
        {
            this.StaffName = staffName;
            this.StaffType = staffType;
        }
    }
}
