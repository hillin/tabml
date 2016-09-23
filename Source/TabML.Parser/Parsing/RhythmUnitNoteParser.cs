using TabML.Core.MusicTheory;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing
{
    class RhythmUnitNoteParser : ParserBase<RhythmUnitNoteNode>
    {
        public override bool TryParse(Scanner scanner, out RhythmUnitNoteNode result)
        {
            var anchor = scanner.MakeAnchor();
            result = new RhythmUnitNoteNode();

            LiteralNode<PreNoteConnection> preConnection;
            Parser.TryReadPreNoteConnection(scanner, this, out preConnection);
            result.PreConnection = preConnection;

            LiteralNode<int> stringNumber;
            if (!Parser.TryReadInteger(scanner, out stringNumber))
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                            ParseMessages.Error_RhythmUnitInvalidStringNumberInStringsSpecifier);
                result = null;
                return false;
            }

            result.String = stringNumber;

            if (scanner.Expect('='))
            {
                LiteralNode<int> fret;
                if (!Parser.TryReadInteger(scanner, out fret))
                {
                    this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                                ParseMessages.Error_RhythmUnitInvalidFretNumberInStringsSpecifier);
                    result = null;
                    return false;
                }

                result.Fret = fret;
            }

            LiteralNode<PostNoteConnection> postConnection;
            Parser.TryReadPostNoteConnection(scanner, this, out postConnection);
            result.PostConnection = postConnection;

            result.Range = anchor.Range;
            return true;
        }
    }
}
