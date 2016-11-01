using System;
using System.Text;
using TabML.Core.Logging;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Bar
{
    class LyricsSegmentParser : ParserBase<LyricsSegmentNode>
    {
        private readonly LyricsParser _owner;

        public LyricsSegmentParser(LyricsParser owner)
        {
            _owner = owner;
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
                        this.Report(LogLevel.Warning, scanner.LastReadRange,
                                    Messages.Warning_TiedLyricsNotEnclosed);
                        result = new LyricsSegmentNode(groupedLyrics, scanner.LastReadRange);
                        return true;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            while (!_owner.IsEndOfLyrics(scanner))
            {
                var chr = scanner.Read();

                if (char.IsWhiteSpace(chr))
                {
                    result = new LyricsSegmentNode(builder.ToString(), anchor.Range);
                    return true;
                }

                if (chr == '-')
                {
                    if (builder.Length > 0)
                    {
                        if (char.IsWhiteSpace(scanner.Peek()))
                        {
                            result = new LyricsSegmentNode(builder.ToString(), anchor.Range);
                            return true;
                        }

                        builder.Append(chr);
                        result = new LyricsSegmentNode(builder.ToString(), anchor.Range);
                        return true;
                    }

                    result = new LyricsSegmentNode(string.Empty, scanner.LastReadRange);
                    return true;
                }

                builder.Append(chr);
            }

            result = new LyricsSegmentNode(builder.ToString(), anchor.Range);
            return true;
        }
    }
}
