using System.Collections.Generic;
using System.Linq;

namespace TabML.Core.Document
{
    public class RhythmSegment 
    {
        public List<RhythmUnit> Units { get; } = new List<RhythmUnit>();
        public ChordDefinition Chord { get; set; }

        public double GetDuration()
        {
            return this.Units.Sum(n => n.GetDuration());
        }
    }
}
