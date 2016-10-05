using System;
using System.Collections.Generic;
using System.Linq;

namespace TabML.Parser.AST
{
    abstract class RhythmSegmentNodeBase : Node
    {
        public List<VoiceNode> Voices { get; }

        public override IEnumerable<Node> Children => this.Voices.Cast<Node>();


        protected RhythmSegmentNodeBase()
        {
            this.Voices = new List<VoiceNode>();
        }

    }
}