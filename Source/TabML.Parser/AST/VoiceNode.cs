using System.Collections.Generic;
using TabML.Parser.Document;
using TabML.Parser.Parsing;
using System.Linq;
using TabML.Core.MusicTheory;

namespace TabML.Parser.AST
{
    class VoiceNode : Node, IValueEquatable<VoiceNode>, IDocumentElementFactory<Voice>
    {
        public List<BeatNode> Beats { get; }
        public double ExpectedDuration { get; set; }

        public override IEnumerable<Node> Children => this.Beats;

        public VoiceNode()
        {
            this.Beats = new List<BeatNode>();
        }

        public double GetDuration() => this.Beats.Sum(b => b.NoteValue.ToNoteValue().GetDuration());

        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out Voice voice)
        {
            voice = new Voice()
            {
                Range = this.Range
            };

            foreach (var beat in this.Beats)
            {
                Beat documentBeat;
                if (!beat.ToDocumentElement(context, reporter, out documentBeat))
                    return false;

                voice.Beats.Add(documentBeat);
            }

            var duration = this.GetDuration();
            if (duration < this.ExpectedDuration)
            {
                BaseNoteValue[] factors;
                if (!BaseNoteValues.TryFactorize(this.ExpectedDuration - duration, out factors))
                {
                    reporter.Report(ReportLevel.Error, this.Range,
                                    Messages.Error_InconsistentVoiceDurationCannotBeFilledWithRest);
                    return false;
                }

                reporter.Report(ReportLevel.Suggestion, this.Range, Messages.Suggestion_InconsistentVoiceDuration);

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

                    voice.Beats.Add(beat);
                }
            }

            return true;
        }

        public bool ValueEquals(VoiceNode other)
        {
            if (other == null)
                return false;

            return other.Beats.Count == this.Beats.Count
                   && this.Beats.Where((b, i) => !b.ValueEquals(other.Beats[i])).Any();
        }
    }
}
