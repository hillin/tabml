using System.Collections.Generic;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class RhythmCommandletNode : CommandletNode
    {
        public RhythmTemplateNode TemplateNode { get; }

        public RhythmCommandletNode(RhythmTemplateNode templateNode)
        {
            this.TemplateNode = templateNode;
        }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get { yield return this.TemplateNode; }
        }

        internal override bool Apply(TablatureContext context, IReporter reporter)
        {
            if (context.DocumentState.RhythmTemplate != null && this.Equals(context.DocumentState.RhythmTemplate))
            {
                reporter.Report(ReportLevel.Suggestion, this.Range, Messages.Suggestion_UselessRhythmInstruction);
                return true;
            }

            RhythmTemplate rhythmTemplate;
            if (!this.TemplateNode.ToDocumentElement(context, reporter, out rhythmTemplate))
                return false;

            using (var state = context.AlterDocumentState())
            {
                state.RhythmTemplate = rhythmTemplate;
            }

            return true;
        }

        public bool Equals(RhythmTemplate other)
        {
            return this.TemplateNode.ValueEquals(other);
        }

    }
}
