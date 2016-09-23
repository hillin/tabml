using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Parser.AST;
using TabML.Parser.Parsing.Bar;

namespace TabML.Parser.Parsing.Commandlets
{
    [CommandletParser("pattern")]
    class PatternCommandletParser : CommandletParserBase<PatternCommandletNode>
    {
        public override bool TryParse(Scanner scanner, out PatternCommandletNode commandlet)
        {
            commandlet = new PatternCommandletNode();
            scanner.SkipOptional(':', true);
            scanner.SkipWhitespaces(false); // allow a new line here

            while (!scanner.EndOfInput)
            {
                BarNode bar;
                if (new BarParser(false).TryParse(scanner, out bar))
                    commandlet.PatternBars.Add(bar);
                else
                {
                    commandlet = null;
                    return false;
                }

                scanner.SkipWhitespaces(false);

                if (scanner.Peek() == '{')
                    break;
            }

            foreach (
                var barLine in
                    commandlet.PatternBars.Select(b => b.OpenLine)
                              .Where(l => l != null)
                              .Where(barLine => barLine.Value != OpenBarLine.Standard))
            {
                this.Report(ParserReportLevel.Error, barLine.Range, ParseMessages.Error_InvalidBarLineInPattern);
                commandlet = null;
                return false;
            }

            foreach (
                var barLine in
                    commandlet.PatternBars.Select(b => b.CloseLine)
                              .Where(l => l != null)
                              .Where(barLine => barLine.Value != CloseBarLine.Standard))
            {
                this.Report(ParserReportLevel.Error, barLine.Range, ParseMessages.Error_InvalidBarLineInPattern);
                commandlet = null;
                return false;
            }

            if (!scanner.Expect('{'))
            {
                commandlet = null;
                return false;
            }

            scanner.SkipWhitespaces(false);

            while (!scanner.EndOfInput && scanner.Peek() != '}')
            {
                BarNode bar;
                if (new BarParser(true).TryParse(scanner, out bar))
                    commandlet.InstanceBars.Add(bar);
                else
                {
                    commandlet = null;
                    return false;
                }

                scanner.SkipWhitespaces(false);
            }

            if (!scanner.Expect('}'))
            {
                this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                            ParseMessages.Warning_PatternBodyNotEnclosed);
            }

            return true;
        }
    }
}
