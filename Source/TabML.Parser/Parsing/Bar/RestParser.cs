using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Bar
{
    class RestParser : ParserBase<RestNode>
    {
        public override bool TryParse(Scanner scanner, out RestNode result)
        {
            if (scanner.Expect('r'))
            {
                result = new RestNode
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
