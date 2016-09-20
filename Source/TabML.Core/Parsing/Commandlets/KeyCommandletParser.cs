using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing.Commandlets
{
    [CommandletParser("key")]
    class KeyCommandletParser : CommandletParserBase<KeyCommandletNode>
    {
        public override bool TryParse(Scanner scanner, out KeyCommandletNode commandlet)
        {
            scanner.SkipOptional(':', true);
            NoteName noteName;
            if (!Parser.TryParseNoteName(scanner, this, out noteName))
            {
                this.Report(ParserReportLevel.Warning, scanner.LastReadRange, ParseMessages.Error_InvalidKeySignature);
                commandlet = null;
                return false;
            }

            commandlet = new KeyCommandletNode(noteName);
            return true;
        }
    }
}
