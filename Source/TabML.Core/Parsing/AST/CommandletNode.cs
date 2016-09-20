namespace TabML.Core.Parsing.AST
{
    abstract class CommandletNode : TopLevelNode
    {
        public StringNode CommandletNameNode { get; set; }
    }
}
