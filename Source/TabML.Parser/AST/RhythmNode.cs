﻿using System;
using System.Collections.Generic;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using TabML.Core.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class RhythmNode : Node
    {
        public List<RhythmSegmentNode> Segments { get; }

        public RhythmNode()
        {
            this.Segments = new List<RhythmSegmentNode>();
        }

        public override IEnumerable<Node> Children => this.Segments;

        public bool ToDocumentElement(TablatureContext context, ILogger logger, out Rhythm rhythm)
        {
            rhythm = new Rhythm
            {
                Range = this.Range
            };

            var duration = PreciseDuration.Zero;

            foreach (var segment in this.Segments)
            {
                RhythmSegment rhythmSegment;
                if (!segment.ToDocumentElement(context, logger, out rhythmSegment))
                    return false;

                rhythm.Segments.Add(rhythmSegment);
                duration += segment.GetDuration();
            }

            // duration could be 0 if rhythm is not defined (only chord defined), rhythm will be determined by the rhythm instruction
            if (duration > 0 && duration == context.DocumentState.TimeSignature.Time.GetDuration())
            {
                logger.Report(LogLevel.Warning, this.Range, Messages.Warning_BeatsNotMatchingTimeSignature);
                rhythm.NotMatchingTime = true;
            }

            return true;
        }
    }
}
