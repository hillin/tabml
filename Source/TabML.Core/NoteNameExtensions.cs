using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public static class NoteNameExtensions
    {
        public static Accidental GetAccidental(this NoteName noteName)
        {
            switch (noteName)
            {
                case NoteName.C:
                case NoteName.D:
                case NoteName.E:
                case NoteName.F:
                case NoteName.G:
                case NoteName.A:
                case NoteName.B:
                    return Accidental.Natural;
                case NoteName.CSharp:
                case NoteName.DSharp:
                case NoteName.ESharp:
                case NoteName.FSharp:
                case NoteName.GSharp:
                case NoteName.ASharp:
                case NoteName.BSharp:
                    return Accidental.Sharp;
                case NoteName.CFlat:
                case NoteName.DFlat:
                case NoteName.EFlat:
                case NoteName.FFlat:
                case NoteName.GFlat:
                case NoteName.AFlat:
                case NoteName.BFlat:
                    return Accidental.Flat;
                default:
                    throw new ArgumentOutOfRangeException(nameof(noteName), noteName, null);
            }
        }

        public static int GetTET12Value(this NoteName noteName)
        {
            switch (noteName)
            {
                case NoteName.BSharp:
                case NoteName.C:
                    return 0;
                case NoteName.CSharp:
                case NoteName.DFlat:
                    return 1;
                case NoteName.D:
                    return 2;
                case NoteName.DSharp:
                case NoteName.EFlat:
                    return 3;
                case NoteName.E:
                case NoteName.FFlat:
                    return 4;
                case NoteName.ESharp:
                case NoteName.F:
                    return 5;
                case NoteName.FSharp:
                case NoteName.GFlat:
                    return 6;
                case NoteName.G:
                    return 7;
                case NoteName.GSharp:
                case NoteName.AFlat:
                    return 8;
                case NoteName.A:
                    return 9;
                case NoteName.ASharp:
                case NoteName.BFlat:
                    return 10;
                case NoteName.B:
                case NoteName.CFlat:
                    return 11;
                default:
                    throw new ArgumentOutOfRangeException(nameof(noteName), noteName, null);
            }
        }
    }
}
