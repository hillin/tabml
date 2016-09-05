using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public static class BaseNoteNameExtensions
    {
        public static int GetSemitones(this BaseNoteName noteName)
        {
            switch (noteName)
            {
                case BaseNoteName.C:
                    return 0;
                case BaseNoteName.D:
                    return 2;
                case BaseNoteName.E:
                    return 4;
                case BaseNoteName.F:
                    return 5;
                case BaseNoteName.G:
                    return 7;
                case BaseNoteName.A:
                    return 9;
                case BaseNoteName.B:
                    return 11;
                default:
                    throw new ArgumentOutOfRangeException(nameof(noteName), noteName, null);
            }
        }


        public static int GetAbsoluteDegrees(this BaseNoteName noteName)
        {
            switch (noteName)
            {
                case BaseNoteName.C:
                    return 0;
                case BaseNoteName.D:
                    return 1;
                case BaseNoteName.E:
                    return 2;
                case BaseNoteName.F:
                    return 3;
                case BaseNoteName.G:
                    return 4;
                case BaseNoteName.A:
                    return 5;
                case BaseNoteName.B:
                    return 6;
                default:
                    throw new ArgumentOutOfRangeException(nameof(noteName), noteName, null);
            }
        }

    }
}
