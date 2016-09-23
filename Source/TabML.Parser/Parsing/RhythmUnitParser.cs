using System.Collections.Generic;
using TabML.Core.MusicTheory;
using TabML.Parser.AST;

// ReSharper disable InconsistentNaming

namespace TabML.Parser.Parsing
{
    class RhythmUnitParser : ParserBase<RhythmUnitNode>
    {

        public override bool TryParse(Scanner scanner, out RhythmUnitNode result)
        {
            var anchor = scanner.MakeAnchor();
            result = new RhythmUnitNode();

            NoteValueNode noteValue;
            var noteValueParser = new NoteValueParser();
            noteValueParser.TryParse(scanner, out noteValue);
            result.NoteValue = noteValue;
            var noteValueIndetemined = noteValue == null && !noteValueParser.HasError;

            scanner.SkipWhitespaces();
            if (!this.TryReadNotes(scanner, result.Notes))
            {
                result = null;
                return false;
            }

            LiteralNode<StrumTechnique> strumTechnique;
            Parser.TryReadHeadStrumTechnique(scanner, this, out strumTechnique);

            if (noteValueIndetemined && result.Notes.Count == 0 && strumTechnique == null)
            {

                    this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                                ParseMessages.Error_RhythmUnitBodyExpected);
                    result = null;
                    return false;
            }

            result.StrumTechnique = strumTechnique;

            scanner.SkipWhitespaces();

            if (scanner.Expect(':'))
            {
                do
                {
                    scanner.SkipWhitespaces();
                    if (this.TryReadModifier(scanner, result))
                        continue;

                    result = null;
                    return false;
                } while (scanner.Expect(','));
            }

            result.Range = anchor.Range;
            return true;
        }

        private bool TryReadModifier(Scanner scanner, RhythmUnitNode result)
        {
            LiteralNode<StrumTechnique> strumTechnique;
            if (Parser.TryReadStrumTechnique(scanner, this, out strumTechnique))
            {
                if (result.StrumTechnique != null)
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Warning_RhythmUnitStrumTechniqueAlreadySpecified);
                else
                    result.StrumTechnique = strumTechnique;
                return true;
            }

            LiteralNode<NoteAccent> accent;
            if (Parser.TryReadNoteAccent(scanner, this, out accent))
            {
                if (result.Accent != null)
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Warning_RhythmUnitAccentAlreadySpecified);
                else
                    result.Accent = accent;
                return true;
            }

            LiteralNode<NoteEffectTechnique> noteEffectTechnique;
            LiteralNode<double> techniqueParameter;
            if (Parser.TryReadNoteEffectTechnique(scanner, this, out noteEffectTechnique, out techniqueParameter))
            {
                if (result.EffectTechnique != null)
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Warning_RhythmUnitNoteEffectTechniqueAlreadySpecified);
                else
                {
                    result.EffectTechnique = noteEffectTechnique;
                    result.EffectTechniqueParameter = techniqueParameter;
                }

                return true;
            }

            LiteralNode<NoteDurationEffect> durationEffect;
            if (Parser.TryReadNoteDurationEffect(scanner, this, out durationEffect))
            {
                if (result.DurationEffect != null)
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Warning_RhythmUnitNoteDurationEffectAlreadySpecified);
                else
                    result.DurationEffect = durationEffect;
                return true;
            }

            this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                        ParseMessages.Error_RhythmUnitModifierExpected);
            return false;
        }


        private bool TryReadNotes(Scanner scanner, ICollection<RhythmUnitNoteNode> notes)
        {
            if (!scanner.Expect('('))
                return true;

            scanner.SkipWhitespaces();

            var parenthesisClosed = false;
            while (!scanner.EndOfLine)
            {
                RhythmUnitNoteNode note;
                if (!new RhythmUnitNoteParser().TryParse(scanner, out note))
                {
                    return false;
                }

                notes.Add(note);

                if (!scanner.SkipOptional(',', true))
                {
                    if (scanner.Expect(')'))
                    {
                        parenthesisClosed = true;
                        break;
                    }
                }
            }

            if (!parenthesisClosed)
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                            ParseMessages.Error_RhythmCommandletMissingCloseParenthesisInStringsSpecifier);
                return false;
            }

            return true;
        }
    }
}
