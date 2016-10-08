using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    [DebuggerDisplay("bar: {Range.Content}")]
    class BarNode : TopLevelNode, IDocumentElementFactory<Bar>
    {

        private static Rhythm ApplyRhythmTemplate(Rhythm template, Rhythm rhythm, IReporter reporter)
        {
            if (rhythm == null)
                return template;

            if (rhythm.Segments.Count == 0) // empty rhythm, should be filled with rest
                return rhythm;

            if (rhythm.Segments.Any(s => s.Voices.Count != 0))  // rhythm already defined
                return rhythm;

            if (rhythm.Segments.Count > template.Segments.Count)
            {
                reporter.Report(ReportLevel.Warning, rhythm.Range,
                                Messages.Warning_TooManyChordsToMatchRhythmTemplate);

                for (var i = 0; i < template.Segments.Count; ++i)
                {
                    rhythm.Segments[i].Voices.AddRange(template.Segments[i].Voices);
                }

                for (var i = template.Segments.Count; i < rhythm.Segments.Count; ++i)
                {
                    rhythm.Segments[i].IsOmittedByTemplate = true;
                }
            }
            else if (rhythm.Segments.Count < template.Segments.Count && rhythm.Segments.Count != 1)
            {
                reporter.Report(ReportLevel.Warning, rhythm.Range,
                                Messages.Warning_InsufficientChordsToMatchRhythmTemplate);

                var lastChord = rhythm.Segments[rhythm.Segments.Count - 1].Chord;

                for (var i = 0; i < rhythm.Segments.Count; ++i)
                {
                    rhythm.Segments[i].Voices.AddRange(template.Segments[i].Voices);
                }

                for (var i = rhythm.Segments.Count; i < template.Segments.Count; ++i)
                {
                    var segment = template.Segments[i].Clone();
                    segment.Chord = lastChord;
                    rhythm.Segments.Add(segment);
                }
            }
            else
            {
                for (var i = 0; i < template.Segments.Count; ++i)
                {
                    rhythm.Segments[i].Voices.AddRange(template.Segments[i].Voices);
                }
            }

            return rhythm;
        }

        public LiteralNode<OpenBarLine> OpenLine { get; set; }
        public LiteralNode<CloseBarLine> CloseLine { get; set; }
        public RhythmNode Rhythm { get; set; }
        public LyricsNode Lyrics { get; set; }

        public override IEnumerable<Node> Children
        {
            get
            {
                if (this.OpenLine != null)
                    yield return this.OpenLine;
                if (this.Rhythm != null)
                    yield return this.Rhythm;
                if (this.Lyrics != null)
                    yield return this.Lyrics;
                if (this.CloseLine != null)
                    yield return this.CloseLine;
            }
        }

        internal override bool Apply(TablatureContext context, IReporter reporter)
        {
            Bar bar;
            if (!this.ToDocumentElement(context, reporter, out bar))
                return false;

            context.AddBar(bar);

            return true;
        }

        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out Bar bar)
        {
            Rhythm rhythm;
            if (this.Rhythm == null)
                rhythm = null;
            else
            {
                if (!this.Rhythm.ToDocumentElement(context, reporter, out rhythm))
                {
                    bar = null;
                    return false;
                }

                if (context.DocumentState.RhythmTemplate != null)
                    rhythm = BarNode.ApplyRhythmTemplate(context.DocumentState.RhythmTemplate, rhythm, reporter);
            }

            Lyrics lyrics;
            if (this.Lyrics == null)
                lyrics = null;
            else if (!this.Lyrics.ToDocumentElement(context, reporter, out lyrics))
            {
                bar = null;
                return false;
            }

            if (rhythm != null && lyrics != null)
            {
                var beats = rhythm.Segments.SelectMany(s => s.Voices[0].Beats).Count();
                if (beats < lyrics.Segments.Count)
                {
                    reporter.Report(ReportLevel.Suggestion, lyrics.Range, Messages.Suggestion_LyricsTooLong);
                }
            }

            bar = new Bar
            {
                OpenLine = this.OpenLine?.Value,
                CloseLine = this.CloseLine?.Value,
                Rhythm = rhythm,
                Lyrics = lyrics,
                Range = this.Range
            };

            return true;
        }
    }
}
