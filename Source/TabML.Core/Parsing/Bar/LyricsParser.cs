using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing.Bar
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

            return true;
        }
    }
}
