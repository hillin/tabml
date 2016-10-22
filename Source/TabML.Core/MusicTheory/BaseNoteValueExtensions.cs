using System;

namespace TabML.Core.MusicTheory
{
    public static class BaseNoteValueExtensions
    {
        public static PreciseDuration GetDuration(this BaseNoteValue value)
        {
            return new PreciseDuration(Math.Pow(2, (int)value));
        }

        public static int GetInvertedDuration(this BaseNoteValue value)
        {
            var intValue = (int)value;
            if (intValue > 0)
                throw new ArgumentOutOfRangeException(nameof(value), "only whole or shorter values are supported");

            return (int)Math.Pow(2, -intValue);
        }

        public static BaseNoteValue Half(this BaseNoteValue value)
        {
            if (value.GetIsShortestSupported())
                throw new ArgumentOutOfRangeException(nameof(value));

            return value - 1;
        }

        public static BaseNoteValue Double(this BaseNoteValue value)
        {
            if (value.GetIsLongestSupported())
                throw new ArgumentOutOfRangeException(nameof(value));

            return value + 1;
        }

        public static bool GetIsShortestSupported(this BaseNoteValue value)
        {
            return value == BaseNoteValue.TwoHundredFiftySixth;
        }

        public static bool GetIsLongestSupported(this BaseNoteValue value)
        {
            return value == BaseNoteValue.Large;
        }
    }
}
