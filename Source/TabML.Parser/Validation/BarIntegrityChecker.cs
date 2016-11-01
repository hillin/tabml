using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Logging;
using TabML.Parser.Parsing;

namespace TabML.Parser.Validation
{
    class BarIntegrityChecker : IntegrityChecker
    {
        [SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
        public override bool CheckIntegrity(TablatureContext context, ILogger logger)
        {
            if (context.Bars.Count > 0)
            {
                var firstBar = context.Bars[0];
                if (firstBar.OpenLine == null)
                {
                    logger.Report(LogLevel.Hint, firstBar.Range.Value.From.AsRange(), Messages.Hint_FirstOpenBarLineMissing);
                }

                for (var i = 1; i < context.Bars.Count - 1; ++i)
                {
                    if (context.Bars[i].CloseLine == null && context.Bars[i + 1].OpenLine == null)
                    {
                        logger.Report(LogLevel.Warning, context.Bars[i].Range.Value.To.AsRange(), Messages.Warning_BarLineMissing);
                    }
                }

                var lastBar = context.Bars[context.Bars.Count - 1];
                if (lastBar.CloseLine == null)
                {
                    logger.Report(LogLevel.Hint, lastBar.Range.Value.To.AsRange(), Messages.Hint_LastCloseBarLineMissing);
                }
            }

            return true;
        }

    }
}
