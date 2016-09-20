using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Core.Parsing.AST
{
    class BarNode : TopLevelNode
    {
        public OpenBarLine OpenLine { get; set; }
        public CloseBarLine CloseLine { get; set; }
        public RhythmNode Rhythm { get; set; }
        public LyricsNode Lyrics { get; set; }
    }
}
