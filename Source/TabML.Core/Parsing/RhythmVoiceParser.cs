using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing
{
    class RhythmVoiceParser : ParserBase<RhythmVoiceNode>
    {
        public static bool IsEndOfVoice(Scanner scanner)
        {
            return scanner.Peek() == ';' || RhythmSegmentParser.IsEndOfSegment(scanner);
        }

        public override bool TryParse(Scanner scanner, out RhythmVoiceNode result)
        {
            scanner.SkipWhitespaces();

            result = new RhythmVoiceNode();
            RhythmUnitNode unit;
            while (new RhythmUnitParser().TryParse(scanner, out unit))
            {
                result.Units.Add(unit);
                scanner.SkipWhitespaces();

                if (RhythmVoiceParser.IsEndOfVoice(scanner))
                    break;
            }

            return true;
        }
    }
}
