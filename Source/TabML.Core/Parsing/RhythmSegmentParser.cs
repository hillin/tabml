using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing
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
            string chordName;
            if (Parser.TryReadChordName(scanner, this, out chordName))
            {
                result.ChordName = chordName;

                scanner.SkipWhitespaces();
                if (scanner.Expect('('))
                {
                    int[] fingering;
                    if (!Parser.TryReadChordFingering(scanner, this, out fingering))
                    {
                        this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                                    ParseMessages.Error_RhythmSegmentMissingFingering);
                        result = null;
                        return false;
                    }

                    result.Fingering = fingering;

                    if (!scanner.Expect(')'))
                    {
                        this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                                    ParseMessages.Error_RhythmSegmentChordFingeringNotEnclosed);
                        result = null;
                        return false;
                    }
                }
            }

            if (!rhythmDefined && string.IsNullOrEmpty(chordName))
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                            ParseMessages.Error_RhythmDefinitionExpected);
                result = null;
                return false;
            }

            return true;
        }
    }
}
