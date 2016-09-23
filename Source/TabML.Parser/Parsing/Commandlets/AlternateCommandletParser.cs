using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Commandlets
{
    [CommandletParser("alternate")]
    class AlternateCommandletParser : CommandletParserBase<AlternateCommandletNode>
    {
        private static readonly HashSet<string> ValidAlternationTexts =
            new HashSet<string>
            {
                "1", "2", "3", "4", "5", "6", "7", "8", "9",
                "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX",
                "i","ii","iii","iv","v","vi","vii","viii","ix",
                "Ⅰ","Ⅱ","Ⅲ","Ⅳ","Ⅴ","Ⅵ","Ⅶ","Ⅷ","Ⅸ",
                "ⅰ","ⅱ","ⅲ","ⅳ","ⅴ","ⅵ","ⅶ","ⅷ","ⅸ"
            };

        public override bool TryParse(Scanner scanner, out AlternateCommandletNode commandlet)
        {
            commandlet = new AlternateCommandletNode();
            scanner.SkipOptional(':', true);

            string alternationText;
            if (scanner.Peek() == '"')
            {
                switch (scanner.TryReadParenthesis(out alternationText, '"', '"', allowNesting: false))
                {
                    case Scanner.ParenthesisReadResult.Success:
                        break;
                    case Scanner.ParenthesisReadResult.MissingClose:
                        this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                    ParseMessages.Warning_AlternationTextMissingCloseQuoteMark);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
                alternationText = scanner.ReadToLineEnd();

            alternationText = alternationText.Trim();
            if (string.IsNullOrEmpty(alternationText))
                return true;

            if (!ValidAlternationTexts.Contains(alternationText))
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_InvalidAlternationText);
                commandlet = null;
                return false;
            }

            commandlet.AlternationText = new LiteralNode<string>(alternationText, scanner.LastReadRange);

            return true;
        }
    }
}
