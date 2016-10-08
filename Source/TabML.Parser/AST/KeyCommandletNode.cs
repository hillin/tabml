using System.Collections.Generic;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class KeyCommandletNode : CommandletNode
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
            if (context.DocumentState.Key?.Key.ValueEquals(this.Key) == true)
            {
                reporter.Report(ReportLevel.Suggestion, this.Range, Messages.Suggestion_RedundantKeySignature);
                return true;
            }

            using (var state = context.AlterDocumentState())
                state.Key = this;

            return true;
        }
    }
}
