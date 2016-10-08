using System.Collections.Generic;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class RhythmCommandletNode : CommandletNode, IValueEquatable<RhythmCommandletNode>
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
            if (context.DocumentState.RhythmInstruction != null && context.DocumentState.RhythmInstruction.ValueEquals(this))
            {
                reporter.Report(ReportLevel.Suggestion, this.Range, Messages.Suggestion_UselessRhythmInstruction);
                return true;
            }

            Rhythm rhythm;
            if (!this.TemplateNode.ToDocumentElement(context, reporter, out rhythm))
                return false;

            using (var state = context.AlterDocumentState())
            {
                state.RhythmTemplate = rhythm;
                state.RhythmInstruction = this;
            }

            return true;
        }

        public bool ValueEquals(RhythmCommandletNode other)
        {
            return other != null && this.TemplateNode.ValueEquals(other.TemplateNode);
        }
        
    }
}
