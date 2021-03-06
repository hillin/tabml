﻿using System;
using System.Collections.Generic;
using TabML.Core;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using TabML.Core.Document;
using TabML.Core.MusicTheory.String.Plucked;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class CapoCommandletNode : CommandletNode
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

        internal override bool Apply(TablatureContext context, ILogger logger)
        {
            Capo capo;
            if (!this.ToDocumentElement(context, logger, out capo))
                return false;

            using (var state = context.AlterDocumentState())
            {
                state.Capos.Add(capo);
                state.CapoFretOffsets = capo.OffsetFrets(state.CapoFretOffsets);
            }

            return true;
        }


        public bool ToDocumentElement(TablatureContext context, ILogger logger, out Capo element)
        {
            if (context.DocumentState.BarAppeared)
            {
                logger.Report(LogLevel.Error, this.Range, Messages.Error_CapoInstructionAfterBarAppeared);
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
