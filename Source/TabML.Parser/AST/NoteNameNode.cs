using TabML.Core.MusicTheory;

namespace TabML.Parser.AST
{
    class NoteNameNode : Node
    {
        public LiteralNode<BaseNoteName> BaseNoteName { get; set; }
        public LiteralNode<Accidental> Accidental { get; set; }
    }
}
