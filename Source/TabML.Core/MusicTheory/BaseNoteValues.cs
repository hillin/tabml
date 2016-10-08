using System;
using System.Collections.Generic;

namespace TabML.Core.MusicTheory
{
    public static class BaseNoteValues
    {
        public static bool TryParse(int reciprocalValue, out BaseNoteValue value)
        {
            switch (reciprocalValue)
            {
                case 1: value = BaseNoteValue.Whole; break;
                case 2: value = BaseNoteValue.Half; break;
                case 4: value = BaseNoteValue.Quater; break;
                case 8: value = BaseNoteValue.Eighth; break;
                case 16: value = BaseNoteValue.Sixteenth; break;
                case 32: value = BaseNoteValue.ThirtySecond; break;
                case 64: value = BaseNoteValue.SixtyFourth; break;
                case 128: value = BaseNoteValue.HundredTwentyEighth; break;
                case 256: value = BaseNoteValue.TwoHundredFiftySixth; break;
                default:
                    value = BaseNoteValue.Whole;
                    return false;
            }

            return true;
        }

        public static bool TryFactorize(double duration, out BaseNoteValue[] values)
        {
            var valueList = new List<BaseNoteValue>();
            var currentNoteValue = BaseNoteValue.Large;
            var currentDuration = currentNoteValue.GetDuration();
            while (currentNoteValue >= BaseNoteValue.TwoHundredFiftySixth)
            {
                if (duration < currentDuration)
                {
                    --currentNoteValue;
                    currentDuration = currentNoteValue.GetDuration();
                    continue;
                }

                valueList.Add(currentNoteValue);
                duration -= currentDuration;
            }

            values = valueList.ToArray();

            return Math.Abs(duration) < 1e-7;
        }
    }
}
