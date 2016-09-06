namespace TabML.Core.Document
{
    public class ScoreFlowState
    {
        public MusicTheory.BaseNoteName Key { get; set; }
        public MusicTheory.TimeSignature TimeSignature { get; set; }
        public int Tempo { get; set; }
        public int Capo { get; set; }

    }
}
