using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class ChordCommandletNode : CommandletNode
    {
        public LiteralNode<string> Name { get; set; }
        public LiteralNode<string> DisplayName { get; set; }
        public ChordFingeringNode Fingering { get; set; }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get
            {
                yield return this.Name;
                if (this.DisplayName != null)
                    yield return this.DisplayName;
                yield return this.Fingering;
            }
        }

        internal override bool Apply(TablatureContext context, IReporter reporter)
        {
            if (context.DocumentState.DefinedChords.Any(
                c => c.DisplayName.Value.Equals(this.DisplayName.Value,
                                                StringComparison.InvariantCultureIgnoreCase)))
            {
                reporter.Report(ReportLevel.Warning, this.Range, Messages.Warning_ChordAlreadyDefined);
                return true;
            }

            using (var state = context.AlterDocumentState())
            {
                state.DefinedChords.Add(this);
            }

            return true;
        }
        
    }
}
