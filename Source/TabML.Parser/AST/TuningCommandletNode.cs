using System.Collections.Generic;
using System.Linq;
using TabML.Core.Logging;
using TabML.Core.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class TuningCommandletNode : CommandletNode, IDocumentElementFactory<TuningSignature>
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

            if (context.DocumentState.TuningSignature != null)
            {
                reporter.Report(ReportLevel.Warning, this.Range, Messages.Warning_RedefiningTuningInstruction);
                return false;
            }

            TuningSignature tuning;
            if (!this.ToDocumentElement(context, reporter, out tuning))
                return false;

            using (var state = context.AlterDocumentState())
            {
                state.TuningSignature = tuning;
            }

            return true;
        }

        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out TuningSignature element)
        {
            element = new TuningSignature
            {
                Range = this.Range,
                Tuning =
                    new Core.MusicTheory.Tuning(this.Name?.Value, this.StringTunings.Select(t => t.ToPitch()).ToArray())
            };

            return true;
        }
    }
}
