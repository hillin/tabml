using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Core.Parsing.AST
{
    class RhythmTemplateNode : Node
    {
        public List<RhythmTemplateSegmentNode> Segments { get; }

        public RhythmTemplateNode()
        {
            this.Segments = new List<RhythmTemplateSegmentNode>();
        }
    }
}
