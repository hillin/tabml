using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Core.Parsing.AST;
// ReSharper disable InconsistentNaming

namespace TabML.Core.Parsing
{
    class RhythmUnitParser : ParserBase<RhythmUnitNode>
    {
        private enum ModifierIndex
        {
            StrumTechnique = 0,
            NoteEffectTechnique = 1,
            NoteEffectTechniqueParameter = 2,
            NoteDurationEffect = 3,
            NoteAccent = 4,
            Max = 5
        }

        public override bool TryParse(Scanner scanner, out RhythmUnitNode result)
        {
            int noteValue;
            var noteValueIndetemined = !scanner.TryReadInteger(out noteValue);

            RhythmUnitNote[] stringsSpecifier;
            if (!this.TryReadNotes(scanner, out stringsSpecifier))
            {
                result = null;
                return false;
            }

            var modifiers = new object[(int)ModifierIndex.Max];

            if (noteValueIndetemined && stringsSpecifier == null)
            {
                StrumTechnique strumTechnique;
                if (!Parser.TryParseHeadStrumTechnique(scanner, this, out strumTechnique))
                {
                    this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                                ParseMessages.Error_RhythmUnitBodyExpected);
                    result = null;
                    return false;
                }

                modifiers[(int)ModifierIndex.StrumTechnique] = strumTechnique;
            }

            if (scanner.Expect(':'))
            {
                do
                {
                    scanner.SkipWhitespaces();
                    if (this.TryReadModifier(scanner, modifiers))
                        continue;

                    result = null;
                    return false;
                } while (scanner.Expect(','));
            }

            var unit = new RhythmUnit
            {
                Notes = stringsSpecifier,
                StrumTechnique = (StrumTechnique?)modifiers[(int)ModifierIndex.StrumTechnique] ?? StrumTechnique.None,
                DurationEffect = (NoteDurationEffect?)modifiers[(int)ModifierIndex.NoteDurationEffect] ?? NoteDurationEffect.None,
                Accent = (NoteAccent?)modifiers[(int)ModifierIndex.NoteAccent] ?? NoteAccent.Normal,
                EffectTechnique = (NoteEffectTechnique?)modifiers[(int)ModifierIndex.NoteEffectTechnique] ?? NoteEffectTechnique.None,
                EffectTechniqueParameter = (double)modifiers[(int)ModifierIndex.NoteEffectTechnique],
            };
            result = new RhythmUnitNode(unit, !noteValueIndetemined);
            return true;
        }

        private bool TryReadModifier(Scanner scanner, object[] modifiers)
        {
            StrumTechnique strumTechnique;
            if (Parser.TryParseStrumTechnique(scanner, this, out strumTechnique))
            {
                if (modifiers[(int)ModifierIndex.StrumTechnique] != null)
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Warning_RhythmUnitStrumTechniqueAlreadySpecified);
                else
                    modifiers[(int)ModifierIndex.StrumTechnique] = strumTechnique;
                return true;
            }

            NoteAccent accent;
            if (Parser.TryParseNoteAccent(scanner, this, out accent))
            {
                if (modifiers[(int)ModifierIndex.NoteAccent] != null)
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Warning_RhythmUnitAccentAlreadySpecified);
                else
                    modifiers[(int)ModifierIndex.NoteAccent] = accent;
                return true;
            }

            NoteEffectTechnique noteEffectTechnique;
            double? techniqueParameter;
            if (Parser.TryParseNoteEffectTechnique(scanner, this, out noteEffectTechnique, out techniqueParameter))
            {
                if (modifiers[(int)ModifierIndex.NoteEffectTechnique] != null)
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Warning_RhythmUnitNoteEffectTechniqueAlreadySpecified);
                else
                {
                    modifiers[(int)ModifierIndex.NoteEffectTechnique] = noteEffectTechnique;
                    modifiers[(int)ModifierIndex.NoteEffectTechniqueParameter] = techniqueParameter ?? default(double);
                }

                return true;
            }

            NoteDurationEffect durationEffect;
            if (Parser.TryParseNoteDurationEffect(scanner, this, out durationEffect))
            {
                if (modifiers[(int)ModifierIndex.NoteDurationEffect] != null)
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Warning_RhythmUnitNoteDurationEffectAlreadySpecified);
                else
                    modifiers[(int)ModifierIndex.NoteDurationEffect] = durationEffect;
                return true;
            }

            this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                        ParseMessages.Error_RhythmUnitModifierExpected);
            return false;
        }


        private bool TryReadNotes(Scanner baseScanner, out RhythmUnitNote[] notesSpecifier)
        {
            string notesSpecifierString;
            switch (baseScanner.TryReadParenthesis(out notesSpecifierString, allowNesting: false))
            {
                case Scanner.ParenthesisReadResult.Success:
                    var notesSpecifierList = new List<RhythmUnitNote>();
                    var scanner = new Scanner(notesSpecifierString);
                    while (scanner.EndOfInput)
                    {
                        scanner.SkipWhitespaces();

                        PreNoteConnection preConnection;
                        Parser.TryParsePreNoteConnection(scanner, this, out preConnection);

                        int stringNumber;
                        if (!scanner.TryReadInteger(out stringNumber))
                        {
                            this.Report(ParserReportLevel.Error,
                                        scanner.LastReadRange.Offset(baseScanner.LastReadRange.From),
                                        ParseMessages.Error_RhythmUnitInvalidStringNumberInStringsSpecifier);
                            notesSpecifier = null;
                            return false;
                        }

                        var fret = RhythmUnitNote.UnspecifiedFret;
                        if (scanner.Expect('='))
                        {
                            if (!scanner.TryReadInteger(out fret))
                            {
                                this.Report(ParserReportLevel.Error,
                                            scanner.LastReadRange.Offset(baseScanner.LastReadRange.From),
                                            ParseMessages.Error_RhythmUnitInvalidFretNumberInStringsSpecifier);
                                notesSpecifier = null;
                                return false;
                            }
                        }

                        PostNoteConnection postConnection;
                        Parser.TryParsePostNoteConnection(scanner, this, out postConnection);

                        notesSpecifierList.Add(new RhythmUnitNote(stringNumber, fret, preConnection, postConnection));
                    }

                    notesSpecifier = notesSpecifierList.ToArray();
                    return true;
                case Scanner.ParenthesisReadResult.MissingOpen:
                    notesSpecifier = null;
                    return true;
                case Scanner.ParenthesisReadResult.MissingClose:
                    this.Report(ParserReportLevel.Error, baseScanner.LastReadRange,
                                ParseMessages.Error_RhythmCommandletMissingCloseParenthesisInStringsSpecifier);
                    notesSpecifier = null;
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
