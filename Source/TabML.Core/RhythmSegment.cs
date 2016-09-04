using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public class RhythmSegment 
    {
        public List<RhythmUnit> Units { get; } = new List<RhythmUnit>();
        

        public double GetDuration()
        {
            return this.Units.Sum(n => n.GetDuration());
        }
    }
}
