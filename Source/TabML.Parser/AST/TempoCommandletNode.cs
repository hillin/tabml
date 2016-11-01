using System.Collections.Generic;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using TabML.Core.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class TempoCommandletNode : CommandletNode, IDocumentElementFactory<TempoSignature>
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
            TempoSignature tempo;
            if (!this.ToDocumentElement(context, reporter, out tempo))
                return false;

            using (var state = context.AlterDocumentState())
            {
                state.TempoSignature = tempo;
            }

            return true;
        }

        public bool ValueEquals(TempoSignature other)
        {
            if (other == null)
                return false;

            if (this.NoteValue == null)
            {
                if (other.Tempo.NoteValue != BaseNoteValue.Quater)
                    return false;
            }
            else if (this.NoteValue.Value != other.Tempo.NoteValue)
                return false;

            return this.Beats.Value == other.Tempo.Beats;
        }

        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out TempoSignature element)
        {
            if (this.ValueEquals(context.DocumentState.TempoSignature))
            {
                reporter.Report(ReportLevel.Suggestion, this.Range, Messages.Suggestion_UselessTempoInstruction);
                element = null;
                return false;
            }

            element = new TempoSignature
            {
                Range = this.Range,
                Tempo = new Tempo(this.Beats.Value, this.NoteValue?.Value ?? BaseNoteValue.Quater)
            };

            return true;
        }
    }
}
