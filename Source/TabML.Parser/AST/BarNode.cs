using TabML.Core.Document;

namespace TabML.Parser.AST
{
    class BarNode : TopLevelNode
    {
        public LiteralNode<OpenBarLine> OpenLine { get; set; }
        public LiteralNode<CloseBarLine> CloseLine { get; set; }
        public RhythmNode Rhythm { get; set; }
        public LyricsNode Lyrics { get; set; }
    }
}
