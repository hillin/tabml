using System.Collections.Generic;

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
