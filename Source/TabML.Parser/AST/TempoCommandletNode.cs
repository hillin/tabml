using TabML.Core.MusicTheory;

namespace TabML.Parser.AST
{
    class TempoCommandletNode : CommandletNode
    {
        public LiteralNode<BaseNoteValue> NoteValue { get; set; }
        public LiteralNode<int> Beats { get; set; }

    }
}
