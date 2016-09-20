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

        public static bool TryParseInteger(Scanner scanner, out IntegerNode node)
        {
            int value;
            if (!scanner.TryReadInteger(out value))
            {
                node = null;
                return false;
            }

            node = new IntegerNode(value, scanner.LastReadRange);
            return true;
        }

        public static bool TryReadChordName(Scanner scanner, IParseReporter reporter, out string chordName)
        {
            chordName = scanner.Read(@"[a-zA-Z0-9\*\$\#♯♭\-\+\?'\`\~\&\^\!]+");
            return true;
        }

        public static bool TryReadChordFingering(Scanner scanner, IParseReporter reporter, out int[] fingering)
        {
            var definitionString = scanner.Read(@"[\dxX ,]+").Trim();
            if (string.IsNullOrEmpty(definitionString))
            {
                reporter.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_ChordCommandletMissingFingering);
                fingering = null;
                return false;
            }

            var containsComma = definitionString.Contains(',');
            var containsWhitespace = definitionString.Any(char.IsWhiteSpace);
            IEnumerable<string> fingeringTokens;

            if (containsComma)
            {
                if (containsWhitespace)
                {
                    reporter.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                    ParseMessages.Warning_BothChordFingeringDelimiterUsed);
                    definitionString = Regex.Replace(definitionString, @"\s+", ",");
                }
                fingeringTokens = definitionString.Split(',');
            }
            else if (containsWhitespace)
                fingeringTokens = Regex.Split(definitionString, @"\s+");
            else
                fingeringTokens = definitionString.Select(char.ToString).ToArray();

            fingering =
                fingeringTokens.Select(
                    f => f.Equals("x", StringComparison.InvariantCultureIgnoreCase)
                        ? ChordDefinition.FingeringSkipString
                        : int.Parse(f)).ToArray();

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

        public static bool TryReadStaffType(Scanner scanner, IParseReporter reporter, out StaffType staffType)
        {
            switch (scanner.ReadToLineEnd().Trim().ToLowerInvariant())
            {
                case "guitar":
                case "acoustic guitar":
                    staffType = StaffType.Guitar; return true;
                case "steel":
                case "steel guitar":
                    staffType = StaffType.SteelGuitar; return true;
                case "nylon":
                case "nylon guitar":
                case "classical":
                case "classical guitar":
                    staffType = StaffType.NylonGuitar; return true;
                case "electric guitar":
                    staffType = StaffType.ElectricGuitar; return true;
                case "bass":
                    staffType = StaffType.Bass; return true;
                case "acoustic bass":
                    staffType = StaffType.AcousticBass; return true;
                case "electric bass":
                    staffType = StaffType.ElectricBass; return true;
                case "ukulele":
                case "uku":
                    staffType = StaffType.Ukulele; return true;
                case "mandolin":
                    staffType = StaffType.Mandolin; return true;
                case "vocal":
                    staffType = StaffType.Vocal; return true;
            }

            staffType = StaffType.Guitar;
            return false;
        }
    }
}
