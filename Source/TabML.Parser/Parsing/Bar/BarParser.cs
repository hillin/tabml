﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;
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
            return scanner.EndOfInput || scanner.Peek() == '+' ||  (this.InBraces && scanner.Peek() == '}');
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


            while (!this.IsEndOfBlock(scanner))
            {
                if (scanner.Peek() == '@')
                {
                    if (isLyricsRead)
                    {
                        this.Report(LogLevel.Error, scanner.LastReadRange, Messages.Error_UnexpectedLyrics);
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

                    scanner.SkipWhitespaces(false);
                    continue;
                }

                LiteralNode<CloseBarLine> closeLine;
                if (Parser.TryReadCloseBarLine(scanner, this, out closeLine))
                {
                    result.CloseLine = closeLine;
                    break;
                }

                if (!isLyricsRead && result.Rhythm == null)
                {
                    RhythmNode rhythmNode;
                    if (new RhythmParser(this).TryParse(scanner, out rhythmNode))
                    {
                        result.Rhythm = rhythmNode;
                        continue;
                    }
                }

                break;
            }

            result.Range = anchor.Range;
            return true;
        }

    }
}
