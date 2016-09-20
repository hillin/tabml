using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing.Commandlets
{
    [CommandletParser("staff")]
    class StaffCommandletParser : CommandletParserBase<StaffCommandletNode>
    {
        public override bool TryParse(Scanner scanner, out StaffCommandletNode commandlet)
        {
            scanner.SkipWhitespaces();
            var name = scanner.Read(@"[\w ]*").Trim();

            StaffType? staffType = null;
            if (scanner.Expect(':'))
            {
                StaffType staffTypeValue;
                if (!Parser.TryReadStaffType(scanner, this, out staffTypeValue))
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Warning_StaffCommandletUnknownStaffType);
                else
                    staffType = staffTypeValue;
            }

            commandlet = new StaffCommandletNode(name, staffType);
            return true;
        }
    }
}
