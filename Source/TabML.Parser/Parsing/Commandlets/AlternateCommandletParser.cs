using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Commandlets
{
    [CommandletParser("alternate")]
    class AlternateCommandletParser : CommandletParserBase<AlternateCommandletNode>
    {

        public override bool TryParse(Scanner scanner, out AlternateCommandletNode commandlet)
        {
            commandlet = new AlternateCommandletNode();
            var hasColon = scanner.SkipOptional(':', true);

            while (!scanner.EndOfLine)
            {
                var text = scanner.Read(@"\w+");

                if (!AlternationText.IsValid(text))
                {
                    this.Report(ReportLevel.Error, scanner.LastReadRange, Messages.Error_InvalidAlternationText);
                    commandlet = null;
                    return false;
                }

                commandlet.AlternationTexts.Add(new LiteralNode<string>(text, scanner.LastReadRange));

                scanner.SkipOptional(',', true);
            }

            if (hasColon && commandlet.AlternationTexts.Count == 0)
            {
                this.Report(ReportLevel.Warning, scanner.LastReadRange,
                            Messages.Warning_AlternationTextExpectedAfterColon);
            }
            
            return true;
        }
    }
}
