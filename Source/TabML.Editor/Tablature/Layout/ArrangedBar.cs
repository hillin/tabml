using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Editor.Tablature.Layout
{
    class ArrangedBar
    {
        public List<ArrangedBarVoice> Voices { get; }
        public List<ArrangedBarColumn> Columns { get; }

        public ArrangedBar()
        {
            this.Voices = new List<ArrangedBarVoice>();
            this.Columns = new List<ArrangedBarColumn>();
        }
    }
}
