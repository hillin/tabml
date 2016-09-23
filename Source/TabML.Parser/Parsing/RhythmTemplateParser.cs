using TabML.Parser.AST;

namespace TabML.Parser.Parsing
{
    class RhythmTemplateParser : ParserBase<RhythmTemplateNode>
    {
        public override bool TryParse(Scanner scanner, out RhythmTemplateNode result)
        {
            result = new RhythmTemplateNode();

            scanner.SkipWhitespaces();

            if (scanner.Peek() != '[')  // handle optional brackets
            {
                RhythmTemplateSegmentNode segment;
                if (new RhythmTemplateSegmentParser(true).TryParse(scanner, out segment))
                {
                    result.Segments.Add(segment);
                    return true;
                }

                result = null;
                return false;
            }

            while (!scanner.EndOfLine)
            {
                RhythmTemplateSegmentNode segment;
                if (!new RhythmTemplateSegmentParser().TryParse(scanner, out segment))
                {
                    result = null;
                    return false;
                }

                result.Segments.Add(segment);
                scanner.SkipWhitespaces();
            }

            return true;
        }
    }
}
