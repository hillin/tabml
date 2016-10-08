using System.Collections.Generic;
using TabML.Core.MusicTheory;

namespace TabML.Parser.AST
{
    class NoteValueNode : Node, IValueEquatable<NoteValueNode>
    {
        public LiteralNode<BaseNoteValue> Base { get; set; }
        public LiteralNode<NoteValueAugment> Augment { get; set; }
        public LiteralNode<int> Tuplet { get; set; }

        public override IEnumerable<Node> Children
        {
            get
            {
                yield return this.Base;
                if (this.Tuplet != null)
                    yield return this.Tuplet;

                yield return this.Augment;
            }
        }

        public NoteValue ToNoteValue()
        {
            return new NoteValue(this.Base.Value, this.Augment?.Value ?? NoteValueAugment.None, this.Tuplet?.Value);
        }

        public bool ValueEquals(NoteValueNode other)
        {
            if (other == null)
                return false;

            return ValueEquatable.ValueEquals(this.Base, other.Base)
                   && ValueEquatable.ValueEquals(this.Augment, other.Augment)
                   && ValueEquatable.ValueEquals(this.Tuplet, other.Tuplet);
        }
    }
}
