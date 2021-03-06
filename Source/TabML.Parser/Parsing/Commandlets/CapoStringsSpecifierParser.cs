﻿using System.Linq;
using TabML.Core;
using TabML.Core.Logging;
using TabML.Core.Parsing;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Commandlets
{
    class CapoStringsSpecifierParser : ParserBase<CapoStringsSpecifierNode>
    {
        public override bool TryParse(Scanner scanner, out CapoStringsSpecifierNode result)
        {
            scanner.Expect('(');
            scanner.SkipWhitespaces();

            var match = scanner.Match(@"(\d)\s*-\s*(\d)");

            if (match.Success)
            {
                var from = int.Parse(match.Groups[1].Value);

                if (from == 0 || from > Defaults.Strings)
                {
                    this.Report(LogLevel.Error, scanner.LastReadRange,
                                Messages.Error_CapoStringsSpecifierInvalidStringNumber);
                    result = null;
                    return false;
                }

                var to = int.Parse(match.Groups[2].Value);

                if (to == 0 || to > Defaults.Strings)
                {
                    this.Report(LogLevel.Error, scanner.LastReadRange,
                                Messages.Error_CapoStringsSpecifierInvalidStringNumber);
                    result = null;
                    return false;
                }

                result = new CapoRangeStringsSpecifierNode
                {
                    From = new LiteralNode<int>(from, new TextRange(scanner.LastReadRange, match.Groups[1], scanner.Source)),
                    To = new LiteralNode<int>(to, new TextRange(scanner.LastReadRange, match.Groups[2], scanner.Source))
                };
            }
            else
            {
                var discreteResult = new CapoDiscreteStringsSpecifierNode();
                result = discreteResult;
                while (!scanner.EndOfInput && scanner.Peek() != ')')
                {
                    var str = scanner.ReadPattern(@"\d");
                    int stringNumber;
                    if (string.IsNullOrEmpty(str)
                        || !int.TryParse(str, out stringNumber)
                        || stringNumber == 0
                        || stringNumber > Defaults.Strings)
                    {
                        this.Report(LogLevel.Error, scanner.LastReadRange,
                                    Messages.Error_CapoStringsSpecifierInvalidStringNumber);
                        result = null;
                        return false;
                    }

                    if (discreteResult.Strings.Any(s => s.Value == stringNumber))
                    {
                        this.Report(LogLevel.Warning, scanner.LastReadRange,
                                    Messages.Warning_RedundantCapoStringSpecifier, stringNumber);
                    }
                    else
                        discreteResult.Strings.Add(new LiteralNode<int>(stringNumber, scanner.LastReadRange));

                    scanner.SkipWhitespaces();
                }
            }

            if (!scanner.Expect(')'))
            {
                this.Report(LogLevel.Warning, scanner.LastReadRange,
                            Messages.Warning_CapoStringsSpecifierNotEnclosed);
            }
            
            return true;
        }
    }
}
