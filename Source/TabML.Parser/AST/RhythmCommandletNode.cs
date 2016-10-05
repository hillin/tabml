using System.Collections.Generic;

namespace TabML.Parser.AST
{
    class RhythmCommandletNode : CommandletNode
    {
        public RhythmTemplateNode TemplateNode { get; }

        public RhythmCommandletNode(RhythmTemplateNode templateNode)
        {
            this.TemplateNode = templateNode;
        }

        protected override IEnumerable<Node> CommandletChildNodes
        {
            get { yield return this.TemplateNode; }
        }
    }
}
