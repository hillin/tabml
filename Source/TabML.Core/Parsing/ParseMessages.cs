using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming

namespace TabML.Core.Parsing
{
    internal static class ParseMessages
    {
        public const string Suggestion_TuningNotSpecified = "Redundant empty tuning specifier (standard tuning used)";
        public const string Error_InvalidTuning = "Unrecognizable tuning specifier, standard tuning assumed";

        public const string Hint_RedundantTuningSpecifier =
            "\"{0}\" is a well-known tuning, so you don't have to explicitly define it";

        public const string Error_InvalidStringCount =
            "Unrecognizable string count, 6 strings assumed";

        public const string Error_StringCountOutOfRange =
            "Unsupported string count, should be between {0} and {1} strings. 6 strings assumed";

        public const string Error_MissingChordName = "Please specify a chord name";

        public const string Error_MissingChordDisplayNameNotEnclosed =
            "Chord display name specifier is not enclosed with '>'";

        public const string Error_MissingChordFingering = "Missing chord fingering";

        public const string Warning_BothChordFingeringDelimiterUsed =
            "Use either comma or whitespace to separate chord fingering numbers, don't use both";

        public const string Error_InvalidTimeSignature =
            "Unrecognizable time signature, please use a time signature like 4/4. 4/4 assumed.";

        public const string Error_UnsupportedBeatsInTimeSignature =
            "Time signature with more than 32 beats per bar is not supported. 4/4 assumed.";

        public const string Error_UnsupportedNoteValueInTimeSignature =
            "Time signature with a note value shorter than 1/32 is not supported. 4/4 assumed.";

        public const string Error_IrrationalNoteValueInTimeSignatureNotSupported =
            "Time signature with an irrational note value is not supported. 4/4 assumed.";

        public const string Error_InvalidKeySignature = "Unrecognizable key signature, key signature ignored";

        public const string Error_InvalidTempoSignature =
            "Unrecognizable tempo signature, please use a tempo signature like 4=72. Tempo signature ignored";

        public const string Error_IrrationalNoteValueInTempoSignatureNotSupported =
            "Tempo signature with an irrational note value is not supported, tempo signature ignored";

        public const string Error_TempoSignatureSpeedTooLow = "Tempo speed is too low";

        public const string Error_TempoSignatureSpeedTooFast =
            "Tempo speed is too fast, the maximum tempo speed is 10000";

        public const string Error_InvalidCapoPosition = "Unrecognizable capo position, capo instruction ignored";
        public const string Error_CapoTooHigh = "Capo position is too high, maximum capo position is 12";

        public const string Warning_CapoStringsSpecifierNotEnclosed =
            "Capo strings specifier is not enclosed with ')', all strings assumed";

        public const string Warning_CapoStringsSpecifierInvalidStringNumber =
            "Invalid string number, all strings assumed";

        public const string Warning_BothCapoStringsSpecifierDelimiterUsed =
            "Use either comma or whitespace to separate string numbers, don't use both";

        public const string Warning_RedundantCapoStringSpecifier = "String #{0} is specified for more than once";

        public const string Warning_RhythmCommandletMissingCloseBracket =
            "Missing close bracket, please use both brackets or don't use brackets at all";
        public const string Error_RhythmCommandletMissingCloseBracket =
            "Missing close bracket";

        public const string Error_RhythmCommandletMissingCloseParenthesisInStringsSpecifier =
            "Missing close parenthesis in strings specifier";

        public const string Error_RhythmUnitInvalidStringNumberInStringsSpecifier =
            "Unrecognizable string number";

        public const string Error_RhythmNodeExpectOpeningBracket = "'[' expceted to declare a rhythm";

        public const string Error_RhythmUnitBodyExpected =
            "Note value, strings specification or all-string strum technique expected";
        public const string Error_RhythmUnitModifierExpected =
            "Strum technique, note effect technique, duration effect, accent or connection expected";
        public const string Warning_RhythmUnitStrumTechniqueAlreadySpecified =
            "Strum technique is already specified for this note, this one will be ignored";
        public const string Warning_RhythmUnitNoteEffectTechniqueAlreadySpecified =
            "Note effect technique is already specified for this note, this one will be ignored";
        public const string Warning_RhythmUnitNoteDurationEffectAlreadySpecified =
            "Duration effect is already specified for this note, this one will be ignored";
        public const string Warning_RhythmUnitAccentAlreadySpecified =
            "Accent is already specified for this note, this one will be ignored";
        public const string Warning_RhythmUnitConnectionAlreadySpecified =
            "Connection is already specified for this note, this one will be ignored";


        public const string Error_ArtificialHarmonicFretSpecifierNotEnclosed =
            "Artificial harmonic fret specifier is not enclosed with '>'";
    }
}
