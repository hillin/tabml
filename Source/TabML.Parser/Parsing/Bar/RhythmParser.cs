using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Bar
{
    class RhythmParser : ParserBase<RhythmNode>
    {
        private readonly BarParser _ownerBarParser;


        public RhythmParser(BarParser ownerBarParser)
        {
            _ownerBarParser = ownerBarParser;
        }

        public bool IsEndOfRhythm(Scanner scanner)
        {
            return _ownerBarParser.IsEndOfBar(scanner) || scanner.Peek() == '@';
        }

        public override bool TryParse(Scanner scanner, out RhythmNode result)
        {
            result = new RhythmNode();

            scanner.SkipWhitespaces(false);

            var anchor = scanner.MakeAnchor();
            while (!this.IsEndOfRhythm(scanner))
            {
                RhythmSegmentNode rhythmSegment;
                if (!new RhythmSegmentParser().TryParse(scanner, out rhythmSegment))
                {
                    result = null;
                    return false;
                }

                result.Segments.Add(rhythmSegment);

                scanner.SkipWhitespaces(false);
            }

            result.Range = anchor.Range;

            return true;

        }
    }
}
