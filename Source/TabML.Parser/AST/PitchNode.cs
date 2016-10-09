using System.Collections.Generic;
using TabML.Core.MusicTheory;

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

        public Pitch ToPitch()
        {
            return new Pitch(this.NoteName.ToNoteName(), this.OctaveGroup?.Value ?? -1);
        }
    }
}
