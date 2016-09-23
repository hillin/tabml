using TabML.Parser.AST;

namespace TabML.Parser.Parsing
{
    class RhythmTemplateSegmentParser : RhythmSegmentParserBase<RhythmTemplateSegmentNode>
    {
        public RhythmTemplateSegmentParser(bool optionalBrackets = false)
            : base(optionalBrackets)
        {

        }

        public override bool TryParse(Scanner scanner, out RhythmTemplateSegmentNode result)
        {

            result = new RhythmTemplateSegmentNode();
            if (!this.TryParseRhythmDefinition(scanner, ref result))
            {
                result = null;
                return false;
            }

            return true;
        }
    }
}
