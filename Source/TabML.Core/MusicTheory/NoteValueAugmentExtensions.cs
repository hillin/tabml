using System;

namespace TabML.Core.MusicTheory
{
    public static class NoteValueAugmentExtensions
    {
        public static double GetDurationMultiplier(this NoteValueAugment augment)
        {
            switch (augment)
            {
                case NoteValueAugment.None:
                    return 1.0;
                case NoteValueAugment.Dot:
                    return 1.5;
                case NoteValueAugment.TwoDots:
                    return 1.75;
                case NoteValueAugment.ThreeDots:
                    return 1.875;
                default:
                    throw new ArgumentOutOfRangeException(nameof(augment), augment, null);
            }
        }
    }
}
