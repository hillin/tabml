using TabML.Core.MusicTheory;

namespace TabML.Parser.AST
{
    class NoteValueNode : Node
    {
        public LiteralNode<BaseNoteValue> Base { get; set; }
        public LiteralNode<NoteValueAugment> Augment { get; set; }
        public LiteralNode<int> Tuplet { get; set; }
    }
}
