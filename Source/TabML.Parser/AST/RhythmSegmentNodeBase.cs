using System.Collections.Generic;

namespace TabML.Parser.AST
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