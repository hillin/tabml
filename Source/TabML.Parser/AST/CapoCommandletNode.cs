using System;
using System.Collections.Generic;
using TabML.Core;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using TabML.Core.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class CapoCommandletNode : CommandletNode, IDocumentElementFactory<Capo>
    {
        public LiteralNode<int> Position { get; set; }
        public CapoStringsSpecifierNode StringsSpecifier { get; set; }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get
            {
                if (this.StringsSpecifier != null)
                    yield return this.StringsSpecifier;

                yield return this.Position;
            }
        }

        internal override bool Apply(TablatureContext context, IReporter reporter)
        {
            Capo capo;
            if (!this.ToDocumentElement(context, reporter, out capo))
                return false;

            using (var state = context.AlterDocumentState())
            {
                state.Capos.Add(capo);
                state.CapoFretOffsets = capo.OffsetFrets(state.CapoFretOffsets);
            }

            return true;
        }


        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out Capo element)
        {
            if (context.DocumentState.BarAppeared)
            {
                reporter.Report(ReportLevel.Error, this.Range, Messages.Error_CapoInstructionAfterBarAppeared);
                element = null;
                return false;
            }

            element = new Capo
            {
                Range = this.Range,
                CapoInfo =
                    new CapoInfo(this.Position.Value,
                                 this.StringsSpecifier?.GetStringNumbers() ?? CapoInfo.AffectAllStrings)
            };

            return true;
        }
    }
}
