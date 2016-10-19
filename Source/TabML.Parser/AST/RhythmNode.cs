using System;
using System.Collections.Generic;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class RhythmNode : Node, IDocumentElementFactory<Rhythm>
    {
        public List<RhythmSegmentNode> Segments { get; }

        public RhythmNode()
        {
            this.Segments = new List<RhythmSegmentNode>();
        }

        public override IEnumerable<Node> Children => this.Segments;

        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out Rhythm rhythm)
        {
            rhythm = new Rhythm
            {
                Range = this.Range
            };

            var duration = PreciseDuration.Zero;

            foreach (var segment in this.Segments)
            {
                RhythmSegment rhythmSegment;
                if (!segment.ToDocumentElement(context, reporter, out rhythmSegment))
                    return false;

                rhythm.Segments.Add(rhythmSegment);
                duration += segment.GetDuration();
            }

            // duration could be 0 if rhythm is not defined (only chord defined), rhythm will be determined by the rhythm instruction
            if (duration > 0 && duration == context.DocumentState.TimeSignature.Time.GetDuration())
            {
                reporter.Report(ReportLevel.Warning, this.Range, Messages.Warning_BeatsNotMatchingTimeSignature);
                rhythm.NotMatchingTime = true;
            }

            return true;
        }
    }
}
