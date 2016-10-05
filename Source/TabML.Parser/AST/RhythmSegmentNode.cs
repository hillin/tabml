using System.Collections.Generic;

namespace TabML.Parser.AST
{
    class RhythmSegmentNode : RhythmSegmentNodeBase
    {
        public LiteralNode<string> ChordName { get; set; }
        public ChordFingeringNode Fingering { get; set; }

        public override IEnumerable<Node> Children
        {
            get
            {
                foreach (var child in base.Children)
                    yield return child;

                if (this.ChordName != null)
                    yield return this.ChordName;

                if (this.Fingering != null)
                    yield return this.Fingering;
            }
        }

    }
}
