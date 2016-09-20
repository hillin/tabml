using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Document
{
    public class RhythmVoice
    {
        public List<RhythmUnit> Units { get; }

        public RhythmVoice()
        {
            this.Units = new List<RhythmUnit>();
        }
        public double GetDuration()
        {
            return this.Units.Sum(n => n.GetDuration());
        }
    }
}
