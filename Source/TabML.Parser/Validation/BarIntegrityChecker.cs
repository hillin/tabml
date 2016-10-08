using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Parser.Parsing;

namespace TabML.Parser.Validation
{
    class BarIntegrityChecker : IntegrityChecker
    {
        [SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
        public override bool CheckIntegrity(TablatureContext context, IReporter reporter)
        {
            if (context.Bars.Count > 0)
            {
                var firstBar = context.Bars[0];
                if (firstBar.OpenLine == null)
                {
                    reporter.Report(ReportLevel.Hint, firstBar.Range.Value.From.AsRange(), Messages.Hint_FirstOpenBarLineMissing);
                }

                for (var i = 1; i < context.Bars.Count - 1; ++i)
                {
                    if (context.Bars[i].CloseLine == null && context.Bars[i + 1].OpenLine == null)
                    {
                        reporter.Report(ReportLevel.Warning, context.Bars[i].Range.Value.To.AsRange(), Messages.Warning_BarLineMissing);
                    }
                }

                var lastBar = context.Bars[context.Bars.Count - 1];
                if (lastBar.CloseLine == null)
                {
                    reporter.Report(ReportLevel.Hint, lastBar.Range.Value.To.AsRange(), Messages.Hint_LastCloseBarLineMissing);
                }
            }

            return true;
        }

    }
}
