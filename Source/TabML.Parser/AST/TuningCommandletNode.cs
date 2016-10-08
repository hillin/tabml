using System.Collections.Generic;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class TuningCommandletNode : CommandletNode
    {
        public LiteralNode<string> Name { get; set; }
        public List<PitchNode> StringTunings { get; }

        public TuningCommandletNode()
        {
            this.StringTunings = new List<PitchNode>();
        }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get
            {
                if (this.Name != null)
                    yield return this.Name;

                foreach (var node in this.StringTunings)
                    yield return node;
            }
        }

        internal override bool Apply(TablatureContext context, IReporter reporter)
        {
            if (context.DocumentState.BarAppeared)
            {
                reporter.Report(ReportLevel.Error, this.Range, Messages.Error_TuningInstructionAfterBarAppeared);
                return false;
            }

            if (context.DocumentState.Tuning != null)
            {
                reporter.Report(ReportLevel.Warning, this.Range, Messages.Warning_RedefiningTuningInstruction);
                return true;
            }

            using (var state = context.AlterDocumentState())
            {
                state.Tuning = this;
            }

            return true;
        }
    }
}
