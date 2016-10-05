using TabML.Parser.AST;
using TabML.Parser.Parsing.Bar;

namespace TabML.Parser.Parsing
{
    class TablatureParser : ParserBase<TablatureNode>
    {
        public override bool TryParse(Scanner scanner, out TablatureNode result)
        {
            result = new TablatureNode();

            while (!scanner.EndOfInput)
            {
                this.ParseNode(scanner, result);
            }

            result.Range = new TextRange(TextPointer.Zero, scanner.Pointer, scanner);

            return true;
        }

        private void ParseNode(Scanner scanner, TablatureNode tablature)
        {
            scanner.SkipWhitespaces(false);
            if (scanner.Peek() == '+')
            {
                CommandletParserBase commandletParser; ;
                if (!CommandletParser.TryCreate(scanner, this, out commandletParser))
                {
                    return;
                }

                CommandletNode commandlet;
                if (!commandletParser.TryParse(scanner, out commandlet))
                {
                    return;
                }

                tablature.Nodes.Add(commandlet);
                return;
            }

            BarNode bar;
            if (new BarParser(false).TryParse(scanner, out bar))
                tablature.Nodes.Add(bar);
        }
    }
}
