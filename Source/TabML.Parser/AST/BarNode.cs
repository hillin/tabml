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


            if (bar.Rhythm != null && bar.Lyrics != null)
            {
                var beats = bar.Rhythm.Segments.SelectMany(s => s.Voices[0].Beats).Count();
                if (beats < bar.Lyrics.Segments.Count)
                {
                    reporter.Report(ReportLevel.Suggestion, bar.Lyrics.Range, Messages.Suggestion_LyricsTooLong);
                }
            }

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
                    rhythm = context.DocumentState.RhythmTemplate.Apply(rhythm, reporter);
            }

            Lyrics lyrics;
            if (this.Lyrics == null)
                lyrics = null;
            else if (!this.Lyrics.ToDocumentElement(context, reporter, out lyrics))
            {
                bar = null;
                return false;
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
