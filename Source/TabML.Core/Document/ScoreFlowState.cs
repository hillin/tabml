using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public class ScoreFlowState
    {
        public BaseNoteName Key { get; set; }
        public TimeSignature TimeSignature { get; set; }
        public int Tempo { get; set; }
        public int Capo { get; set; }

    }
}
