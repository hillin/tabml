using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing.Commandlets
{
    [CommandletParser("chord")]
    class ChordCommandletParser : CommandletParserBase<ChordCommandletNode>
    {
        public override bool TryParse(Scanner scanner, out ChordCommandletNode commandlet)
        {
            var chordName = scanner.Read(c => c != '<' && c != ':' && !char.IsWhiteSpace(c));
            if (string.IsNullOrEmpty(chordName))
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_MissingChordName);
                commandlet = null;
                return false;
            }

            scanner.SkipWhitespaces();

            string displayName;
            var readDisplayNameResult = scanner.TryReadParenthesis(out displayName, '<', '>', allowNesting: false);
            if (readDisplayNameResult == Scanner.ParenthesisReadResult.MissingClose)
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                            ParseMessages.Error_MissingChordDisplayNameNotEnclosed);
                commandlet = null;
                return false;
            }

            scanner.SkipOptional(':', true);

            var definitionString = scanner.Read(@"[\dxX ,]+").Trim();
            if (string.IsNullOrEmpty(definitionString))
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_MissingChordFingering);
                commandlet = null;
                return false;
            }

            var containsComma = definitionString.Contains(',');
            var containsWhitespace = definitionString.Any(char.IsWhiteSpace);
            IEnumerable<string> fingeringTokens;

            if (containsComma)
            {
                if (containsWhitespace)
                {
                    this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                                ParseMessages.Warning_BothChordFingeringDelimiterUsed);
                    definitionString = Regex.Replace(definitionString, @"\s+", ",");
                }
                fingeringTokens = definitionString.Split(',');
            }
            else if (containsWhitespace)
                fingeringTokens = Regex.Split(definitionString, @"\s+");
            else
                fingeringTokens = definitionString.Select(char.ToString).ToArray();

            var fingering =
                fingeringTokens.Select(
                    f => f.Equals("x", StringComparison.InvariantCultureIgnoreCase)
                        ? ChordDefinition.FingeringSkipString
                        : int.Parse(f)).ToArray();

            var chordDefinition = new ChordDefinition(chordName, displayName, fingering);
            commandlet = new ChordCommandletNode(chordDefinition);
            return true;
        }
    }
}
