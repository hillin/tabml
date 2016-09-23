using TabML.Core.MusicTheory;

namespace TabML.Parser.AST
{
    class RhythmUnitNoteNode : Node
    {
        public LiteralNode<int> String { get; set; }
        public LiteralNode<int> Fret { get; set; }
        public LiteralNode<PreNoteConnection> PreConnection { get; set; }
        public LiteralNode<PostNoteConnection> PostConnection { get; set; }
    }
}
