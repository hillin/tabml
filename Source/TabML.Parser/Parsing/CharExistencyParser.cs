using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Bar
{
    class CharExistencyParser : ParserBase<ExistencyNode>
    {
        private readonly char _char;

        public CharExistencyParser(char @char)
        {
            _char = @char;
        }

        public override bool TryParse(Scanner scanner, out ExistencyNode result)
        {
            if (scanner.Expect(_char))
            {
                result = new ExistencyNode
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
