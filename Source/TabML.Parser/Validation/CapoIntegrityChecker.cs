using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Logging;
using TabML.Parser.Parsing;

namespace TabML.Parser.Validation
{
    class CapoIntegrityChecker : IntegrityChecker
    {
        public override bool CheckIntegrity(TablatureContext context, ILogger logger)
        {
            foreach (var capo in
                context.DocumentState
                       .Capos
                       .Where(capo => capo.CapoInfo.AffectedStrings
                                          .All(n => capo.CapoInfo.Position < context.DocumentState.CapoFretOffsets[n - 1]))
                )
            {
                logger.Report(LogLevel.Suggestion, capo.Range, Messages.Suggestion_UselessCapoInstruction);
            }

            return true;
        }
    }
}
