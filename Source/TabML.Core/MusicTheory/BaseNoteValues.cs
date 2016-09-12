using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
