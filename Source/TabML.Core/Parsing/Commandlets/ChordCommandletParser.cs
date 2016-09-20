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
            string chordName;
            if (!Parser.TryReadChordName(scanner, this, out chordName) || string.IsNullOrEmpty(chordName))
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

            int[] fingering;
            if (!Parser.TryReadChordFingering(scanner, this, out fingering))
            {
                commandlet = null;
                return false;
            }

            var chordDefinition = new ChordDefinition(chordName, displayName, fingering);
            commandlet = new ChordCommandletNode(chordDefinition);
            return true;
        }
    }
}
