using System.Collections.Generic;
using TabML.Core.MusicTheory;

namespace TabML.Parser.AST
{
    class TimeSignatureCommandletNode : CommandletNode
    {
        public LiteralNode<int> Beats { get; set; }
        public LiteralNode<BaseNoteValue> NoteValue { get; set; }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get
            {
                yield return this.Beats;
                yield return this.NoteValue;
            }
        }
    }
}
