using System.Collections.Generic;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using TabML.Core.Document;
using TabML.Parser.Parsing;

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

        public bool ValueEquals(TimeSignature other)
        {
            if (other == null)
                return false;

            return this.Beats.Value == other.Time.Beats && this.NoteValue.Value == other.Time.NoteValue;
        }

        internal override bool Apply(TablatureContext context, ILogger logger)
        {
            TimeSignature time;
            if (!this.ToDocumentElement(context, logger, out time))
                return false;

            using (var state = context.AlterDocumentState())
            {
                state.TimeSignature = time;
            }

            return true;
        }

        public bool ToDocumentElement(TablatureContext context, ILogger logger, out TimeSignature element)
        {
            if (context.DocumentState.RhythmTemplate != null || context.DocumentState.BarAppeared)
            {
                logger.Report(LogLevel.Error, this.Range, Messages.Error_TimeInstructionAfterBarAppearedOrRhythmInstruction);
                element = null;
                return false;
            }

            if (context.DocumentState.TimeSignature != null && this.ValueEquals(context.DocumentState.TimeSignature))
            {
                logger.Report(LogLevel.Suggestion, this.Range, Messages.Suggestion_UselessTimeInstruction);
                element = null;
                return false;
            }

            element = new TimeSignature
            {
                Range = this.Range,
                Time = new Time(this.Beats.Value, this.NoteValue.Value)
            };

            return true;
        }
    }
}
