namespace TabML.Parser.AST
{
    abstract class CommandletNode : TopLevelNode
    {
        public LiteralNode<string> CommandletNameNode { get; set; }
    }
}
