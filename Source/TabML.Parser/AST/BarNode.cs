using System;
using System.Collections.Generic;
using System.Diagnostics;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    [DebuggerDisplay("bar: {Range.Content}")]
    class BarNode : TopLevelNode
    {
        public LiteralNode<OpenBarLine> OpenLine { get; set; }
        public LiteralNode<CloseBarLine> CloseLine { get; set; }
        public RhythmNode Rhythm { get; set; }
        public LyricsNode Lyrics { get; set; }

        public DocumentState DocumentState { get; set; }

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
            this.Rhythm.Apply(context, reporter);
            this.Lyrics.Apply(context, reporter);
            // todo: validate lyrics-rhythm matching

            Rhythm rhythm;
            if (!this.Rhythm.ToDocumentElement(context, reporter, out rhythm))
                return false;

            Lyrics lyrics;
            if (!this.Lyrics.ToDocumentElement(context, reporter, out lyrics))
                return false;

            var bar = new Bar()
            {
                OpenLine = this.OpenLine?.Value,
                CloseLine = this.CloseLine?.Value,
                Rhythm = rhythm,
                Lyrics = lyrics,
                Range = this.Range
            };

            context.AddBar(bar);

            return true;
        }
        
    }
}
