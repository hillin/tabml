using System.Collections.Generic;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing
{
    internal static class Parser
    {

        public static bool TryReadInteger(Scanner scanner, out LiteralNode<int> node)
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

        public static bool TryReadChordName(Scanner scanner, ILogger logger, out LiteralNode<string> chordName)
        {
            var name = scanner.Read(@"[a-zA-Z0-9\*\$\#♯♭\-\+\?'\`\~\&\^\!]+");
            chordName = string.IsNullOrEmpty(name) ? null : new LiteralNode<string>(name, scanner.LastReadRange);
            return true;
        }

        public static bool TryReadBaseNoteValue(Scanner scanner, ILogger logger,
                                                out LiteralNode<BaseNoteValue> baseNoteValueNode)
        {
            int reciprocal;
            if (!scanner.TryReadInteger(out reciprocal))
            {
                logger.Report(LogLevel.Error, scanner.LastReadRange, Messages.Error_NoteValueExpected);
                baseNoteValueNode = null;
                return false;
            }

            BaseNoteValue baseNoteValue;
            if (!BaseNoteValues.TryParse(reciprocal, out baseNoteValue))
            {
                logger.Report(LogLevel.Error, scanner.LastReadRange, Messages.Error_InvalidReciprocalNoteValue);
                baseNoteValueNode = null;
                return false;
            }

            baseNoteValueNode = new LiteralNode<BaseNoteValue>(baseNoteValue, scanner.LastReadRange);
            return true;
        }

        public static bool TryReadNoteValueAugment(Scanner scanner, ILogger logger,
                                                   out LiteralNode<NoteValueAugment> augmentNode)
        {
            var anchor = scanner.MakeAnchor();
            var dots = 0;
            while (!scanner.EndOfLine)
            {
                if (!scanner.Expect('.'))
                    break;

                ++dots;
            }

            switch (dots)
            {
                case 0:
                    augmentNode = new LiteralNode<NoteValueAugment>(NoteValueAugment.None, anchor.Range);
                    return true;
                case 1:
                    augmentNode = new LiteralNode<NoteValueAugment>(NoteValueAugment.Dot, anchor.Range);
                    return true;
                case 2:
                    augmentNode = new LiteralNode<NoteValueAugment>(NoteValueAugment.TwoDots, anchor.Range);
                    return true;
                case 3:
                    augmentNode = new LiteralNode<NoteValueAugment>(NoteValueAugment.ThreeDots, anchor.Range);
                    return true;
                default:
                    logger.Report(LogLevel.Error, anchor.Range,
                                    Messages.Error_TooManyDotsInNoteValueAugment);
                    augmentNode = null;
                    return false;
            }
        }

        public static bool TryReadBaseNoteName(Scanner scanner, ILogger logger,
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

        public static bool TryReadAccidental(Scanner scanner, ILogger logger,
                                             out LiteralNode<Accidental> accidentalNode)
        {
            var accidentalText = scanner.ReadAny(@"\#\#", "bb", "♯♯", "♭♭", @"\#", "♯", "b", "♭", "\u1d12a", "\u1d12b");
            Accidental accidental;
            if (!Accidentals.TryParse(accidentalText, out accidental))
            {
                logger.Report(LogLevel.Error, scanner.LastReadRange, Messages.Error_InvalidAccidental);
                accidentalNode = null;
                return false;
            }

            accidentalNode = new LiteralNode<Accidental>(accidental, scanner.LastReadRange);
            return true;
        }

        public static bool TryReadAllStringStrumTechnique(Scanner scanner, ILogger logger,
                                                      out LiteralNode<AllStringStrumTechnique> technique)
        {
            switch (scanner.ReadAny("rasg", "ad", "au", @"\|", "x", "d", "↑", "u", "↓"))
            {
                case "|":
                case "x":
                    technique = new LiteralNode<AllStringStrumTechnique>(AllStringStrumTechnique.None, scanner.LastReadRange);
                    return true;
                case "d":
                case "↑":
                    technique = new LiteralNode<AllStringStrumTechnique>(AllStringStrumTechnique.BrushDown, scanner.LastReadRange);
                    return true;
                case "u":
                case "↓":
                    technique = new LiteralNode<AllStringStrumTechnique>(AllStringStrumTechnique.BrushUp, scanner.LastReadRange);
                    return true;
                case "ad":
                    technique = new LiteralNode<AllStringStrumTechnique>(AllStringStrumTechnique.ArpeggioDown, scanner.LastReadRange);
                    return true;
                case "au":
                    technique = new LiteralNode<AllStringStrumTechnique>(AllStringStrumTechnique.ArpeggioUp, scanner.LastReadRange);
                    return true;
                case "rasg":
                    technique = new LiteralNode<AllStringStrumTechnique>(AllStringStrumTechnique.Rasgueado, scanner.LastReadRange);
                    return true;
            }

            technique = null;
            return false;
        }


        public static bool TryReadStrumTechnique(Scanner scanner, ILogger logger, out LiteralNode<StrumTechnique> technique)
        {
            switch (scanner.ReadAny("rasg", "ad", "au", "pu", "pd", "d", "D", "↑", "u", "U", "↓"))
            {
                case "d":
                case "↑":
                    technique = new LiteralNode<StrumTechnique>(StrumTechnique.BrushDown, scanner.LastReadRange);
                    return true;
                case "u":
                case "↓":
                    technique = new LiteralNode<StrumTechnique>(StrumTechnique.BrushUp, scanner.LastReadRange);
                    return true;
                case "ad":
                    technique = new LiteralNode<StrumTechnique>(StrumTechnique.ArpeggioDown, scanner.LastReadRange);
                    return true;
                case "au":
                    technique = new LiteralNode<StrumTechnique>(StrumTechnique.ArpeggioUp, scanner.LastReadRange);
                    return true;
                case "rasg":
                    technique = new LiteralNode<StrumTechnique>(StrumTechnique.Rasgueado, scanner.LastReadRange);
                    return true;
                case "pu":
                    technique = new LiteralNode<StrumTechnique>(StrumTechnique.PickstrokeUp, scanner.LastReadRange);
                    return true;
                case "pd":
                    technique = new LiteralNode<StrumTechnique>(StrumTechnique.PickstrokeDown, scanner.LastReadRange);
                    return true;
            }

            technique = null;
            return false;
        }


        public static bool TryReadPreNoteConnection(Scanner scanner, ILogger logger,
                                                     out LiteralNode<PreNoteConnection> connection)
        {
            switch (scanner.ReadAny("~", @"\/", @"\\", @"\.\/", @"\`\\", "h", "p", "s"))

            {
                case @"~":
                    connection = new LiteralNode<PreNoteConnection>(PreNoteConnection.Tie, scanner.LastReadRange);
                    return true;
                case @"/":
                case @"\":
                case "s":
                    connection = new LiteralNode<PreNoteConnection>(PreNoteConnection.Slide, scanner.LastReadRange);
                    return true;
                case @"./":
                    connection = new LiteralNode<PreNoteConnection>(PreNoteConnection.SlideInFromLower, scanner.LastReadRange);
                    return true;
                case @"`\":
                    connection = new LiteralNode<PreNoteConnection>(PreNoteConnection.SlideInFromHigher, scanner.LastReadRange);
                    return true;
                case @"h":
                    connection = new LiteralNode<PreNoteConnection>(PreNoteConnection.Hammer, scanner.LastReadRange);
                    return true;
                case @"p":
                    connection = new LiteralNode<PreNoteConnection>(PreNoteConnection.Pull, scanner.LastReadRange);
                    return true;
            }

            connection = null;
            return false;
        }


        public static bool TryReadPostNoteConnection(Scanner scanner, ILogger logger,
                                                      out LiteralNode<PostNoteConnection> connection)
        {
            switch (scanner.ReadAny(@"\/\`", @"\\\.").ToLowerInvariant())
            {
                case @"/`":
                    connection = new LiteralNode<PostNoteConnection>(PostNoteConnection.SlideOutToHigher, scanner.LastReadRange);
                    return true;
                case @"\.":
                    connection = new LiteralNode<PostNoteConnection>(PostNoteConnection.SlideOutToLower, scanner.LastReadRange);
                    return true;
            }

            connection = null;
            return false;
        }

        public static bool TryReadBeatEffectTechnique(Scanner scanner, ILogger logger,
                                                       out LiteralNode<BeatEffectTechnique> technique, out LiteralNode<double> argument)
        {
            argument = null;

            switch (scanner.ReadAny("tremolo", "vibrato", "tr", "vib"))
            {
                case "tr":
                case "tremolo":
                    technique = new LiteralNode<BeatEffectTechnique>(BeatEffectTechnique.Tremolo, scanner.LastReadRange);
                    return true;
                case "vib":
                case "vibrato":
                    technique = new LiteralNode<BeatEffectTechnique>(BeatEffectTechnique.Vibrato, scanner.LastReadRange);
                    return true;
            }

            technique = null;
            return false;
        }


        public static bool TryReadNoteEffectTechnique(Scanner scanner, ILogger logger,
                                                       out LiteralNode<NoteEffectTechnique> technique, out LiteralNode<double> argument)
        {
            argument = null;

            switch (scanner.ReadAny("dead", "bend", "ah", "nh", "◆", "◇", "x", "b"))
            {
                case "dead":
                case "x":
                    technique = new LiteralNode<NoteEffectTechnique>(NoteEffectTechnique.DeadNote, scanner.LastReadRange);
                    return true;
                case "ah":
                case "◆":
                    technique = new LiteralNode<NoteEffectTechnique>(NoteEffectTechnique.ArtificialHarmonic, scanner.LastReadRange);
                    string argumentString;
                    switch (scanner.TryReadParenthesis(out argumentString, '<', '>', allowNesting: false))
                    {
                        case Scanner.ParenthesisReadResult.Success:
                            int ahArgument;
                            if (int.TryParse(argumentString, out ahArgument))
                            {
                                argument = new LiteralNode<double>(ahArgument, scanner.LastReadRange);
                                return true;
                            }

                            technique = null;
                            return false;
                        case Scanner.ParenthesisReadResult.MissingOpen:
                            return true;
                        case Scanner.ParenthesisReadResult.MissingClose:
                            logger.Report(LogLevel.Error, scanner.LastReadRange,
                                            Messages.Error_ArtificialHarmonicFretSpecifierNotEnclosed);
                            technique = null;
                            return false;
                    }

                    return true;
                case "nh":
                case "◇":
                    technique = new LiteralNode<NoteEffectTechnique>(NoteEffectTechnique.NaturalHarmonic, scanner.LastReadRange);
                    return true;
                case "bend":
                case "b":
                    // todo: bend args
                    technique = new LiteralNode<NoteEffectTechnique>(NoteEffectTechnique.Bend, scanner.LastReadRange);
                    return true;
            }

            technique = null;
            return false;
        }

        public static bool TryReadNoteDurationEffect(Scanner scanner, ILogger logger,
                                                      out LiteralNode<BeatDurationEffect> technique)
        {
            switch (scanner.ReadAny("fermata", "staccato"))
            {
                case "fermata":
                    technique = new LiteralNode<BeatDurationEffect>(BeatDurationEffect.Fermata, scanner.LastReadRange);
                    return true;
                case "staccato":
                    technique = new LiteralNode<BeatDurationEffect>(BeatDurationEffect.Staccato, scanner.LastReadRange);
                    return true;
            }

            technique = null;
            return false;
        }

        public static bool TryReadNoteAccent(Scanner scanner, ILogger logger, out LiteralNode<BeatAccent> accent)
        {
            switch (scanner.ReadAny("accented", "heavy", "ghost"))
            {
                case "accented":
                    accent = new LiteralNode<BeatAccent>(BeatAccent.Accented, scanner.LastReadRange);
                    return true;
                case "heavy":
                    accent = new LiteralNode<BeatAccent>(BeatAccent.HeavilyAccented, scanner.LastReadRange);
                    return true;
                case "ghost":
                    accent = new LiteralNode<BeatAccent>(BeatAccent.Ghost, scanner.LastReadRange);
                    return true;
            }

            accent = null;
            return false;
        }

        public static bool TryReadOpenBarLine(Scanner scanner, ILogger logger, out LiteralNode<OpenBarLine> barLine)
        {
            if (scanner.Expect("||:"))
            {
                barLine = new LiteralNode<OpenBarLine>(OpenBarLine.BeginRepeat, scanner.LastReadRange);
                return true;
            }

            if (scanner.Expect("||"))
            {
                barLine = new LiteralNode<OpenBarLine>(OpenBarLine.Double, scanner.LastReadRange);
                return true;
            }

            if (scanner.Expect('|'))
            {
                barLine = new LiteralNode<OpenBarLine>(OpenBarLine.Standard, scanner.LastReadRange);
                return true;
            }

            barLine = null;
            return false;
        }

        public static bool TryReadCloseBarLine(Scanner scanner, ILogger logger, out LiteralNode<CloseBarLine> barLine)
        {
            if (scanner.Expect(":||"))
            {
                barLine = new LiteralNode<CloseBarLine>(CloseBarLine.EndRepeat, scanner.LastReadRange);
                return true;
            }

            if (scanner.Expect("||"))
            {
                barLine = new LiteralNode<CloseBarLine>(CloseBarLine.Double, scanner.LastReadRange);
                return true;
            }

            if (scanner.Expect("|"))
            {
                barLine = new LiteralNode<CloseBarLine>(CloseBarLine.Standard, scanner.LastReadRange);
                return true;
            }

            barLine = null;
            return false;
        }

        public static bool TryReadStaffType(Scanner scanner, ILogger logger, out LiteralNode<StaffType> staffTypeNode)
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
