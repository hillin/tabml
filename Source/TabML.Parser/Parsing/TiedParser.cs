using TabML.Parser.AST;

namespace TabML.Parser.Parsing
{
    class TiedParser : ParserBase<TiedNode>
    {
        public override bool TryParse(Scanner scanner, out TiedNode result)
        {
            if (scanner.Expect('~'))
            {
                result = new TiedNode
                {
                    Range = scanner.LastReadRange
                };
                return true;
            }

            result = null;
            return true;
        }
    }
}
