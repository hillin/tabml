using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Core.Document
{
   public static   class VoicePartExtensions
    {
        public static TiePosition GetDefaultTiePosition(this VoicePart voicePart)
        {
            switch (voicePart)
            {
                case VoicePart.Treble:
                    return TiePosition.Above;
                case VoicePart.Bass:
                    return TiePosition.Under;
                default:
                    throw new ArgumentOutOfRangeException(nameof(voicePart), voicePart, null);
            }
        }
    }
}
