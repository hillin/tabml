using TabML.Core;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using TabML.Core.Parsing;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Commandlets
{
    [CommandletParser("time")]
    class TimeCommandletParser : CommandletParserBase<TimeSignatureCommandletNode>
    {
        public override bool TryParse(Scanner scanner, out TimeSignatureCommandletNode commandlet)
        {
            commandlet = new TimeSignatureCommandletNode();
            scanner.SkipOptional(':', true);
            var match = scanner.Match(@"(\d+)\s*\/\s*(\d+)");
            if (!match.Success)
            {
                this.Report(LogLevel.Error, scanner.LastReadRange, Messages.Error_InvalidTimeSignature);
                commandlet = null;
                return false;
            }

            var beats = int.Parse(match.Groups[1].Value);

            if (beats > 32)
            {
                this.Report(LogLevel.Error, scanner.LastReadRange, Messages.Error_UnsupportedBeatsInTimeSignature);
                commandlet = null;
                return false;
            }

            commandlet.Beats = new LiteralNode<int>(beats, new TextRange(scanner.LastReadRange, match.Groups[1], scanner.Source));

            var noteValueNumber = int.Parse(match.Groups[2].Value);
            if (noteValueNumber > 32)
            {
                this.Report(LogLevel.Error, scanner.LastReadRange, Messages.Error_UnsupportedNoteValueInTimeSignature);
                commandlet = null;
                return false;
            }

            BaseNoteValue noteValue;
            if (!BaseNoteValues.TryParse(noteValueNumber, out noteValue))
            {
                this.Report(LogLevel.Error, scanner.LastReadRange, Messages.Error_IrrationalNoteValueInTimeSignatureNotSupported);
                commandlet = null;
                return false;
            }

            commandlet.NoteValue = new LiteralNode<BaseNoteValue>(noteValue,
                                                                  new TextRange(scanner.LastReadRange, match.Groups[2], scanner.Source));

            return true;
        }
    }
}
