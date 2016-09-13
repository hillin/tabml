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
            scanner.SkipOptional(':', true);
            int position;
            if (!scanner.TryReadInteger(out position))
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_InvalidCapoPosition);
                commandlet = null;
                return false;
            }

            if (position > 12)
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_CapoTooHigh);
                commandlet = null;
                return false;
            }

            string stringsSpecifierString;
            var readParenthesisResult = scanner.TryReadParenthesis(out stringsSpecifierString, allowNesting: false);

            int[] stringsSpecifier;
            switch (readParenthesisResult)
            {
                case Scanner.ParenthesisReadResult.MissingClose:
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Warning_CapoStringsSpecifierNotEnclosed);
                    stringsSpecifier = CapoInfo.AffectAllStrings;
                    break;
                case Scanner.ParenthesisReadResult.MissingOpen:
                    stringsSpecifier = CapoInfo.AffectAllStrings;
                    break;
                case Scanner.ParenthesisReadResult.Success:
                    if (!this.TryParseStringsSpecifier(scanner, stringsSpecifierString, out stringsSpecifier))
                    {
                        commandlet = null;
                        return false;
                    }
                    break;
                default:
                    stringsSpecifier = CapoInfo.AffectAllStrings;
                    break;
            }

            var capoInfo = new CapoInfo(position, stringsSpecifier);

            commandlet = new CapoCommandletNode(capoInfo);
            return true;

        }

        private bool TryParseStringsSpecifier(Scanner scanner, string stringsSpecifierString, out int[] stringsSpecifier)
        {
            stringsSpecifierString = stringsSpecifierString.Trim();
            var match = Regex.Match(stringsSpecifierString, @"(\d)-(\d)");
            if (match.Success)
            {
                var from = int.Parse(match.Groups[1].Value);

                if (from == 0 || from > Constants.MaxStringCount)
                {
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange, ParseMessages.Warning_CapoStringsSpecifierInvalidStringNumber);
                    stringsSpecifier = null;
                    return false;
                }

                var to = int.Parse(match.Groups[2].Value);

                if (to == 0 || to > Constants.MaxStringCount)
                {
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange, ParseMessages.Warning_CapoStringsSpecifierInvalidStringNumber);
                    stringsSpecifier = null;
                    return false;
                }

                stringsSpecifier = Enumerable.Range(Math.Min(from, to), Math.Abs(to - from)).ToArray();
                return true;
            }

            var containsComma = stringsSpecifierString.Contains(',');
            var containsWhitespace = stringsSpecifierString.Any(char.IsWhiteSpace);
            IEnumerable<string> stringTokens;

            if (containsComma)
            {
                if (containsWhitespace)
                {
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Warning_BothCapoStringsSpecifierDelimiterUsed);
                    stringsSpecifierString = Regex.Replace(stringsSpecifierString, @"\s+", ",");
                }
                stringTokens = stringsSpecifierString.Split(',');
            }
            else if (containsWhitespace)
                stringTokens = Regex.Split(stringsSpecifierString, @"\s+");
            else
                stringTokens = stringsSpecifierString.Select(char.ToString);

            var stringsSpecifierSet = new HashSet<int>();
            foreach (var token in stringTokens)
            {
                int stringNumber;
                if (!int.TryParse(token, out stringNumber)
                    || stringNumber == 0
                    || stringNumber > Constants.MaxStringCount)
                {
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange, ParseMessages.Warning_CapoStringsSpecifierInvalidStringNumber);
                    stringsSpecifier = null;
                    return false;
                }

                if (stringsSpecifierSet.Contains(stringNumber))
                {
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Warning_RedundantCapoStringSpecifier, stringNumber);
                }
                else
                    stringsSpecifierSet.Add(stringNumber);
            }

            stringsSpecifier = stringsSpecifierSet.ToArray();
            return true;
        }

        protected override CommandletNode Recover(Scanner scanner)
        {
            return null;
        }
    }
}
