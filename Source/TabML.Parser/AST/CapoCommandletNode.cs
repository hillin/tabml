namespace TabML.Parser.AST
{
    class CapoCommandletNode : CommandletNode, IRequireStringValidation
    {
        public LiteralNode<int> Position { get; set; }
        public CapoStringsSpecifierNode StringsSpecifier { get; set; }
    }
}
