using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Core
{
    public static class Defaults
    {
        public const int Strings = 6;
        public static readonly Tuning Tuning = Tunings.Standard;
        public static readonly CapoInfo Capo = CapoInfo.NoCapo;
        public static readonly Tempo Tempo = new Tempo(72);
        public static readonly Time Time = Times.T44;
    }
}
