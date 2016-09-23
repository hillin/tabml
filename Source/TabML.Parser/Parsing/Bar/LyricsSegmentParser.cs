using System;
using System.Text;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Bar
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
            var anchor = scanner.MakeAnchor();

            if (scanner.Peek() == '(')
            {
                string groupedLyrics;
                switch (scanner.TryReadParenthesis(out groupedLyrics))
                {
                    case Scanner.ParenthesisReadResult.Success:
                        result = new LyricsSegmentNode(groupedLyrics, scanner.LastReadRange);
                        return true;
                    case Scanner.ParenthesisReadResult.MissingClose:
                        this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                    ParseMessages.Warning_TiedLyricsNotEnclosed);
                        result = new LyricsSegmentNode(groupedLyrics, scanner.LastReadRange);
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
                        result = new LyricsSegmentNode(string.Empty, scanner.LastReadRange.From.AsRange());
                        return true;
                    case '-':
                        if (builder.Length > 0)
                        {
                            if (scanner.Peek() == ' ')
                            {
                                result = new LyricsSegmentNode(builder.ToString(), anchor.Range);
                                return true;
                            }

                            builder.Append(chr);
                            result = new LyricsSegmentNode(builder.ToString(), anchor.Range);
                            return true;
                        }

                        result = new LyricsSegmentNode(string.Empty, scanner.LastReadRange.From.AsRange());
                        return true;
                    default:
                        builder.Append(chr);
                        break;
                }
            }

            result = new LyricsSegmentNode(builder.ToString(), anchor.Range);
            return true;
        }
    }
}
