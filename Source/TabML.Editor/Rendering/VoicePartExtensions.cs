using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.Style;

namespace TabML.Editor.Rendering
{
    static class VoicePartExtensions
    {
        public static VerticalDirection ToDirection(this VoicePart voicePart)
        {
            switch (voicePart)
            {
                case VoicePart.Treble:
                    return VerticalDirection.Above;
                case VoicePart.Bass:
                    return VerticalDirection.Under;
                default:
                    throw new ArgumentOutOfRangeException(nameof(voicePart), voicePart, null);
            }
        }
    }
}
