using System.Collections.Generic;

namespace TabML.Parser.AST
{
    class PitchNode : Node
    {
        public NoteNameNode NoteName { get; set; }
        public LiteralNode<int> OctaveGroup { get; set; }

        public override IEnumerable<Node> Children
        {
            get
            {
                yield return this.NoteName;
                if (this.OctaveGroup != null)
                    yield return this.OctaveGroup;
            }
        }
    }
}
