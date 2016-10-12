using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Bar
{
    abstract class RhythmSegmentParserBase<TNode> : ParserBase<TNode>
         where TNode : RhythmSegmentNodeBase, new()
    {

        public bool OptionalBrackets { get; }

        protected RhythmSegmentParserBase(bool optionalBrackets = false)
        {
            this.OptionalBrackets = optionalBrackets;
        }


        protected bool TryParseRhythmDefinition(Scanner scanner, ref TNode node)
        {
            var anchor = scanner.MakeAnchor();
            var hasBrackets = scanner.Expect('[');

            if (!this.OptionalBrackets && !hasBrackets)
            {
                this.Report(ReportLevel.Error, scanner.LastReadRange,
                            Messages.Error_RhythmSegmentExpectOpeningBracket);
                return false;
            }

            scanner.SkipWhitespaces();

            if (!RhythmSegmentParser.IsEndOfSegment(scanner))
            {
                VoiceNode voice;
                while (new VoiceParser().TryParse(scanner, out voice))
                {
                    node.Voices.Add(voice);
                    scanner.SkipWhitespaces();

                    if (RhythmSegmentParser.IsEndOfSegment(scanner))
                        break;

                    if (scanner.Peek() == ';')
                        continue;

                    this.Report(ReportLevel.Error, scanner.Pointer.AsRange(scanner),
                                Messages.Error_UnrecognizableRhythmSegmentElement);
                    node = null;
                    return false;
                }

                if (node.Voices.Count > 2)
                {
                    this.Report(ReportLevel.Error,
                                new TextRange(node.Voices[2].Range.From, node.Voices[node.Voices.Count - 1].Range.To),
                                Messages.Error_TooManyVoices);
                    node = null;
                    return false;
                }
            }

            if (hasBrackets)
            {
                if (scanner.Expect(']'))
                    return true;

                if (this.OptionalBrackets)
                    this.Report(ReportLevel.Warning, scanner.LastReadRange,
                                Messages.Warning_RhythmSegmentMissingCloseBracket);
                else
                {
                    this.Report(ReportLevel.Error, scanner.LastReadRange,
                                Messages.Error_RhythmSegmentMissingCloseBracket);
                    node = null;
                    return false;
                }
            }

            if (node.Voices.Count == 0)
            {
                this.Report(ReportLevel.Warning, scanner.LastReadRange,
                            Messages.Warning_EmptyRhythmSegment);
            }

            node.Range = anchor.Range;
            return true;
        }
    }
}
