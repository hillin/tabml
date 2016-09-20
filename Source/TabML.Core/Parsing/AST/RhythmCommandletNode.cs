using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing.AST
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
