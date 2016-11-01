using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Bar
{
    class BeatNoteParser : ParserBase<BeatNoteNode>
    {
        public override bool TryParse(Scanner scanner, out BeatNoteNode result)
        {
            var anchor = scanner.MakeAnchor();
            result = new BeatNoteNode();

            LiteralNode<PreNoteConnection> preConnection;
            Parser.TryReadPreNoteConnection(scanner, this, out preConnection);
            result.PreConnection = preConnection;

            LiteralNode<int> stringNumber;
            if (!Parser.TryReadInteger(scanner, out stringNumber))
            {
                this.Report(ReportLevel.Error, scanner.LastReadRange,
                            Messages.Error_BeatInvalidStringNumberInStringsSpecifier);
                result = null;
                return false;
            }

            result.String = stringNumber;

            if (scanner.Expect('='))
            {
                LiteralNode<int> fret;
                if (!Parser.TryReadInteger(scanner, out fret))
                {
                    this.Report(ReportLevel.Error, scanner.LastReadRange,
                                Messages.Error_BeatInvalidFretNumberInStringsSpecifier);
                    result = null;
                    return false;
                }

                result.Fret = fret;
            }

            scanner.SkipWhitespaces();
            if (scanner.Expect(':'))
            {
                LiteralNode<NoteEffectTechnique> noteEffectTechnique;
                LiteralNode<double> techniqueParameter;
                if (Parser.TryReadNoteEffectTechnique(scanner, this, out noteEffectTechnique, out techniqueParameter))
                {
                    result.EffectTechnique = noteEffectTechnique;
                    result.EffectTechniqueParameter = techniqueParameter;
                }
            }

            scanner.SkipWhitespaces();
            LiteralNode<PostNoteConnection> postConnection;
            Parser.TryReadPostNoteConnection(scanner, this, out postConnection);
            result.PostConnection = postConnection;

            result.Range = anchor.Range;
            return true;
        }
    }
}
