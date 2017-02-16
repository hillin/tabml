using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    static class BarExtensions
    {
        public static bool HasSingularVoice(this Bar bar)
        {
            return bar.BassVoice == null
                || bar.BassVoice.BeatElements.Count == 0
                || bar.TrebleVoice == null
                || bar.TrebleVoice.BeatElements.Count == 0;
        }
    }
}
