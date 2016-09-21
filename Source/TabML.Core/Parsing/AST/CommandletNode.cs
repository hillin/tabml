namespace TabML.Core.Parsing.AST
{
    abstract class CommandletNode : TopLevelNode
    {
        public LiteralNode<string> CommandletNameNode { get; set; }
    }
}
