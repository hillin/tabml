using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing.Commandlets
{
    class CapoStringsSpecifierParser : ParserBase<CapoStringsSpecifierNode>
    {
        public override bool TryParse(Scanner scanner, out CapoStringsSpecifierNode result)
        {
            var anchor = scanner.MakeAnchor();
            scanner.Expect('(');
            scanner.SkipWhitespaces();

            var match = scanner.Match(@"(\d)\s*-\s*(\d)");

            if (match.Success)
            {
                var from = int.Parse(match.Groups[1].Value);

                if (from == 0 || from > Constants.MaxStringCount)
                {
                    // report a warning here, because we can still go on parsing by ignoring the strings specifier
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Warning_CapoStringsSpecifierInvalidStringNumber);
                    result = null;
                    return false;
                }

                var to = int.Parse(match.Groups[2].Value);

                if (to == 0 || to > Constants.MaxStringCount)
                {
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Warning_CapoStringsSpecifierInvalidStringNumber);
                    result = null;
                    return false;
                }

                result = new CapoRangeStringsSpecifierNode
                {
                    From = new IntegerNode(from, new TextRange(scanner.LastReadRange, match.Groups[1])),
                    To = new IntegerNode(to, new TextRange(scanner.LastReadRange, match.Groups[2]))
                };
            }
            else
            {
                var discreteResult = new CapoDiscreteStringsSpecifierNode();
                result = discreteResult;
                while (!scanner.EndOfInput && scanner.Peek() != ')')
                {
                    var str = scanner.Read(@"\d");
                    int stringNumber;
                    if (string.IsNullOrEmpty(str)
                        || !int.TryParse(str, out stringNumber)
                        || stringNumber == 0
                        || stringNumber > Constants.MaxStringCount)
                    {
                        this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                    ParseMessages.Warning_CapoStringsSpecifierInvalidStringNumber);
                        result = null;
                        return false;
                    }

                    if (discreteResult.Strings.Any(s => s.Value == stringNumber))
                    {
                        this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                    ParseMessages.Warning_RedundantCapoStringSpecifier, stringNumber);
                    }
                    else
                        discreteResult.Strings.Add(new IntegerNode(stringNumber, scanner.LastReadRange));

                    scanner.SkipWhitespaces();
                }
            }

            if (!scanner.Expect(')'))
            {
                this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                            ParseMessages.Warning_CapoStringsSpecifierNotEnclosed);
            }

            result.Range = anchor.Range;
            return true;
        }
    }
}
