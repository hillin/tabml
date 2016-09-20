using System.Collections.Generic;

namespace TabML.Core.Parsing.AST
{
    abstract class RhythmSegmentNodeBase : Node
    {
        public List<RhythmVoiceNode> Voices { get; }

        protected RhythmSegmentNodeBase()
        {
            this.Voices = new List<RhythmVoiceNode>();
        }

    }
}