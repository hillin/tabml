using System.Collections.Generic;
using System.Linq;

namespace TabML.Parser.Document
{
    class Voice : Element
    {
        public List<Beat> Beats { get; }

        public Voice()
        {
            this.Beats = new List<Beat>();
        }
        public double GetDuration()
        {
            return this.Beats.Sum(n => n.GetDuration());
        }

        public void ClearRange()
        {
            this.Range = null;

            foreach (var beat in this.Beats)
                beat.ClearRange();
        }

        public Voice Clone()
        {
            var clone = new Voice
            {
                Range = this.Range
            };
            clone.Beats.AddRange(this.Beats.Select(b => b.Clone()));
            return clone;
        }
    }
}
