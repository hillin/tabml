using System.Collections.Generic;
using TabML.Core.MusicTheory;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class TempoCommandletNode : CommandletNode, IValueEquatable<TempoCommandletNode>
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

        internal override bool Apply(TablatureContext context, IReporter reporter)
        {
            if (context.DocumentState.Tempo?.ValueEquals(this) == true)
            {
                reporter.Report(ReportLevel.Suggestion, this.Range, Messages.Suggestion_UselessTempoInstruction);
                return true;
            }

            using (var state = context.AlterDocumentState())
            {
                state.Tempo = this;
            }

            return true;
        }

        public bool ValueEquals(TempoCommandletNode other)
        {
            if (this.NoteValue == null)
            {
                if (other.NoteValue != null && other.NoteValue.Value != BaseNoteValue.Quater)
                    return false;
            }
            else if (other.NoteValue == null)
            {
                if (this.NoteValue.Value != BaseNoteValue.Quater)
                    return false;
            }
            else if (this.NoteValue.Value != other.NoteValue.Value)
                return false;

            return this.Beats.Value == other.Beats.Value;
        }
    }
}
