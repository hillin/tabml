using System.Diagnostics;
using System.Linq;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Bar
{
    class LyricsParser : ParserBase<LyricsNode>
    {
        private readonly BarParser _owner;

        public LyricsParser(BarParser owner)
        {
            _owner = owner;
        }

        public override bool TryParse(Scanner scanner, out LyricsNode result)
        {
            var rangeFrom = scanner.Pointer;
            scanner.Expect('@');
            scanner.SkipWhitespaces();

            result = new LyricsNode();

            while (!_owner.IsEndOfBar(scanner))
            {
                LyricsSegmentNode lyricsSegmentNode;
                if (new LyricsSegmentParser(_owner).TryParse(scanner, out lyricsSegmentNode))
                    result.LyricsSegments.Add(lyricsSegmentNode);
                else
                    Debug.Assert(false, "LyricsSegmentParser.TryParse() should not return false");
            }

            for (var i = result.LyricsSegments.Count - 1; i >= 0; --i)
            {
                if (result.LyricsSegments[i].LyricsSegment.Value.Length == 0)
                    result.LyricsSegments.RemoveAt(i);
                else
                    break;
            }

            result.Range = result.LyricsSegments.Count > 0
                ? new TextRange(rangeFrom, result.LyricsSegments.Last().Range.To)
                : rangeFrom.AsRange();
            return true;
        }
    }
}
