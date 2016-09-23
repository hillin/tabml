namespace TabML.Parser.AST
{
    class PitchNode : Node
    {
        public NoteNameNode NoteName { get; set; }
        public LiteralNode<int> OctaveGroup { get; set; }
    }
}
