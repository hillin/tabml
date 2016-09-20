using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TabML.Core.Document;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing.Bar
{
    class BarParser : ParserBase<BarNode>
    {
        public bool InBraces { get; }

        public BarParser(bool inBraces)
        {
            this.InBraces = inBraces;
        }

        private bool IsEndOfBlock(Scanner scanner)
        {
            return scanner.EndOfInput || (this.InBraces && scanner.Peek() == '}');
        }

        public bool IsEndOfBar(Scanner scanner)
        {
            if (this.IsEndOfBlock(scanner))
                return true;

            return scanner.Peek() == '|' || scanner.Peek(3) == ":||";
        }

        public override bool TryParse(Scanner scanner, out BarNode result)
        {
            OpenBarLine openLine;
            Parser.TryReadOpenBarLine(scanner, this, out openLine);

            scanner.SkipWhitespaces();

            result = new BarNode
            {
                OpenLine = openLine
            };

            var isLyricsRead = false;

            CloseBarLine? closeLine = null;

            while (!this.IsEndOfBlock(scanner))
            {
                if (scanner.Peek() == '@')
                {
                    if (isLyricsRead)
                    {
                        this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_UnexpectedLyrics);
                        result = null;
                        return false;
                    }

                    LyricsNode lyricsNode;
                    if (new LyricsParser(this).TryParse(scanner, out lyricsNode))
                    {
                        isLyricsRead = true;
                        result.Lyrics = lyricsNode;
                    }
                    else
                        Debug.Assert(false, "LyricsParser.TryParse() should not return false");

                    scanner.SkipWhitespaces();
                    continue;
                }

                CloseBarLine closeLineValue;
                if (Parser.TryReadCloseBarLine(scanner, this, out closeLineValue))
                {
                    closeLine = result.CloseLine = closeLineValue;
                    break;
                }

                RhythmNode rhythmNode;
                if (new RhythmParser(this).TryParse(scanner, out rhythmNode))
                {
                    result.Rhythm = rhythmNode;
                }

                this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                            isLyricsRead
                                ? ParseMessages.Error_InvalidBarContent_EndBarLineExpected
                                : ParseMessages.Error_InvalidBarContent);
                result = null;
                return false;
            }

            if (closeLine == null)
            {
                this.Report(ParserReportLevel.Warning, scanner.LastReadRange, ParseMessages.Warning_MissingEndBarLine);
                result.CloseLine = CloseBarLine.End;
            }

            return true;
        }

        private bool TryReadLyrics(Scanner scanner, out string[] lyrics)
        {
            scanner.Expect('@');
            scanner.SkipWhitespaces();

            var lyricsList = new List<string>();
            var builder = new StringBuilder();

            while (!this.IsEndOfBar(scanner))
            {
                if (scanner.Peek() == '(')
                {
                    if (builder.Length > 0)
                    {
                        lyricsList.Add(builder.ToString());
                        builder.Clear();
                    }

                    string groupedLyrics;
                    switch (scanner.TryReadParenthesis(out groupedLyrics))
                    {
                        case Scanner.ParenthesisReadResult.Success:
                            lyricsList.Add(groupedLyrics);
                            break;
                        case Scanner.ParenthesisReadResult.MissingClose:
                            this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                        ParseMessages.Warning_TiedLyricsNotEnclosed);
                            lyricsList.Add(groupedLyrics);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                }

                var chr = scanner.Read();

                switch (chr)
                {
                    case ' ':
                        lyricsList.Add(builder.ToString());
                        builder.Clear();
                        break;
                    case '-':
                        if (builder.Length > 0)
                        {
                            if (scanner.Expect(' '))
                            {
                                lyricsList.Add(builder.ToString());
                                builder.Clear();
                                lyricsList.Add(string.Empty);
                            }
                            else
                            {
                                builder.Append(chr);
                                lyricsList.Add(builder.ToString());
                                builder.Clear();
                            }
                        }
                        else
                        {
                            lyricsList.Add(string.Empty);
                        }
                        break;
                    default:
                        builder.Append(chr);
                        break;
                }
            }

            lyrics = lyricsList.ToArray();
            return true;
        }
    }
}
