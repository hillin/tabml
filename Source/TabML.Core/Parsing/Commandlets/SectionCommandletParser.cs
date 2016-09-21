using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing.Commandlets
{
    [CommandletParser("section")]
    class SectionCommandletParser : CommandletParserBase<SectionCommandletNode>
    {
        public override bool TryParse(Scanner scanner, out SectionCommandletNode commandlet)
        {
            scanner.SkipOptional(':', true);

            string sectionName;
            if (scanner.Peek() == '"')
            {
                switch (scanner.TryReadParenthesis(out sectionName, '"', '"', allowNesting: false))
                {
                    case Scanner.ParenthesisReadResult.Success:
                        break;
                    case Scanner.ParenthesisReadResult.MissingClose:
                        this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                    ParseMessages.Warning_SectionNameMissingCloseQuoteMark);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
                sectionName = scanner.ReadToLineEnd();

            sectionName = sectionName.Trim();
            if (string.IsNullOrEmpty(sectionName))
                this.Report(ParserReportLevel.Warning, scanner.LastReadRange, ParseMessages.Warning_EmptySectionName);

            var sectionNameNode = new LiteralNode<string>(sectionName, scanner.LastReadRange);

            commandlet = new SectionCommandletNode
            {
                SectionName = sectionNameNode
            };

            return true;
        }
    }
}
