using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    abstract class RhythmSegmentNodeBase : Node
    {
        public List<VoiceNode> Voices { get; }

        public override IEnumerable<Node> Children => this.Voices;


        protected RhythmSegmentNodeBase()
        {
            this.Voices = new List<VoiceNode>();
        }
        
        protected bool FillRhythmSegmentVoices(TablatureContext context, IReporter reporter, RhythmSegmentBase rhythmSegment)
        {
            if (this.Voices.Count > 0)
            {
                var maxDuration = this.Voices.Max(v => v.GetDuration());

                foreach (var voice in this.Voices)
                {
                    voice.ExpectedDuration = maxDuration;

                    Voice documentVoice;
                    if (!voice.ToDocumentElement(context, reporter, out documentVoice))
                        return false;

                    rhythmSegment.Voices.Add(documentVoice);
                }
            }
            return true;
        }
    }
}