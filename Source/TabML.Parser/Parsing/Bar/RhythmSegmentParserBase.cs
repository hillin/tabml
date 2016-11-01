using TabML.Core.Logging;
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
                this.Report(LogLevel.Error, scanner.LastReadRange,
                            Messages.Error_RhythmSegmentExpectOpeningBracket);
                return false;
            }

            scanner.SkipWhitespaces();

            if (!RhythmSegmentParser.IsEndOfSegment(scanner))
            {
                VoiceNode voice;

                if (scanner.Expect(';'))
                {
                    if (!this.ReadBassVoice(scanner, node))
                        return false;
                }
                else if (new VoiceParser().TryParse(scanner, out voice))
                {
                    node.TrebleVoice = voice;
                    scanner.SkipWhitespaces();

                    if (scanner.Expect(';'))
                        if (!this.ReadBassVoice(scanner, node))
                            return false;
                }
                else
                {
                    this.Report(LogLevel.Error, scanner.Pointer.AsRange(scanner.Source),
                                Messages.Error_UnrecognizableRhythmSegmentElement);
                }
            }

            if (hasBrackets)
            {
                if (scanner.Expect(']'))
                    return true;

                if (this.OptionalBrackets)
                    this.Report(LogLevel.Warning, scanner.LastReadRange,
                                Messages.Warning_RhythmSegmentMissingCloseBracket);
                else
                {
                    this.Report(LogLevel.Error, scanner.LastReadRange,
                                Messages.Error_RhythmSegmentMissingCloseBracket);
                    node = null;
                    return false;
                }
            }

            if (node.BassVoice == null && node.TrebleVoice == null)
            {
                this.Report(LogLevel.Warning, scanner.LastReadRange,
                            Messages.Warning_EmptyRhythmSegment);
            }

            node.Range = anchor.Range;
            return true;
        }

        private bool ReadBassVoice(Scanner scanner, TNode node)
        {
            VoiceNode voice;
            if (new VoiceParser().TryParse(scanner, out voice))
            {
                node.BassVoice = voice;
                scanner.SkipWhitespaces();

                if (scanner.Peek() == ';')
                {
                    this.Report(LogLevel.Error, scanner.Pointer.AsRange(scanner.Source),
                                Messages.Error_UnrecognizableRhythmSegmentElement);
                    return false;
                }
            }
            else
            {
                this.Report(LogLevel.Error, scanner.Pointer.AsRange(scanner.Source),
                            Messages.Error_UnrecognizableRhythmSegmentElement);
                return false;
            }

            return true;
        }
    }
}
