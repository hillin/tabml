using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Editor.Tablature.Layout
{
    class ArrangedBar
    {
        public ArrangedBarVoice TrebleVoice { get; set; }
        public ArrangedBarVoice BassVoice { get; set; }
        public List<ArrangedBarColumn> Columns { get; }

        public ArrangedBar()
        {
            
            this.Columns = new List<ArrangedBarColumn>();
        }
    }
}
