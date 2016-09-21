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

            commandlet = new KeyCommandletNode();

            NoteNameNode keyNode;
            if (!new NoteNameParser().TryParse(scanner, out keyNode))
            {
                commandlet = null;
                return false;
            }

            commandlet.Key = keyNode;

            return true;
        }
    }
}
