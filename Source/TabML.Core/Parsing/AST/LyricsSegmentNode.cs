using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
{
    class LyricsSegmentNode : Node
    {
        public string LyricsSegment { get; set; }

        public LyricsSegmentNode()
        {
            
        }

        public LyricsSegmentNode(string lyricsSegment)
        {
            this.LyricsSegment = lyricsSegment;
        }
    }

}
