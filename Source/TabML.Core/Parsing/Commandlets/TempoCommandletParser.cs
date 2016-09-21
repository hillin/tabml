using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing.Commandlets
{
    [CommandletParser("tempo")]
    class TempoCommandletParser : CommandletParserBase<TempoCommandletNode>
    {
        public override bool TryParse(Scanner scanner, out TempoCommandletNode commandlet)
        {
            scanner.SkipOptional(':', true);

            commandlet = new TempoCommandletNode();

            var match = scanner.Match(@"((\d+)\s*=\s*)?(\d+)");

            if (!match.Success)
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                            ParseMessages.Error_InvalidTempoSignature);
                commandlet = null;
                return false;
            }

            var noteValueNumber = int.Parse(match.Groups[2].Value);

            BaseNoteValue noteValue;
            if (!BaseNoteValues.TryParse(noteValueNumber, out noteValue))
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                            ParseMessages.Error_IrrationalNoteValueInTempoSignatureNotSupported);
                commandlet = null;
                return false;
            }

            commandlet.NoteValue = new LiteralNode<BaseNoteValue>(noteValue,
                                                                  new TextRange(scanner.LastReadRange, match.Groups[2]));

            var beats = int.Parse(match.Groups[3].Value);

            if (beats == 0)
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                            ParseMessages.Error_TempoSignatureSpeedTooLow);
                commandlet = null;
                return false;
            }

            if (beats > 10000)
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                            ParseMessages.Error_TempoSignatureSpeedTooFast);
                commandlet = null;
                return false;
            }

            commandlet.Beats = new LiteralNode<int>(beats, new TextRange(scanner.LastReadRange, match.Groups[3]));
            
            return true;
        }
    }
}
