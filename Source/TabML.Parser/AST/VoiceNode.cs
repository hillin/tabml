using System.Collections.Generic;
using TabML.Core.Document;
using TabML.Parser.Parsing;
using System.Linq;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using System;

namespace TabML.Parser.AST
{
    class VoiceNode : Node
    {
        public List<BeatNode> Beats { get; }
        public PreciseDuration ExpectedDuration { get; set; }

        public override IEnumerable<Node> Children => this.Beats;

        public VoiceNode()
        {
            this.Beats = new List<BeatNode>();
        }

        public PreciseDuration GetDuration() => this.Beats.Sum(b => b.NoteValue.ToNoteValue().GetDuration());

        public bool ToDocumentElement(TablatureContext context, ILogger logger, VoicePart voicePart, out RhythmSegmentVoice voice)
        {
            voice = new RhythmSegmentVoice(voicePart)
            {
                Range = this.Range
            };

            foreach (var beat in this.Beats)
            {
                Beat documentBeat;
                if (!beat.ToDocumentElement(context, logger, voicePart, out documentBeat))
                    return false;

                if (context.CurrentBar != null) // if context.CurrentBar is null, it means we are in a template
                                                // todo: this is ugly, refactor it
                    context.CurrentBar.SetVoiceRestedState(voicePart, documentBeat.IsRest);

                voice.BeatElements.Add(documentBeat);
            }

            var duration = this.GetDuration();
            if (duration < this.ExpectedDuration)
            {
                BaseNoteValue[] factors;
                if (!BaseNoteValues.TryFactorize(this.ExpectedDuration - duration, out factors))
                {
                    logger.Report(LogLevel.Error, this.Range,
                                    Messages.Error_InconsistentVoiceDurationCannotBeFilledWithRest);
                    return false;
                }

                logger.Report(LogLevel.Suggestion, this.Range, Messages.Suggestion_InconsistentVoiceDuration);

                var isFirstFactor = true;
                foreach (var factor in factors)
                {
                    var beat = new Beat
                    {
                        NoteValue = new NoteValue(factor),
                        IsRest = true,
                        IsTied = !isFirstFactor
                    };

                    isFirstFactor = false;

                    if (context.CurrentBar != null)  // if context.CurrentBar is null, it means we are in a template
                                                     // todo: this is ugly, refactor it
                        context.CurrentBar.SetVoiceRestedState(voicePart, true);
                    voice.BeatElements.Add(beat);
                }
            }

            return true;
        }

        public bool ValueEquals(RhythmSegmentVoice other)
        {
            if (other == null)
                return false;

            return other.BeatElements.Count == this.Beats.Count
                   && this.Beats.Where((b, i) => !b.ValueEquals(other.BeatElements[i])).Any();
        }


    }
}
