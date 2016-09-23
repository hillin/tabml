using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Commandlets
{
    [CommandletParser("rhythm")]
    class RhythmCommandletParser : CommandletParserBase<RhythmCommandletNode>
    {
        public override bool TryParse(Scanner scanner, out RhythmCommandletNode commandlet)
        {
            scanner.SkipOptional(':', true);

            RhythmTemplateNode templateNode;
            if (!new RhythmTemplateParser().TryParse(scanner, out templateNode))
            {
                commandlet = null;
                return false;
            }

            commandlet = new RhythmCommandletNode(templateNode);
            return true;
        }

    }
}
