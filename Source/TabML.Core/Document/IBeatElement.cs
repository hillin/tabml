using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
    public interface IBeatElement : IBarVoiceElement
    {
        Beam OwnerBeam { get; }
        PreciseDuration GetDuration();
        void ClearRange();

    }
}
