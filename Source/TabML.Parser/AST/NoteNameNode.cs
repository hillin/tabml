using System.Collections.Generic;
using TabML.Core.MusicTheory;

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
    }
}
