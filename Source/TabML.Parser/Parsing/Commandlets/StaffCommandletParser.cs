using TabML.Core.Document;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Commandlets
{
    [CommandletParser("staff")]
    class StaffCommandletParser : CommandletParserBase<StaffCommandletNode>
    {
        public override bool TryParse(Scanner scanner, out StaffCommandletNode commandlet)
        {
            commandlet = new StaffCommandletNode();

            scanner.SkipWhitespaces();
            var name = scanner.Read(@"[\w ]*").Trim();
            commandlet.StaffName = new LiteralNode<string>(name, scanner.LastReadRange);

            if (scanner.Expect(':'))
            {
                LiteralNode<StaffType> staffTypeNode;
                if (!Parser.TryReadStaffType(scanner, this, out staffTypeNode))
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Warning_StaffCommandletUnknownStaffType);
                else
                    commandlet.StaffType = staffTypeNode;
            }

            return true;
        }
    }
}
