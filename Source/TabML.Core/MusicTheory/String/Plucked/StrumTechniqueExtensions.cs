using System;

namespace TabML.Core.MusicTheory.String.Plucked
{
    public static class StrumTechniqueExtensions
    {
        public static BeatModifier ToBeatModifier(this StrumTechnique technique)
        {
            switch (technique)
            {
                case StrumTechnique.None:
                    return 0;
                case StrumTechnique.PickstrokeDown:
                    return BeatModifier.PickstrokeDown;
                case StrumTechnique.PickstrokeUp:
                    return BeatModifier.PickstrokeUp;
                default:
                    throw new ArgumentOutOfRangeException(nameof(technique), technique, null);
            }
        }
    }
}
