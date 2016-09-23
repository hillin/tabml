using TabML.Core.MusicTheory;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing
{
    class NoteNameParser : ParserBase<NoteNameNode>
    {
        public override bool TryParse(Scanner scanner, out NoteNameNode result)
        {
            result = new NoteNameNode();

            var anchor = scanner.MakeAnchor();

            LiteralNode<BaseNoteName> baseNoteNameNode;
            if (!Parser.TryReadBaseNoteName(scanner, this, out baseNoteNameNode))
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_InvalidNoteName);
                result = null;
                return false;
            }

            result.BaseNoteName = baseNoteNameNode;

            LiteralNode<Accidental> accidentalNode;
            if (!Parser.TryReadAccidental(scanner, this, out accidentalNode))
            {
                result = null;
                return false;
            }

            result.Accidental = accidentalNode;
            result.Range = anchor.Range;

            return true;
        }
    }
}
