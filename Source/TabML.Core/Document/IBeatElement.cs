using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public interface IBeatElement
    {
        PreciseDuration GetDuration();
        void ClearRange();
        IBeatElement Clone();
    }
}
