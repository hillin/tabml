using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.MusicTheory
{
    public enum BeatModifier
    {
        Accent = 1,
        Marcato = 2,
        Staccato = 11,
        Staccatissimo = 12,
        Tenuto = 13,
        Fermata = 14,
        PickstrokeDown = 21,
        PickstrokeUp = 22,
        Trill = 31,
        Mordent = 32,
        LowerMordent = 33,
        Turn = 34,
        InvertedTurn = 35
    }
}
