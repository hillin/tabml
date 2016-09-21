using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing.Commandlets
{
    [CommandletParser("capo")]
    class CapoCommandletParser : CommandletParserBase<CapoCommandletNode>
    {
        public override bool TryParse(Scanner scanner, out CapoCommandletNode commandlet)
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            commandlet = new CapoCommandletNode();

            scanner.SkipOptional(':', true);

            LiteralNode<int> positionNode;
            if (!Parser.TryParseInteger(scanner, out positionNode))
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_InvalidCapoPosition);
                commandlet = null;
                return false;
            }

            if (positionNode.Value > 12)
            {
                this.Report(ParserReportLevel.Warning, scanner.LastReadRange, ParseMessages.Warning_CapoTooHigh);
            }

            commandlet.Position = positionNode;

            scanner.SkipWhitespaces();

            if (scanner.Peek() == '(')
            {
                CapoStringsSpecifierNode stringsSpecifierNode;
                if (!new CapoStringsSpecifierParser().TryParse(scanner, out stringsSpecifierNode))
                {
                    commandlet = null;
                    return false;
                }

                commandlet.StringsSpecifier = stringsSpecifierNode;
            }
            
            return true;

        }
        
        protected override CommandletNode Recover(Scanner scanner)
        {
            return null;
        }
    }
}
