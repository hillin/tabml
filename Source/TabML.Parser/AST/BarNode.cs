using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using TabML.Core.Document;
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

        internal override bool Apply(TablatureContext context, ILogger logger)
        {
            Bar bar;
            if (!this.ToDocumentElement(context, logger, out bar))
                return false;


            if (bar.Rhythm != null && bar.Lyrics != null)
            {
                var beats = bar.Rhythm.Segments.Sum(s => s.FirstVoice.Beats?.Count ?? 0);
                if (beats < bar.Lyrics.Segments.Count)
                    logger.Report(LogLevel.Suggestion, bar.Lyrics.Range, Messages.Suggestion_LyricsTooLong);
            }

            context.AddBar(bar);

            return true;
        }

        public bool ToDocumentElement(TablatureContext context, ILogger logger, out Bar bar)
        {
            Rhythm rhythm;
            if (this.Rhythm == null)
                rhythm = null;
            else
            {
                if (!this.Rhythm.ToDocumentElement(context, logger, out rhythm))
                {
                    bar = null;
                    return false;
                }

                if (context.DocumentState.RhythmTemplate != null)
                    rhythm = context.DocumentState.RhythmTemplate.Apply(rhythm, logger);
            }

            Lyrics lyrics;
            if (this.Lyrics == null)
                lyrics = null;
            else if (!this.Lyrics.ToDocumentElement(context, logger, out lyrics))
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
