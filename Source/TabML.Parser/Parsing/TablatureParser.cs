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

            return true;
        }

        private void ParseNode(Scanner scanner, TablatureNode tablature)
        {
            scanner.SkipWhitespaces();
            switch (scanner.Peek())
            {
                case '+':
                    CommandletNode commandlet;
                    if (CommandletParser.Create(scanner).TryParse(scanner, out commandlet))
                        tablature.Nodes.Add(commandlet);
                    break;
                case '|':
                    BarNode bar;
                    if (new BarParser(false).TryParse(scanner, out bar))
                        tablature.Nodes.Add(bar);
                    break;
            }
        }
    }
}
