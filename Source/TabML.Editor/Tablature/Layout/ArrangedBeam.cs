using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;

namespace TabML.Editor.Tablature.Layout
{
    class ArrangedBeam : IBeamElement
    {
        public List<IBeamElement> Elements { get; }
        public int? Tuplet { get; set; }

        public ArrangedBeam()
        {
            this.Elements = new List<IBeamElement>();
        }

        public PreciseDuration GetDuration()
        {
            return this.Elements.Sum(b => b.GetDuration());
        }
    }
}
