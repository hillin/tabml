namespace TabML.Parser.AST
{
    class ChordCommandletNode : CommandletNode, IRequireStringValidation
    {
        public LiteralNode<string> Name { get; set; }
        public LiteralNode<string> DisplayName { get; set; }
        public ChordFingeringNode Fingering { get; set; }
        
    }
}
