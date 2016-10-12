using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public static class LeftHandFingerIndexExtensions
    {
        public static string ToShortString(this LeftHandFingerIndex index)
        {
            switch (index)
            {
                case LeftHandFingerIndex.Thumb:
                    return "T";
                case LeftHandFingerIndex.Index:
                    return "1";
                case LeftHandFingerIndex.Middle:
                    return "2";
                case LeftHandFingerIndex.Ring:
                    return "3";
                case LeftHandFingerIndex.Pinky:
                    return "4";
                default:
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
        }
    }
}
