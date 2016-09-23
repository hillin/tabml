using System.Collections.Generic;

namespace TabML.Parser.AST
{
    class RhythmVoiceNode : Node
    {
        public List<RhythmUnitNode> Units { get; }

        public RhythmVoiceNode()
        {
            this.Units = new List<RhythmUnitNode>();
        }
    }
}
