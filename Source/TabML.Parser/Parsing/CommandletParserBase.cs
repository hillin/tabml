using TabML.Parser.AST;

namespace TabML.Parser.Parsing
{
    abstract class CommandletParserBase : ParserBase<CommandletNode>
    {
        public LiteralNode<string> CommandletNameNode { get; set; }
    }
}
