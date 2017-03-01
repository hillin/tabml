using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.MusicTheory
{
    public static class OrnamentExtensions
    {
        public static BeatModifier ToBeatModifier(this Ornament ornament)
        {
            return (BeatModifier) ornament;
        }
    }
}
