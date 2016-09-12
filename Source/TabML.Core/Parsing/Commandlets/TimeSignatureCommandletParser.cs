using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing.Commandlets
{
    [CommandletParser("time")]
    class TimeSignatureCommandletParser : CommandletParserBase<TimeSignatureCommandletNode>
    {
        public override bool TryParse(Scanner scanner, out TimeSignatureCommandletNode commandlet)
        {
            scanner.SkipOptional(':', true);
            var match = scanner.Match(@"(\d+)\s*\/\s*(\d+)");
            if (!match.Success)
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_InvalidTimeSignature);
                commandlet = null;
                return false;
            }

            var beats = int.Parse(match.Groups[1].Value);
            var noteValueNumber = int.Parse(match.Groups[2].Value);

            if (beats > 32)
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_UnsupportedBeatsInTimeSignature);
                commandlet = null;
                return false;
            }

            if (noteValueNumber > 32)
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_UnsupportedNoteValueInTimeSignature);
                commandlet = null;
                return false;
            }

            BaseNoteValue noteValue;
            if (!BaseNoteValues.TryParse(noteValueNumber, out noteValue))
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_IrrationalNoteValueInTimeSignatureNotSupported);
                commandlet = null;
                return false;
            }

            var timeSignature = new TimeSignature(beats, noteValue);
            commandlet = new TimeSignatureCommandletNode(timeSignature);

            return true;
        }
    }
}
