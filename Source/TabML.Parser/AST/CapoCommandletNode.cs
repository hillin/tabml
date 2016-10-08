using System;
using System.Collections.Generic;
using TabML.Core;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class CapoCommandletNode : CommandletNode, IRequireStringValidation
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
            if (context.DocumentState.BarAppeared)
            {
                reporter.Report(ReportLevel.Error, this.Range, Messages.Error_CapoInstructionAfterBarAppeared);
                return false;
            }

            using (var state = context.AlterDocumentState())
            {
                state.CapoInstructions.Add(this);
                state.CapoFretOffsets = this.OffsetFrets(state.CapoFretOffsets);
            }


            return true;
        }

        private int[] OffsetFrets(int[] capoFretOffsets)
        {
            if (capoFretOffsets == null)
                capoFretOffsets = new int[Defaults.Strings];

            foreach (var stringIndex in this.StringsSpecifier.GetStringNumbers())
                capoFretOffsets[stringIndex - 1] = Math.Max(capoFretOffsets[stringIndex - 1], this.Position.Value);

            return capoFretOffsets;
        }
    }
}
