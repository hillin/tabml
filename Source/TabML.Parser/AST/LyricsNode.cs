using System.Collections.Generic;

namespace TabML.Parser.AST
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
