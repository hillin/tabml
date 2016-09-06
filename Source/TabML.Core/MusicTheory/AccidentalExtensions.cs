using System;

namespace TabML.Core.MusicTheory
{
    public static class AccidentalExtensions
    {
        public static int GetSemitoneOffset(this Accidental accidental)
        {
            switch (accidental)
            {
                case Accidental.Natural:
                    return 0;
                case Accidental.Sharp:
                    return 1;
                case Accidental.Flat:
                    return -1;
                case Accidental.DoubleSharp:
                    return 2;
                case Accidental.DoubleFlat:
                    return -2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(accidental), accidental, null);
            }
        }

        public static bool GetIsDoubleAccidental(this Accidental accidental)
        {
            return accidental == Accidental.DoubleFlat || accidental == Accidental.DoubleSharp;
        }
    }
}
