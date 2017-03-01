using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.MusicTheory
{
    public static class BeatAccentExtensions
    {
        public static BeatModifier ToBeatModifier(this BeatAccent accent)
        {
            return (BeatModifier) accent;
        }
    }
}
