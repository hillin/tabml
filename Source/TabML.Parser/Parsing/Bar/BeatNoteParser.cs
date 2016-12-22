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

            ExistencyNode tieNode;
            LiteralNode<TiePosition> tiePosition;

            Parser.TryReadTie(scanner, this, out tieNode, out tiePosition);
            result.Tie = tieNode;
            result.TiePosition = tiePosition;

            var isTied = tieNode != null;

            LiteralNode<PreNoteConnection> preConnection;
            Parser.TryReadPreNoteConnection(scanner, this, out preConnection);
            result.PreConnection = preConnection;

            if (isTied && result.PreConnection != null)
            {
                this.Report(LogLevel.Warning, scanner.LastReadRange,
                            Messages.Warning_PreConnectionInTiedNote);
            }

            LiteralNode<int> stringNumber;
            if (!Parser.TryReadInteger(scanner, out stringNumber))
            {
                this.Report(LogLevel.Error, scanner.LastReadRange,
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
                    this.Report(LogLevel.Error, scanner.LastReadRange,
                                Messages.Error_BeatInvalidFretNumberInStringsSpecifier);
                    result = null;
                    return false;
                }

                result.Fret = fret;
            }

            scanner.SkipWhitespaces();
            if (scanner.Expect(':'))
            {
                if (isTied)
                {
                    this.Report(LogLevel.Warning, scanner.LastReadRange,
                                Messages.Warning_EffectTechniqueInTiedNote);
                }

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
