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
            commandlet = new ChordCommandletNode();

            LiteralNode<string> chordName;
            if (!Parser.TryReadChordName(scanner, this, out chordName) || string.IsNullOrEmpty(chordName.Value))
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_MissingChordName);
                commandlet = null;
                return false;
            }

            commandlet.Name = chordName;

            scanner.SkipWhitespaces();

            string displayName;
            if (scanner.TryReadParenthesis(out displayName, '<', '>', allowNesting: false) == Scanner.ParenthesisReadResult.MissingClose)
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                            ParseMessages.Error_MissingChordDisplayNameNotEnclosed);
                commandlet = null;
                return false;
            }

            commandlet.DisplayName = new LiteralNode<string>(displayName, scanner.LastReadRange);

            scanner.SkipOptional(':', true);

            ChordFingeringNode fingeringNode;
            if (!new ChordFingeringParser(s => s.EndOfLine).TryParse(scanner, out fingeringNode))
            {
                commandlet = null;
                return false;
            }

            if (fingeringNode.Fingerings.Count == 0)
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                            ParseMessages.Error_ChordCommandletMissingFingering);
                commandlet = null;
                return false;
            }

            commandlet.Fingering = fingeringNode;
            return true;
        }
    }
}
