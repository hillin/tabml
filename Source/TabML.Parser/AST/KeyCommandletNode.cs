using System.Collections.Generic;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using TabML.Core.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class KeyCommandletNode : CommandletNode, IDocumentElementFactory<KeySignature>
    {
        public NoteNameNode Key { get; set; }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get
            {
                yield return this.Key;
            }
        }

        internal override bool Apply(TablatureContext context, IReporter reporter)
        {
            KeySignature key;
            if (!this.ToDocumentElement(context, reporter, out key))
                return false;

            using (var state = context.AlterDocumentState())
                state.KeySignature = key;

            return true;
        }

        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out KeySignature element)
        {
            var noteName = this.Key.ToNoteName();
            if (context.DocumentState.KeySignature != null && context.DocumentState.KeySignature.Key == noteName)
            {
                reporter.Report(ReportLevel.Suggestion, this.Range, Messages.Suggestion_RedundantKeySignature);
                element = null;
                return false;
            }

            element = new KeySignature
            {
                Range = this.Range,
                Key = noteName
            };

            return true;
        }
    }
}
