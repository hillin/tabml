using TabML.Core;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Commandlets
{
    [CommandletParser("strings")]
    class StringsCommandletParser : CommandletParserBase<StringsCommandletNode>
    {

        public override bool TryParse(Scanner scanner, out StringsCommandletNode commandlet)
        {
            scanner.SkipOptional(':', true);
            int stringCount;

            if (!scanner.TryReadInteger(out stringCount))
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_InvalidStringCount);
                commandlet = null;
                return false;
            }

            if (stringCount < Constants.MinStringCount || stringCount > Constants.MaxStringCount)
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_StringCountOutOfRange, Constants.MinStringCount, Constants.MaxStringCount);
                commandlet = null;
                return false;
            }

            commandlet = new StringsCommandletNode
            {
                StringCount = new LiteralNode<int>(stringCount, scanner.LastReadRange)
            };

            return true;
        }
    }
}
