using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    static class VoicePartExtensions
    {
        public static OffBarDirection ToOffBarDirection(this VoicePart voicePart)
        {
            switch (voicePart)
            {
                case VoicePart.Treble:
                    return OffBarDirection.Top;
                case VoicePart.Bass:
                    return OffBarDirection.Bottom;
                default:
                    throw new ArgumentOutOfRangeException(nameof(voicePart), voicePart, null);
            }
        }
    }
}
