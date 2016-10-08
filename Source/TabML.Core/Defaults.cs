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
        public static readonly TempoSignature Tempo = new TempoSignature(72);
        public static readonly TimeSignature Time = TimeSignatures.T44;
    }
}
