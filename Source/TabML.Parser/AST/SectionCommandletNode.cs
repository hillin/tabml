using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class SectionCommandletNode : CommandletNode, IDocumentElementFactory<Section>
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
            Section section;
            if (!this.ToDocumentElement(context, reporter, out section))
                return false;

            using (var state = context.AlterDocumentState())
            {
                state.DefinedSections.Add(section);
                state.CurrentSection = section;
            }

            return true;
        }

        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out Section element)
        {
            if (context.DocumentState.DefinedSections.Any(this.ValueEquals))
            {
                reporter.Report(ReportLevel.Warning, this.Range, Messages.Warning_DuplicatedSectionName,
                                this.SectionName.Value);
                element = null;
                return true;
            }

            element = new Section
            {
                Name = this.SectionName.Value,
                Range = this.Range
            };

            return true;
        }

        private bool ValueEquals(Section section)
        {
            return section.Name.Equals(this.SectionName.Value, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
