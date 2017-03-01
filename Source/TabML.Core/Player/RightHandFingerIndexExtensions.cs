using System;

namespace TabML.Core.Player
{
    public static class RightHandFingerIndexExtensions
    {
        public static string ToShortString(this RightHandFingerIndex index)
        {
            switch (index)
            {
                case RightHandFingerIndex.Pulgar:
                    return "p";
                case RightHandFingerIndex.Indice:
                    return "i";
                case RightHandFingerIndex.Medio:
                    return "m";
                case RightHandFingerIndex.Anular:
                    return "a";
                case RightHandFingerIndex.Chico:
                    return "c";
                default:
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
        }
    }
}
