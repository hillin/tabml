using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Parser.Document;

namespace TabML.Editor.Tablature.Layout
{
    class ArrangedBeam
    {
        public List<Beat> Beats { get; }
        public int? Tuplet { get; set; }

        public ArrangedBeam()
        {
            this.Beats = new List<Beat>();
        }
    }
}
