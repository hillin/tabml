using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using TabML.Core.String;
using TabML.Core.Style;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Bar
{
    class BeatNoteParser : ParserBase<BeatNoteNode>
    {
        public override bool TryParse(Scanner scanner, out BeatNoteNode result)
        {
            var anchor = scanner.MakeAnchor();
            result = new BeatNoteNode();

            // read tie
            ExistencyNode tieNode;
            LiteralNode<VerticalDirection> tiePosition;
            Parser.TryReadTie(scanner, this, out tieNode, out tiePosition);
            result.Tie = tieNode;
            result.TiePosition = tiePosition;

            var isTied = tieNode != null;

            // read pre-connection
            LiteralNode<PreNoteConnection> preConnection;
            Parser.TryReadPreNoteConnection(scanner, this, out preConnection);
            result.PreConnection = preConnection;

            if (isTied && result.PreConnection != null)
            {
                this.Report(LogLevel.Warning, scanner.LastReadRange,
                            Messages.Warning_PreConnectionInTiedNote);
            }

            // read ghost note
            var ghostNoteAnchor = scanner.MakeAnchor();
            var ghostNoteOpened = scanner.Expect('(');

            // read string number
            LiteralNode<int> stringNumber;
            if (!Parser.TryReadInteger(scanner, out stringNumber))
            {
                this.Report(LogLevel.Error, scanner.LastReadRange,
                            Messages.Error_InvalidStringNumberInStringsSpecifier);
                result = null;
                return false;
            }

            result.String = stringNumber;

            // start reading fret
            if (scanner.Expect('='))
            {
                // read alternative ghost note
                if (!ghostNoteOpened && scanner.Expect('('))
                {
                    ghostNoteOpened = true;
                    ghostNoteAnchor = scanner.MakeAnchor();
                }

                // read natural harmonic
                var naturalHarmonicAnchor = scanner.MakeAnchor();
                var naturalHarmonicOpened = scanner.Expect('<');

                // read fret number
                LiteralNode<int> fret;
                if (!Parser.TryReadInteger(scanner, out fret))
                {
                    this.Report(LogLevel.Error, scanner.LastReadRange,
                                Messages.Error_InvalidFretNumberInStringsSpecifier);
                    result = null;
                    return false;
                }

                result.Fret = fret;

                // enclose natural harmonic
                if (naturalHarmonicOpened)
                {
                    if (!scanner.Expect('>'))
                    {
                        this.Report(LogLevel.Error, scanner.LastReadRange,
                                    Messages.Error_NaturalHarmonicNoteNotEnclosed);
                        result = null;
                        return false;
                    }

                    result.EffectTechnique = new LiteralNode<NoteEffectTechnique>(NoteEffectTechnique.NaturalHarmonic, naturalHarmonicAnchor.Range);
                }

                // read artificial harmonic
                var artificialHarmonicAnchor = scanner.MakeAnchor();
                if (scanner.Expect('<'))
                {
                    // read artificial harmonic fret number
                    LiteralNode<int> artificialHarmonicFret;
                    Parser.TryReadInteger(scanner, out artificialHarmonicFret); // empty fret number is allowed (defaulted to 12th higher)

                    if (artificialHarmonicFret?.Value < fret.Value)
                    {
                        this.Report(LogLevel.Warning, naturalHarmonicAnchor.Range,
                                    Messages.Warning_ArtificialHarmonicFretTooSmall);
                    }

                    // enclose artificial harmonic
                    if (!scanner.Expect('>'))
                    {
                        this.Report(LogLevel.Error, scanner.LastReadRange,
                                    Messages.Error_ArtificialHarmonicFretSpecifierNotEnclosed);
                        result = null;
                        return false;
                    }

                    if (naturalHarmonicOpened)
                    {
                        this.Report(LogLevel.Warning, naturalHarmonicAnchor.Range,
                                    Messages.Warning_BothNaturalAndArtificialHarmonicDeclared);
                    }

                    result.EffectTechnique = new LiteralNode<NoteEffectTechnique>(
                        NoteEffectTechnique.ArtificialHarmonic, artificialHarmonicAnchor.Range);
                    result.EffectTechniqueParameter = artificialHarmonicFret == null ? null : new LiteralNode<double>(artificialHarmonicFret.Value, artificialHarmonicFret.Range);
                }
            }

            // enclose ghost note
            if (ghostNoteOpened)
            {
                if (!scanner.Expect(')'))
                {
                    this.Report(LogLevel.Error, scanner.LastReadRange,
                                Messages.Error_GhostNoteNotEnclosed);
                    result = null;
                    return false;
                }
                result.Accent = new LiteralNode<NoteAccent>(NoteAccent.Ghost, ghostNoteAnchor.Range);
            }

            // read effect techniques
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

            // read post-connection
            scanner.SkipWhitespaces();
            LiteralNode<PostNoteConnection> postConnection;
            Parser.TryReadPostNoteConnection(scanner, this, out postConnection);
            result.PostConnection = postConnection;


            result.Range = anchor.Range;
            return true;
        }
    }
}
