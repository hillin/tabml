using System.Collections.Generic;
using System.Linq;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using TabML.Core.String;
using TabML.Core.String.Plucked;
using TabML.Core.Style;
using TabML.Parser.AST;

// ReSharper disable InconsistentNaming

namespace TabML.Parser.Parsing.Bar
{
    class BeatParser : ParserBase<BeatNode>
    {

        public override bool TryParse(Scanner scanner, out BeatNode result)
        {
            var anchor = scanner.MakeAnchor();
            result = new BeatNode();

            ExistencyNode tieNode;
            LiteralNode<VerticalDirection> tiePosition;

            Parser.TryReadTie(scanner, this, out tieNode, out tiePosition);
            result.Tie = tieNode;
            result.TiePosition = tiePosition;

            var isTied = tieNode != null;

            LiteralNode<PreBeatConnection> preConnection;
            
            Parser.TryReadPreBeatConnection(scanner, this, out preConnection);
            result.PreConnection = preConnection;
            
            NoteValueNode noteValue;
            if (!new NoteValueParser().TryParse(scanner, out noteValue))
            {
                result = null;
                return false;
            }

            result.NoteValue = noteValue;

            scanner.SkipWhitespaces();

            var postNoteValueAnchor = scanner.MakeAnchor();

            ExistencyNode restNode;
            if (new CharExistencyParser('r').TryParse(scanner, out restNode))
            {
                result.Rest = restNode;
            }

            scanner.SkipWhitespaces();

            var postRestAnchor = scanner.MakeAnchor();

            if (!this.TryReadNotes(scanner, result.Notes))
            {
                result = null;
                return false;
            }

            var noteValueIndetemined = noteValue == null && !new NoteValueParser().HasError;

            // certain strum techniques (head strum techniques) can be placed before
            // the colon token
            LiteralNode<ChordStrumTechnique> strumTechnique;
            Parser.TryReadAllStringStrumTechnique(scanner, this, out strumTechnique);

            if (noteValueIndetemined && result.Notes.Count == 0 && strumTechnique == null)
            {
                this.Report(LogLevel.Error, scanner.LastReadRange,
                            Messages.Error_BeatBodyExpected);
                result = null;
                return false;
            }

            result.ChordStrumTechnique = strumTechnique;

            scanner.SkipWhitespaces();

            if (scanner.Expect(':'))
            {
                scanner.SkipWhitespaces();
                do
                {
                    if (!this.TryReadModifier(scanner, result))
                    {
                        result = null;
                        return false;
                    }

                    scanner.SkipWhitespaces();
                } while (scanner.Expect(','));
            }

            // all notes are tied, which is equal to the beat being tied
            if (!isTied && result.Notes.All(n => n.Tie != null))
            {
                isTied = true;
            }

            // post-connection is allowed for tied beat, so we check it here
            if (isTied && result.HasRedunantSpecifierForTied)
            {
                this.Report(LogLevel.Hint, postNoteValueAnchor.Range,
                            Messages.Hint_RedundantModifiersInTiedBeat);
            }

            scanner.SkipWhitespaces();
            LiteralNode<PostBeatConnection> postConnection;
            Parser.TryReadPostBeatConnection(scanner, this, out postConnection);
            result.PostConnection = postConnection;

            // post-connection is not allowed for rest beat
            if (restNode != null && result.HasRedunantSpecifierForRest)
            {
                this.Report(LogLevel.Warning, postRestAnchor.Range,
                            Messages.Warning_RedundantModifiersInRestBeat);
            }

            result.Range = anchor.Range;
            return true;
        }

        private bool TryReadModifier(Scanner scanner, BeatNode result)
        {
            LiteralNode<BeatAccent> accent;
            if (Parser.TryReadNoteAccent(scanner, this, out accent))
            {
                if (result.Accent != null)
                    this.Report(LogLevel.Warning, scanner.LastReadRange,
                                Messages.Warning_BeatAccentAlreadySpecified);
                else
                {
                    result.Accent = accent;
                    result.Modifiers.Add(accent);
                }
                return true;
            }

            LiteralNode<Ornament> ornament;
            LiteralNode<double> ornamentParameter;
            if (Parser.TryReadOrnament(scanner, this, out ornament, out ornamentParameter))
            {
                if (result.Ornament != null)
                    this.Report(LogLevel.Warning, scanner.LastReadRange,
                                Messages.Warning_OrnamentAlreadySpecified);
                else
                {
                    result.Ornament = ornament;
                    result.OrnamentParameter = ornamentParameter;
                    result.Modifiers.Add(ornament);
                    if (ornamentParameter != null)
                        result.Modifiers.Add(ornamentParameter);
                }

                return true;
            }

            LiteralNode<NoteRepetition> noteRepetition;
            if (Parser.TryReadNoteRepetition(scanner, this, out noteRepetition))
            {
                if (result.NoteRepetition != null)
                    this.Report(LogLevel.Warning, scanner.LastReadRange,
                                Messages.Warning_NoteRepetitionAlreadySpecified);
                else
                {
                    result.NoteRepetition = noteRepetition;
                    result.Modifiers.Add(noteRepetition);
                }

                return true;
            }

            LiteralNode<HoldAndPause> holdAndPause;
            if (Parser.TryReadNoteHoldAndPause(scanner, this, out holdAndPause))
            {
                if (result.HoldAndPause != null)
                    this.Report(LogLevel.Warning, scanner.LastReadRange,
                                Messages.Warning_BeatNoteHoldAndPauseEffectAlreadySpecified);
                else
                {
                    result.HoldAndPause = holdAndPause;
                    result.Modifiers.Add(holdAndPause);
                }
                return true;
            }

            LiteralNode<StrumTechnique> strumTechnique;
            if (Parser.TryReadStrumTechnique(scanner, this, out strumTechnique))
            {
                if (result.StrumTechnique != null || result.ChordStrumTechnique != null)
                    this.Report(LogLevel.Warning, scanner.LastReadRange,
                                Messages.Warning_BeatStrumTechniqueAlreadySpecified);
                else
                {
                    result.StrumTechnique = strumTechnique;
                    result.Modifiers.Add(strumTechnique);
                }
                return true;
            }

            this.Report(LogLevel.Error, scanner.LastReadRange,
                        Messages.Error_BeatModifierExpected);
            return false;
        }


        private bool TryReadNotes(Scanner scanner, ICollection<BeatNoteNode> notes)
        {
            if (!scanner.Expect('('))
                return true;

            scanner.SkipWhitespaces();

            var parenthesisClosed = false;
            while (!scanner.EndOfLine)
            {
                BeatNoteNode note;
                if (!new BeatNoteParser().TryParse(scanner, out note))
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
                this.Report(LogLevel.Error, scanner.LastReadRange,
                            Messages.Error_RhythmInstructionMissingCloseParenthesisInStringsSpecifier);
                return false;
            }

            return true;
        }
    }
}
