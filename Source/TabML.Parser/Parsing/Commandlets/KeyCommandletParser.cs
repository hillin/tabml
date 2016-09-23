using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Commandlets
{
    [CommandletParser("key")]
    class KeyCommandletParser : CommandletParserBase<KeyCommandletNode>
    {
        public override bool TryParse(Scanner scanner, out KeyCommandletNode commandlet)
        {
            scanner.SkipOptional(':', true);

            commandlet = new KeyCommandletNode();

            NoteNameNode keyNode;
            if (!new NoteNameParser().TryParse(scanner, out keyNode))
            {
                commandlet = null;
                return false;
            }

            commandlet.Key = keyNode;

            return true;
        }
    }
}
