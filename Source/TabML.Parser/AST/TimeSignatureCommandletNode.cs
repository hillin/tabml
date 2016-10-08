using System.Collections.Generic;
using TabML.Core.MusicTheory;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class TimeSignatureCommandletNode : CommandletNode, IValueEquatable<TimeSignatureCommandletNode>
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

        public double GetDuration()
        {
            return this.NoteValue.Value.GetDuration() * this.Beats.Value;
        }

        public bool ValueEquals(TimeSignatureCommandletNode other)
        {
            if (other == null)
                return false;

            return this.Beats.Value == other.Beats.Value && this.NoteValue.Value == other.NoteValue.Value;
        }

        internal override bool Apply(TablatureContext context, IReporter reporter)
        {
            if (context.DocumentState.RhythmInstruction != null || context.DocumentState.BarAppeared)
            {
                reporter.Report(ReportLevel.Error, this.Range, Messages.Error_TimeInstructionAfterBarAppearedOrRhythmInstruction);
                return false;
            }

            if (context.DocumentState.Time != null && context.DocumentState.Time.ValueEquals(this))
            {
                reporter.Report(ReportLevel.Suggestion, this.Range, Messages.Suggestion_UselessTimeInstruction);
                return true;
            }

            using (var state = context.AlterDocumentState())
            {
                state.Time = this;
            }

            return true;
        }
    }
}
