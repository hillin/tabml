using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Document
{
    public class Staff
    {
        public List<Bar> Bars { get; }
        public StaffType Type { get; }

        public Staff(StaffType type)
        {
            this.Type = type;
            this.Bars = new List<Bar>();
        }
    }
}
