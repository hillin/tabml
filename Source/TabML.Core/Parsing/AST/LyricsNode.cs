using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
{
    class LyricsNode : Node
    {
        public List<LyricsSegmentNode> LyricsSegments { get; }

        public LyricsNode()
        {
            this.LyricsSegments = new List<LyricsSegmentNode>();
        }
    }
}
