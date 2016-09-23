namespace TabML.Parser.AST
{
    class RhythmSegmentNode : RhythmSegmentNodeBase
    {
        public LiteralNode<string> ChordName { get; set; }
        public ChordFingeringNode Fingering { get; set; }
    }
}
