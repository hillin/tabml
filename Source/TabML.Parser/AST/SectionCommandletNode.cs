using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class SectionCommandletNode : CommandletNode
    {
        public LiteralNode<string> SectionName { get; set; }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get
            {
                yield return this.SectionName;
            }
        }

        internal override bool Apply(TablatureContext context, IReporter reporter)
        {
            if (context.DocumentState.DefinedSections.Any(
                    s =>
                        s.SectionName.Value.Equals(this.SectionName.Value,
                                                   StringComparison.InvariantCultureIgnoreCase)))
            {
                reporter.Report(ReportLevel.Warning, this.Range, Messages.Warning_DuplicatedSectionName, this.SectionName.Value);
                return true;
            }

            using (var state = context.AlterDocumentState())
            {
                state.DefinedSections.Add(this);
                state.CurrentSection = this;
            }

            return true;
        }
    }
}
