using System.Collections.Generic;
using System.Linq;

namespace TabML.Core.Document
{
    public class Voice
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
    }
}
