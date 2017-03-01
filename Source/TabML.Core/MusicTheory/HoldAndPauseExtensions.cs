using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.MusicTheory
{
    public static class HoldAndPauseExtensions
    {
        public static BeatModifier ToBeatModifier(this HoldAndPause holdAndPause)
        {
            return (BeatModifier) holdAndPause;
        }
    }
}
