using System.Collections.Generic;
using TabML.Core.MusicTheory;

namespace TabML.Parser.AST
{
    class TempoCommandletNode : CommandletNode
    {
        public LiteralNode<BaseNoteValue> NoteValue { get; set; }
        public LiteralNode<int> Beats { get; set; }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get
            {
                if (this.NoteValue != null)
                    yield return this.NoteValue;

                yield return this.Beats;
            }
        }
    }
}
