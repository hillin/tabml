using System.Collections.Generic;
using System.Linq;
using TabML.Core.Logging;
using TabML.Core.Document;
using TabML.Core.String;
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

        internal override bool Apply(TablatureContext context, ILogger logger)
        {
            if (context.DocumentState.BarAppeared)
            {
                logger.Report(LogLevel.Error, this.Range, Messages.Error_TuningInstructionAfterBarAppeared);
                return false;
            }

            if (context.DocumentState.TuningSignature != null)
            {
                logger.Report(LogLevel.Warning, this.Range, Messages.Warning_RedefiningTuningInstruction);
                return false;
            }

            TuningSignature tuning;
            if (!this.ToDocumentElement(context, logger, out tuning))
                return false;

            using (var state = context.AlterDocumentState())
            {
                state.TuningSignature = tuning;
            }

            return true;
        }

        public bool ToDocumentElement(TablatureContext context, ILogger logger, out TuningSignature element)
        {
            element = new TuningSignature
            {
                Range = this.Range,
                Tuning =
                    new Tuning(this.Name?.Value, this.StringTunings.Select(t => t.ToPitch()).ToArray())
            };

            return true;
        }
    }
}
