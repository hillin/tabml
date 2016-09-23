using TabML.Parser.AST;

namespace TabML.Parser.Parsing
{
    class RhythmSegmentParser : RhythmSegmentParserBase<RhythmSegmentNode>
    {
        public static bool IsEndOfSegment(Scanner scanner)
        {
            return scanner.Peek() == ']' || scanner.EndOfLine;
        }

        public RhythmSegmentParser() : base(false)
        {

        }

        public override bool TryParse(Scanner scanner, out RhythmSegmentNode result)
        {
            var anchor = scanner.MakeAnchor();
            result = new RhythmSegmentNode();

            scanner.SkipWhitespaces();

            var rhythmDefined = false;

            if (scanner.Peek() == '[')
            {
                rhythmDefined = true;
                if (!this.TryParseRhythmDefinition(scanner, ref result))
                {
                    result = null;
                    return false;
                }
            }

            scanner.SkipWhitespaces();
            LiteralNode<string> chordNameNode;
            if (Parser.TryReadChordName(scanner, this, out chordNameNode))
            {
                result.ChordName = chordNameNode;

                scanner.SkipWhitespaces();
                if (scanner.Expect('('))
                {
                    ChordFingeringNode fingeringNode;
                    if (!new ChordFingeringParser(s => s.EndOfLine || s.Peek() == ')').TryParse(scanner, out fingeringNode))
                    {
                        result = null;
                        return false;
                    }


                    if (fingeringNode.Fingerings.Count == 0)
                    {
                        this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                                    ParseMessages.Error_RhythmSegmentMissingFingering);
                        result = null;
                        return false;
                    }

                    result.Fingering = fingeringNode;

                    if (!scanner.Expect(')'))
                    {
                        this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                                    ParseMessages.Error_RhythmSegmentChordFingeringNotEnclosed);
                        result = null;
                        return false;
                    }
                }
            }

            if (!rhythmDefined && string.IsNullOrEmpty(chordNameNode.Value) && result.Fingering == null)
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                            ParseMessages.Error_RhythmDefinitionExpected);
                result = null;
                return false;
            }

            result.Range = anchor.Range;
            return true;
        }
    }
}
