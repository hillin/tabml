using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class LyricsSegmentNode : Node
    {
        public LiteralNode<string> LyricsSegment { get; set; }

        public LyricsSegmentNode()
        {
            
        }

        public LyricsSegmentNode(string lyricsSegment, TextRange range)
        {
            this.LyricsSegment = new LiteralNode<string>(lyricsSegment, range);
            this.Range = range;
        }
    }

}
