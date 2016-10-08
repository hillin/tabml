using TabML.Parser.AST;
using TabML.Parser.Parsing.Bar;

namespace TabML.Parser.Parsing
{
    class VoiceParser : ParserBase<VoiceNode>
    {
        public static bool IsEndOfVoice(Scanner scanner)
        {
            return scanner.Peek() == ';' || RhythmSegmentParser.IsEndOfSegment(scanner);
        }

        public override bool TryParse(Scanner scanner, out VoiceNode result)
        {
            var anchor = scanner.MakeAnchor();
            scanner.SkipWhitespaces();

            result = new VoiceNode();
            BeatNode unit;
            while (new BeatParser().TryParse(scanner, out unit))
            {
                result.Beats.Add(unit);
                scanner.SkipWhitespaces();

                if (VoiceParser.IsEndOfVoice(scanner))
                    break;
            }

            result.Range = anchor.Range;
            return true;
        }
    }
}
