using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class ChordCommandletNode : CommandletNode, IDocumentElementFactory<ChordDefinition>
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
            ChordDefinition definition;
            if (!this.ToDocumentElement(context, reporter, out definition))
                return false;

            using (var state = context.AlterDocumentState())
            {
                state.DefinedChords.Add(definition);
            }

            return true;
        }

        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out ChordDefinition element)
        {
            if (context.DocumentState.DefinedChords.Any(
                c => c.DisplayName.Equals(this.DisplayName.Value,
                                          StringComparison.InvariantCultureIgnoreCase)))
            {
                reporter.Report(ReportLevel.Warning, this.Range, Messages.Warning_ChordAlreadyDefined);
                element = null;
                return false;
            }

            ChordFingering chordFingering;
            if (!this.Fingering.ToDocumentElement(context, reporter, out chordFingering))
            {
                element = null;
                return false;
            }

            element = new ChordDefinition
            {
                Range = this.Range,
                DisplayName = this.DisplayName.Value,
                Name = this.Name?.Value,
                Fingering = chordFingering
            };

            return true;
        }
    }
}
