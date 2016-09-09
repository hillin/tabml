using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.MusicTheory
{
    public static class Accidentals
    {
        public static bool TryParse(string text, out Accidental accidental)
        {
            switch (text)
            {
                case "":
                    accidental = Accidental.Natural; break;
                case "b":
                case "♭":
                    accidental = Accidental.Flat; break;
                case "bb":
                case "♭♭":
                case "\u1d12b":
                    accidental = Accidental.DoubleFlat; break;
                case "#":
                case "♯":
                    accidental = Accidental.Sharp; break;
                case "##":
                case "♯♯":
                case "\u1d12a":
                    accidental = Accidental.DoubleSharp; break;
                default:
                    accidental = Accidental.Natural;
                    return false;
            }

            return true;
        }
    }
}
