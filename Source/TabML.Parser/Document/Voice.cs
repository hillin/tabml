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
    }
}
