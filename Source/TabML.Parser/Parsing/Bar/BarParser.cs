using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TabML.Core.Document;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Bar
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
            var anchor = scanner.MakeAnchor();
            LiteralNode<OpenBarLine> openLine;
            Parser.TryReadOpenBarLine(scanner, this, out openLine);

            scanner.SkipWhitespaces();

            result = new BarNode
            {
                OpenLine = openLine
            };

            var isLyricsRead = false;

            LiteralNode<CloseBarLine> closeLine = null;

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

                if (Parser.TryReadCloseBarLine(scanner, this, out closeLine))
                {
                    result.CloseLine = closeLine;
                    break;
                }

                RhythmNode rhythmNode;
                if (new RhythmParser(this).TryParse(scanner, out rhythmNode))
                {
                    result.Rhythm = rhythmNode;
                    continue;
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
            }

            result.Range = anchor.Range;
            return true;
        }
        
    }
}
