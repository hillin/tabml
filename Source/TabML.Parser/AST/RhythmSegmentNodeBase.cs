using System;
using System.Collections.Generic;
using System.Linq;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    abstract class RhythmSegmentNodeBase : Node, IValueEquatable<RhythmSegmentNodeBase>, IDocumentElementFactory<RhythmSegment>
    {
        public List<VoiceNode> Voices { get; }

        public override IEnumerable<Node> Children => this.Voices;


        protected RhythmSegmentNodeBase()
        {
            this.Voices = new List<VoiceNode>();
        }

        protected bool ValueEquals(RhythmSegmentNodeBase other)
        {
            if (other == null)
                return false;

            return other.Voices.Count == this.Voices.Count
                && !this.Voices.Where((v, i) => !v.ValueEquals(other.Voices[i])).Any();
        }

        bool IValueEquatable<RhythmSegmentNodeBase>.ValueEquals(RhythmSegmentNodeBase other)
        {
            return this.ValueEquals(other);
        }

        public virtual bool ToDocumentElement(TablatureContext context, IReporter reporter, out RhythmSegment rhythmSegment)
        {
            rhythmSegment = new RhythmSegment
            {
                Range = this.Range
            };

            var maxDuration = this.Voices.Max(v => v.GetDuration());

            foreach (var voice in this.Voices)
            {
                voice.ExpectedDuration = maxDuration;

                Voice documentVoice;
                if (!voice.ToDocumentElement(context, reporter, out documentVoice))
                    return false;

                rhythmSegment.Voices.Add(documentVoice);
            }

            return true;
        }
    }
}