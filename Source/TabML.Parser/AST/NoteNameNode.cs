using System.Collections.Generic;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;

namespace TabML.Parser.AST
{
    class NoteNameNode : Node
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
        
        public NoteName ToNoteName()
        {
            return new NoteName(this.BaseNoteName.Value, this.Accidental.Value);
        }
    }
}
