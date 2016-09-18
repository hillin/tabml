using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
{
    [CommandletParser("rhythm")]
    class RhythmCommandletParser : CommandletParserBase<RhythmCommandletNode>
    {
        public override bool TryParse(Scanner scanner, out RhythmCommandletNode commandlet)
        {
            scanner.SkipOptional(':', true);
            
        }
        
    }
}
