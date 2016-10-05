using System.Collections.Generic;
using TabML.Core.MusicTheory;

namespace TabML.Parser.AST
{
    class RhythmUnitNoteNode : Node
    {
        public LiteralNode<int> String { get; set; }
        public LiteralNode<int> Fret { get; set; }
        public LiteralNode<PreNoteConnection> PreConnection { get; set; }
        public LiteralNode<PostNoteConnection> PostConnection { get; set; }

        public override IEnumerable<Node> Children
        {
            get
            {
                if (this.PreConnection != null)
                    yield return this.PreConnection;

                if (this.String != null)
                    yield return this.String;

                if (this.Fret != null)
                    yield return this.Fret;

                if (this.PostConnection != null)
                    yield return this.PostConnection;
            }
        }
    }
}
