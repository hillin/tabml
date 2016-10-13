using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Editor.Tablature.Layout
{
    class ArrangedBarVoice
    {
        public List<ArrangedBeam> Beams { get; }

        public ArrangedBarVoice()
        {
            this.Beams = new List<ArrangedBeam>();
        }
    }
}
