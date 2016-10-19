using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;

namespace TabML.Editor.Tablature.Layout
{
    class ArrangedBarBeat : IBeamElement
    {
        public PreciseDuration Position { get; set; }
        public VoicePart VoicePart { get; }
        public Beat Beat { get; }

        public ArrangedBarBeat(Beat beat, VoicePart voicePart)
        {
            this.Beat = beat;
            this.VoicePart = voicePart;
        }

        public PreciseDuration GetDuration()
        {
            return this.Beat.GetDuration();
        }
    }
}
