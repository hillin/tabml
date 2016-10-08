using System.Collections.Generic;
using TabML.Core.MusicTheory;

namespace TabML.Parser.AST
{
    class NoteNameNode : Node, IValueEquatable<NoteNameNode>
    {
        public LiteralNode<BaseNoteName> BaseNoteName { get; set; }
        public LiteralNode<Accidental> Accidental { get; set; }

        public override IEnumerable<Node> Children
        {
            get
            {
                yield return this.BaseNoteName;
                if (this.Accidental != null)
                    yield return this.Accidental;
            }
        }

        public bool ValueEquals(NoteNameNode other)
        {
            if (this.BaseNoteName.Value != other?.BaseNoteName.Value)
                return false;

            if (this.Accidental == null)
            {
                return other.Accidental == null;
            }

            return this.Accidental.Value == other.Accidental?.Value;
        }
    }
}
