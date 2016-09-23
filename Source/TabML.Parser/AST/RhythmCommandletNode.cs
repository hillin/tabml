namespace TabML.Parser.AST
{
    class RhythmCommandletNode : CommandletNode
    {
        public RhythmTemplateNode TemplateNode { get; }

        public RhythmCommandletNode(RhythmTemplateNode templateNode)
        {
            this.TemplateNode = templateNode;
        }
    }
}
