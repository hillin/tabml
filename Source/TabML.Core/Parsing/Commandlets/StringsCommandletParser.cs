using System;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing.Commandlets
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

            commandlet = new StringsCommandletNode(stringCount);
            return true;
        }

        protected override CommandletNode Recover(Scanner scanner)
        {
            scanner.SkipLine();
            return new StringsCommandletNode(6);
        }
    }
}
