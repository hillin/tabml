using System.Collections.Generic;
using System.Linq;

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
