using TabML.Core.Logging;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Commandlets
{
    [CommandletParser("chord")]
    class ChordCommandletParser : CommandletParserBase<ChordCommandletNode>
    {
        public override bool TryParse(Scanner scanner, out ChordCommandletNode commandlet)
        {
            commandlet = new ChordCommandletNode();

            scanner.SkipWhitespaces();

            LiteralNode<string> chordName;
            if (!Parser.TryReadChordName(scanner, this, out chordName) || string.IsNullOrEmpty(chordName.Value))
            {
                this.Report(ReportLevel.Error, scanner.LastReadRange, Messages.Error_MissingChordName);
                commandlet = null;
                return false;
            }

            commandlet.Name = chordName;

            scanner.SkipWhitespaces();

            string displayName;
            if (scanner.TryReadParenthesis(out displayName, '<', '>', allowNesting: false) == Scanner.ParenthesisReadResult.MissingClose)
            {
                this.Report(ReportLevel.Error, scanner.LastReadRange,
                            Messages.Error_ChordDisplayNameNotEnclosed);
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
                this.Report(ReportLevel.Error, scanner.LastReadRange,
                            Messages.Error_ChordCommandletMissingFingering);
                commandlet = null;
                return false;
            }

            commandlet.Fingering = fingeringNode;
            return true;
        }
    }
}
