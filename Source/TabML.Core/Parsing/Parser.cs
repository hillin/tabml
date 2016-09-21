using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing
{
    internal static class Parser
    {

        public static bool TryParseInteger(Scanner scanner, out LiteralNode<int> node)
        {
            int value;
            if (!scanner.TryReadInteger(out value))
            {
                node = null;
                return false;
            }

            node = new LiteralNode<int>(value, scanner.LastReadRange);
            return true;
        }

        public static bool TryReadChordName(Scanner scanner, IParseReporter reporter, out LiteralNode<string> chordName)
        {
            var name = scanner.Read(@"[a-zA-Z0-9\*\$\#♯♭\-\+\?'\`\~\&\^\!]+");
            chordName = new LiteralNode<string>(name, scanner.LastReadRange);
            return true;
        }

        public static bool TryReadBaseNoteName(Scanner scanner, IParseReporter reporter,
                                               out LiteralNode<BaseNoteName> baseNoteNameNode)
        {
            var noteNameChar = scanner.Read();
            BaseNoteName baseNoteName;
            if (!BaseNoteNames.TryParse(noteNameChar, out baseNoteName))
            {
                baseNoteNameNode = null;
                return false;
            }

            baseNoteNameNode = new LiteralNode<BaseNoteName>(baseNoteName, scanner.LastReadRange);
            return true;
        }

        public static bool TryReadAccidental(Scanner scanner, IParseReporter reporter,
                                             out LiteralNode<Accidental> accidentalNode)
        {
            var accidentalText = scanner.Read(@"[\#|\#\#|b|bb|♯|♯♯|♭|♭♭|\u1d12a|\u1d12b]*");
            Accidental accidental;
            if (!Accidentals.TryParse(accidentalText, out accidental))
            {
                reporter.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_InvalidAccidental);
                accidentalNode = null;
                return false;
            }

            accidentalNode = new LiteralNode<Accidental>(accidental, scanner.LastReadRange);
            return true;
        }

        public static bool TryParseNoteName(Scanner scanner, IParseReporter reporter, out NoteName noteName)
        {
            var noteNameChar = scanner.Read();
            BaseNoteName baseNoteName;
            if (!BaseNoteNames.TryParse(noteNameChar, out baseNoteName))
            {
                noteName = default(NoteName);
                return false;
            }

            var accidentalText = scanner.Read("[#b♯♭\u1d12a\u1d12b]*");
            Accidental accidental;
            Accidentals.TryParse(accidentalText, out accidental);

            noteName = new NoteName(baseNoteName, accidental);
            return true;
        }


        public static bool TryParseHeadStrumTechnique(Scanner scanner, IParseReporter reporter,
                                                      out StrumTechnique technique)
        {
            switch (scanner.Read(@"\||x|d|↑|u|↓|ad|au|rasg|r").ToLowerInvariant())
            {
                case "|":
                case "x":
                    technique = StrumTechnique.None;
                    return true;
                case "d":
                case "↑":
                    technique = StrumTechnique.BrushDown;
                    return true;
                case "u":
                case "↓":
                    technique = StrumTechnique.BrushUp;
                    return true;
                case "ad":
                    technique = StrumTechnique.ArpeggioDown;
                    return true;
                case "au":
                    technique = StrumTechnique.ArpeggioUp;
                    return true;
                case "rasg":
                case "r":
                    technique = StrumTechnique.Rasgueado;
                    return true;
            }

            technique = StrumTechnique.None;
            return false;
        }

        public static bool TryParseStrumTechnique(Scanner scanner, IParseReporter reporter, out StrumTechnique technique)
        {
            switch (scanner.Read(@"d|↑|u|↓|ad|au|rasg|r|pu|pd").ToLowerInvariant())
            {
                case "d":
                case "↑":
                    technique = StrumTechnique.BrushDown;
                    return true;
                case "u":
                case "↓":
                    technique = StrumTechnique.BrushUp;
                    return true;
                case "ad":
                    technique = StrumTechnique.ArpeggioDown;
                    return true;
                case "au":
                    technique = StrumTechnique.ArpeggioUp;
                    return true;
                case "rasg":
                case "r":
                    technique = StrumTechnique.Rasgueado;
                    return true;
                case "pu":
                    technique = StrumTechnique.PickstrokeUp;
                    return true;
                case "pd":
                    technique = StrumTechnique.PickstrokeDown;
                    return true;
            }

            technique = StrumTechnique.None;
            return false;
        }


        public static bool TryParsePreNoteConnection(Scanner scanner, IParseReporter reporter,
                                                     out PreNoteConnection connection)
        {
            switch (scanner.Read(@"~|\/|\\|\.\/|\`\\|h|p").ToLowerInvariant())
            {
                case "~":
                    connection = PreNoteConnection.Tie;
                    return true;
                case "/":
                case "\\":
                    connection = PreNoteConnection.Slide;
                    return true;
                case "./":
                case "`\\":
                    connection = PreNoteConnection.SlideIn;
                    return true;
                case "h":
                    connection = PreNoteConnection.Hammer;
                    return true;
                case "p":
                    connection = PreNoteConnection.Pull;
                    return true;
            }

            connection = PreNoteConnection.None;
            return false;
        }


        public static bool TryParsePostNoteConnection(Scanner scanner, IParseReporter reporter,
                                                      out PostNoteConnection connection)
        {
            switch (scanner.Read(@"\/\`|\\\.").ToLowerInvariant())
            {
                case "/`":
                case "\\.":
                    connection = PostNoteConnection.SlideOut;
                    return true;
            }

            connection = PostNoteConnection.None;
            return false;
        }

        public static bool TryParseNoteEffectTechnique(Scanner scanner, IParseReporter reporter,
                                                       out NoteEffectTechnique technique, out double? argument)
        {
            argument = null;

            switch (scanner.Read(@"dead|x|ah|◆|nh|◇|b|bend|tr|tremolo|vib|vibrato").ToLowerInvariant())
            {
                case "x":
                case "dead":
                    technique = NoteEffectTechnique.DeadNote;
                    return true;
                case "ah":
                case "◆":
                    technique = NoteEffectTechnique.ArtificialHarmonic;
                    string argumentString;
                    switch (scanner.TryReadParenthesis(out argumentString, '<', '>', allowNesting: false))
                    {
                        case Scanner.ParenthesisReadResult.Success:
                            int ahArgument;
                            if (int.TryParse(argumentString, out ahArgument))
                            {
                                argument = ahArgument;
                                return true;
                            }

                            technique = NoteEffectTechnique.None;
                            return false;
                        case Scanner.ParenthesisReadResult.MissingOpen:
                            return true;
                        case Scanner.ParenthesisReadResult.MissingClose:
                            reporter.Report(ParserReportLevel.Error, scanner.LastReadRange,
                                            ParseMessages.Error_ArtificialHarmonicFretSpecifierNotEnclosed);
                            technique = NoteEffectTechnique.None;
                            return false;
                    }

                    return true;
                case "nh":
                case "◇":
                    technique = NoteEffectTechnique.NaturalHarmonic;
                    return true;
                case "b":
                case "bend":
                    // todo: bend args
                    technique = NoteEffectTechnique.Bend;
                    return true;
                case "tr":
                case "tremolo":
                    technique = NoteEffectTechnique.Tremolo;
                    return true;
                case "vib":
                case "vibrato":
                    technique = NoteEffectTechnique.Vibrato;
                    return true;
            }

            technique = NoteEffectTechnique.None;
            return false;
        }

        public static bool TryParseNoteDurationEffect(Scanner scanner, IParseReporter reporter,
                                                      out NoteDurationEffect technique)
        {
            switch (scanner.Read(@"fermata|staccato").ToLowerInvariant())
            {
                case "fermata":
                    technique = NoteDurationEffect.Fermata;
                    return true;
                case "staccato":
                    technique = NoteDurationEffect.Staccato;
                    return true;
            }

            technique = NoteDurationEffect.None;
            return false;
        }

        public static bool TryParseNoteAccent(Scanner scanner, IParseReporter reporter, out NoteAccent accent)
        {
            switch (scanner.Read(@"a|accented|h|heavy|g|ghost").ToLowerInvariant())
            {
                case "a":
                case "accented":
                    accent = NoteAccent.Accented;
                    return true;
                case "ha":
                case "heavy":
                    accent = NoteAccent.HeavilyAccented;
                    return true;
                case "g":
                case "ghost":
                    accent = NoteAccent.Ghost;
                    return true;
            }

            accent = NoteAccent.Normal;
            return false;
        }

        public static bool TryReadOpenBarLine(Scanner scanner, IParseReporter reporter, out OpenBarLine barLine)
        {
            if (!scanner.Expect('|'))
            {
                barLine = OpenBarLine.Standard;
                return false;
            }

            // |
            if (!scanner.Expect('|'))
            {
                barLine = OpenBarLine.Standard;
                return true;
            }

            // ||
            if (!scanner.Expect(':'))
            {
                barLine = OpenBarLine.Double;
                return true;
            }

            barLine = OpenBarLine.BeginRepeat;
            return true;
        }

        public static bool TryReadCloseBarLine(Scanner scanner, IParseReporter reporter, out CloseBarLine barLine)
        {
            if (scanner.Expect(":||"))
            {
                barLine = CloseBarLine.EndRepeat;
                return true;
            }

            if (scanner.Expect("||"))
            {
                barLine = CloseBarLine.Double;
                return true;
            }

            if (scanner.Expect("|"))
            {
                barLine = CloseBarLine.Standard;
                return true;
            }

            barLine = CloseBarLine.Standard;
            return false;
        }

        public static bool TryReadStaffType(Scanner scanner, IParseReporter reporter, out LiteralNode<StaffType> staffTypeNode)
        {
            StaffType staffType;
            switch (scanner.ReadToLineEnd().Trim().ToLowerInvariant())
            {
                case "guitar":
                case "acoustic guitar":
                    staffType = StaffType.Guitar; break;
                case "steel":
                case "steel guitar":
                    staffType = StaffType.SteelGuitar; break;
                case "nylon":
                case "nylon guitar":
                case "classical":
                case "classical guitar":
                    staffType = StaffType.NylonGuitar; break;
                case "electric guitar":
                    staffType = StaffType.ElectricGuitar; break;
                case "bass":
                    staffType = StaffType.Bass; break;
                case "acoustic bass":
                    staffType = StaffType.AcousticBass; break;
                case "electric bass":
                    staffType = StaffType.ElectricBass; break;
                case "ukulele":
                case "uku":
                    staffType = StaffType.Ukulele; break;
                case "mandolin":
                    staffType = StaffType.Mandolin; break;
                case "vocal":
                    staffType = StaffType.Vocal; break;
                default:
                    staffTypeNode = null;
                    return false;
            }

            staffTypeNode = new LiteralNode<StaffType>(staffType, scanner.LastReadRange);
            return true;
        }
    }
}
