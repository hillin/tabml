using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;
using TabML.Core.Style;

namespace TabML.Core.Document
{
   public static   class VoicePartExtensions
    {
        public static VerticalDirection GetDefaultTiePosition(this VoicePart voicePart)
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
