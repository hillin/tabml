using System.Collections.Generic;
using TabML.Core.MusicTheory;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class BeatNoteNode : Node, IDocumentElementFactory<BeatNote>
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

        public bool ValueEquals(BeatNote other)
        {
            if (other == null)
                return false;

            if (this.String.Value != other.String)
                return false;

            if ((this.Fret?.Value ?? BeatNote.UnspecifiedFret) != other.Fret)
                return false;

            if ((this.PreConnection?.Value ?? PreNoteConnection.None) != other.PreConnection)
                return false;

            if ((this.PostConnection?.Value ?? PostNoteConnection.None) != other.PostConnection)
                return false;

            return true;
        }

        public bool ToDocumentElement(TablatureContext context, IReporter reporter, out BeatNote element)
        {
            var documentState = context.DocumentState;
            if (this.Fret != null
                && this.Fret.Value + documentState.MinimumCapoFret < (documentState.CapoFretOffsets?[this.String.Value] ?? 0))
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
