using System.Collections.Generic;
using TabML.Core.Logging;
using TabML.Core.Document;
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

        internal override bool Apply(TablatureContext context, ILogger logger)
        {
            if (context.DocumentState.RhythmTemplate != null && this.Equals(context.DocumentState.RhythmTemplate))
            {
                logger.Report(LogLevel.Suggestion, this.Range, Messages.Suggestion_UselessRhythmInstruction);
                return true;
            }

            context.CurrentBar = null;  // todo: this is ugly, refactor it

            RhythmTemplate rhythmTemplate;
            if (!this.TemplateNode.ToDocumentElement(context, logger, out rhythmTemplate))
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
