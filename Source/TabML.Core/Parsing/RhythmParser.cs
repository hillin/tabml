using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Parsing.AST;
using TabML.Core.Parsing.Bar;

namespace TabML.Core.Parsing
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

            scanner.SkipWhitespaces();
            while (!this.IsEndOfRhythm(scanner))
            {
                RhythmSegmentNode rhythmSegment;
                if (!new RhythmSegmentParser().TryParse(scanner, out rhythmSegment))
                {
                    result = null;
                    return false;
                }

                result.Segments.Add(rhythmSegment);
            }

            return true;

        }
    }
}
