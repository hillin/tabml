using System.Collections.Generic;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class BeatNoteNode : Node, IValueEquatable<BeatNoteNode>, IDocumentElementFactory<BeatNote>
    {
        public LiteralNode<int> String { get; set; }
        public LiteralNode<int> Fret { get; set; }
        public LiteralNode<PreNoteConnection> PreConnection { get; set; }
        public LiteralNode<PostNoteConnection> PostConnection { get; set; }

        public override IEnumerable<Node> Children
        {
            get
            {
                if (this.PreConnection != null)
                    yield return this.PreConnection;

                yield return this.String;

                if (this.Fret != null)
                    yield return this.Fret;

                if (this.PostConnection != null)
                    yield return this.PostConnection;
            }
        }

        public bool ValueEquals(BeatNoteNode other)
        {
            if (other == null)
                return false;

            return ValueEquatable.ValueEquals(this.String, other.String)
                   && ValueEquatable.ValueEquals(this.Fret, other.Fret)
                   && ValueEquatable.ValueEquals(this.PreConnection, other.PreConnection)
                   && ValueEquatable.ValueEquals(this.PostConnection, other.PostConnection);
        }

        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out BeatNote element)
        {
            var documentState = context.DocumentState;
            if (this.Fret != null
                && this.Fret.Value + documentState.MinimumCapoFret < documentState.CapoFretOffsets[this.String.Value])
            {
                reporter.Report(ReportLevel.Warning, this.Fret.Range,
                                Messages.Warning_FretUnderCapo, this.String.Value,
                                this.Fret.Value);
            }

            element = new BeatNote
            {
                PreConnection = this.PreConnection?.Value ?? PreNoteConnection.None,
                PostConnection = this.PostConnection?.Value ?? PostNoteConnection.None,
                String = this.String.Value,
                Fret = this.Fret?.Value ?? BeatNote.UnspecifiedFret
            };

            return true;
        }
    }
}
