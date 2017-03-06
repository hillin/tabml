using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Core.Logging;
using TabML.Core.Document;
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

        internal override bool Apply(TablatureContext context, ILogger logger)
        {
            Section section;
            if (!this.ToDocumentElement(context, logger, out section))
                return false;

            using (var state = context.AlterDocumentState())
            {
                state.DefinedSections.Add(section);
                state.CurrentSection = section;
            }

            return true;
        }

        public bool ToDocumentElement(TablatureContext context, ILogger logger, out Section element)
        {
            if (context.DocumentState.DefinedSections.Any(this.ValueEquals))
            {
                logger.Report(LogLevel.Warning, this.Range, Messages.Warning_DuplicatedSectionName,
                                this.SectionName.Value);
                element = null;
                return false;
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
