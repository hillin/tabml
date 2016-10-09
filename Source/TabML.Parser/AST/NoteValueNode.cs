using System.Collections.Generic;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;

namespace TabML.Parser.AST
{
    class NoteValueNode : Node
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
        
    }
}
