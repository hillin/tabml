using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser.Document
{
    class Lyrics : Element
    {
        public List<LyricsSegment> Segments { get; }

        public Lyrics()
        {
            this.Segments = new List<LyricsSegment>();
        }
    }
}
