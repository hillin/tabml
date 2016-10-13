using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    abstract class RhythmSegmentNodeBase : Node
    {
        public VoiceNode TrebleVoice { get; set; }
        public VoiceNode BassVoice { get; set; }

        public VoiceNode FirstVoice => this.TrebleVoice ?? this.BassVoice;

        public override IEnumerable<Node> Children
        {
            get
            {
                if (this.TrebleVoice != null)
                    yield return this.TrebleVoice;

                if (this.BassVoice != null)
                    yield return this.BassVoice;
            }
        }


        public PreciseDuration GetDuration()
        {
            return
                new PreciseDuration(Math.Max(this.BassVoice?.GetDuration().FixedPointValue ?? 0,
                                             this.TrebleVoice?.GetDuration().FixedPointValue ?? 0));
        }

        protected bool FillRhythmSegmentVoices(TablatureContext context, IReporter reporter, RhythmSegmentBase rhythmSegment)
        {

            var duration = this.GetDuration();

            if (this.TrebleVoice != null)
            {
                this.TrebleVoice.ExpectedDuration = duration;

                Voice trebleVoice;
                if (!this.TrebleVoice.ToDocumentElement(context, reporter, out trebleVoice))
                    return false;

                trebleVoice.Part = VoicePart.Treble;

                rhythmSegment.TrebleVoice = trebleVoice;
            }

            if (this.BassVoice != null)
            {
                this.BassVoice.ExpectedDuration = duration;

                Voice bassVoice;
                if (!this.BassVoice.ToDocumentElement(context, reporter, out bassVoice))
                    return false;

                bassVoice.Part = VoicePart.Bass;

                rhythmSegment.BassVoice = bassVoice;
            }

            return true;
        }
    }
}