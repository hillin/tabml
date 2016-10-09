using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser.Document
{
    abstract class RhythmSegmentBase : Element
    {
        public List<Voice> Voices { get; }
        protected RhythmSegmentBase()
        {
            this.Voices = new List<Voice>();
        }
        public double GetDuration()
        {
            return this.Voices.Max(v => v.GetDuration());
        }

    }
}
