using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;
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

            var anchor = scanner.MakeAnchor();
            commandlet.TemplateBars = new PatternCommandletNode.TemplateBarsNode();

            while (!scanner.EndOfInput)
            {
                BarNode bar;
                if (new BarParser(false).TryParse(scanner, out bar))
                    commandlet.TemplateBars.Bars.Add(bar);
                else
                {
                    commandlet = null;
                    return false;
                }

                scanner.SkipWhitespaces(false);

                if (scanner.Peek() == '{')
                    break;
            }

            commandlet.TemplateBars.Range = anchor.Range;

            foreach (
                var barLine in
                    commandlet.TemplateBars.Bars.Select(b => b.OpenLine)
                              .Where(l => l != null)
                              .Where(barLine => barLine.Value != OpenBarLine.Standard))
            {
                this.Report(ReportLevel.Error, barLine.Range, Messages.Error_InvalidBarLineInPattern);
                commandlet = null;
                return false;
            }

            foreach (
                var barLine in
                    commandlet.TemplateBars.Bars.Select(b => b.CloseLine)
                              .Where(l => l != null)
                              .Where(barLine => barLine.Value != CloseBarLine.Standard))
            {
                this.Report(ReportLevel.Error, barLine.Range, Messages.Error_InvalidBarLineInPattern);
                commandlet = null;
                return false;
            }

            if (!scanner.Expect('{'))
            {
                commandlet = null;
                return false;
            }

            scanner.SkipWhitespaces(false);

            anchor = scanner.MakeAnchor();
            commandlet.InstanceBars = new PatternCommandletNode.InstanceBarsNode();

            while (!scanner.EndOfInput && scanner.Peek() != '}')
            {
                BarNode bar;
                if (new BarParser(true).TryParse(scanner, out bar))
                    commandlet.InstanceBars.Bars.Add(bar);
                else
                {
                    commandlet = null;
                    return false;
                }

                scanner.SkipWhitespaces(false);
            }

            commandlet.InstanceBars.Range = anchor.Range;

            if (!scanner.Expect('}'))
            {
                this.Report(ReportLevel.Warning, scanner.LastReadRange,
                            Messages.Warning_PatternBodyNotEnclosed);
            }

            return true;
        }
    }
}
