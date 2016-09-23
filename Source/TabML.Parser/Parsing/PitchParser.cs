using TabML.Parser.AST;

namespace TabML.Parser.Parsing
{
    class PitchParser : ParserBase<PitchNode>
    {
        public override bool TryParse(Scanner scanner, out PitchNode result)
        {
            result = new PitchNode();
            var anchor = scanner.MakeAnchor();

            NoteNameNode noteNameNode;
            if (!new NoteNameParser().TryParse(scanner, out noteNameNode))
            {
                result = null;
                return false;
            }

            result.NoteName = noteNameNode;

            int octave;
            if (scanner.TryReadInteger(out octave))
            {
                result.OctaveGroup = new LiteralNode<int>(octave, scanner.LastReadRange);
            }

            result.Range = anchor.Range;

            return true;
        }
    }
}
