using TabML.Core.MusicTheory;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing
{
    class NoteValueParser : ParserBase<NoteValueNode>
    {
        public override bool TryParse(Scanner scanner, out NoteValueNode result)
        {
            var anchor = scanner.MakeAnchor();
            result = new NoteValueNode();

            LiteralNode<BaseNoteValue> baseNoteValue;
            if (!Parser.TryReadBaseNoteValue(scanner, this, out baseNoteValue))
            {
                result = null;
                return false;
            }

            result.Base = baseNoteValue;

            if (scanner.Expect('/'))    //tuplet
            {
                LiteralNode<int> tuplet;
                if (!Parser.TryReadInteger(scanner, out tuplet))
                {
                    this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_NoteValueExpected);
                    result = null;
                    return false;
                }

                if (!NoteValue.IsValidTuplet(tuplet.Value))
                {
                    this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_InvalidTuplet);
                    result = null;
                    return false;
                }

                result.Tuplet = tuplet;
            }

            LiteralNode<NoteValueAugment> augment;
            if (!Parser.TryReadNoteValueAugment(scanner, this, out augment))
            {
                result = null;
                return false;
            }

            result.Augment = augment;

            result.Range = anchor.Range;
            return true;
        }
    }
}
