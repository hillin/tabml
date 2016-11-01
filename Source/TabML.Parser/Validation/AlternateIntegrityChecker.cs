using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using TabML.Parser.AST;
using TabML.Parser.Parsing;

namespace TabML.Parser.Validation
{
    class AlternateIntegrityChecker : IntegrityChecker
    {
        public override bool CheckIntegrity(TablatureContext context, ILogger logger)
        {
            var lastIndex = 0;
            var missingIndices = new List<int>();
            foreach (var index in context.DocumentState.DefinedAlternationIndices.OrderBy(i => i))
            {
                if (index != lastIndex + 1)
                {
                    missingIndices.Add(index);
                }

                lastIndex = index;
            }

            if (missingIndices.Count == 0)
                return true;


            var alternationTextType = context.DocumentState.AlternationTextType ?? AlternationTextType.Arabic;
            logger.Report(LogLevel.Error, null, Messages.Error_MissingAlternationTexts,
                            string.Join(", ",
                                        missingIndices.Select(i => AlternationText.GetAlternationText(alternationTextType, i))));
            return false;
        }
    }
}
