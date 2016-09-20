using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing.Bar
{
    class LyricsSegmentParser : ParserBase<LyricsSegmentNode>
    {
        private readonly BarParser _ownerBarParser;

        public LyricsSegmentParser(BarParser ownerBarParser)
        {
            _ownerBarParser = ownerBarParser;
        }

        public override bool TryParse(Scanner scanner, out LyricsSegmentNode result)
        {
            var builder = new StringBuilder();

            if (scanner.Peek() == '(')
            {
                string groupedLyrics;
                switch (scanner.TryReadParenthesis(out groupedLyrics))
                {
                    case Scanner.ParenthesisReadResult.Success:
                        result = new LyricsSegmentNode(groupedLyrics);
                        return true;
                    case Scanner.ParenthesisReadResult.MissingClose:
                        this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                    ParseMessages.Warning_TiedLyricsNotEnclosed);
                        result = new LyricsSegmentNode(groupedLyrics);
                        return true;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            while (!_ownerBarParser.IsEndOfBar(scanner))
            {
                var chr = scanner.Read();

                switch (chr)
                {
                    case ' ':
                        result = new LyricsSegmentNode(string.Empty);
                        return true;
                    case '-':
                        if (builder.Length > 0)
                        {
                            if (scanner.Peek() == ' ')
                            {
                                result = new LyricsSegmentNode(builder.ToString());
                                return true;
                            }

                            builder.Append(chr);
                            result = new LyricsSegmentNode(builder.ToString());
                            return true;
                        }

                        result = new LyricsSegmentNode(string.Empty);
                        return true;
                    default:
                        builder.Append(chr);
                        break;
                }
            }

            result = new LyricsSegmentNode(builder.ToString());
            return true;
        }
    }
}
