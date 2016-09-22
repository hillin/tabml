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
        public const string Error_InvalidNoteName = "Unrecognizable note name";
        public const string Error_InvalidAccidental = "Unrecognizable accidental";

        public const string Suggestion_TuningNotSpecified = "Redundant empty tuning specifier (standard tuning used)";
        public const string Error_InvalidTuning = "Unrecognizable tuning specifier, standard tuning assumed";

        public const string Hint_RedundantKnownTuningSpecifier =
            "\"{0}\" is a well-known tuning, so you don't have to explicitly define it";

        public const string Hint_RedundantColonInTuningSpecifier = "Redundant ':' in tuning specifier";

        public const string Error_InvalidStringCount =
            "Unrecognizable string count, 6 strings assumed";

        public const string Error_StringCountOutOfRange =
            "Unsupported string count, should be between {0} and {1} strings. 6 strings assumed";

        public const string Error_MissingChordName = "Please specify a chord name";

        public const string Error_MissingChordDisplayNameNotEnclosed =
            "Chord display name specifier is not enclosed with '>'";

        public const string Error_ChordCommandletMissingFingering = "Missing chord fingering";

        public const string Error_ChordFingeringInvalidFingering = "Unrecognizable chord fingering";

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
        public const string Warning_CapoTooHigh = "Capo position is too high, maximum capo position is 12";

        public const string Warning_CapoStringsSpecifierNotEnclosed =
            "Capo strings specifier is not enclosed with ')', all strings assumed";

        public const string Warning_CapoStringsSpecifierInvalidStringNumber =
            "Invalid string number, all strings assumed";

        public const string Warning_RedundantCapoStringSpecifier = "String #{0} is specified for more than once";

        public const string Warning_RhythmSegmentMissingCloseBracket =
            "Missing close bracket, please use both brackets or don't use brackets at all";
        public const string Error_RhythmSegmentMissingCloseBracket =
            "Missing close bracket";
        public const string Warning_EmptyRhythmSegment =
            "This rhythm segment is empty. To use a rhythm template, omit this rhythm segment";

        public const string Error_UnrecognizableRhythmSegmentElement =
            "Unrecognizable note";

        public const string Error_RhythmCommandletMissingCloseParenthesisInStringsSpecifier =
            "Missing close parenthesis in strings specifier";

        public const string Error_RhythmUnitInvalidStringNumberInStringsSpecifier =
            "Unrecognizable string number";

        public const string Error_RhythmUnitInvalidFretNumberInStringsSpecifier =
            "Unrecognizable fret number";

        public const string Error_RhythmSegmentExpectOpeningBracket = "'[' expceted to declare a rhythm";

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

        public const string Warning_SectionNameMissingCloseQuoteMark = "Missing close quote mark";
        public const string Warning_EmptySectionName = "Empty section name, ignored";

        public const string Warning_StaffCommandletUnknownStaffType =
            "Unknown or unsupported staff type, guitar assumed";

        public const string Warning_MissingEndBarLine = "Missing end bar line";

        public const string Error_UnexpectedLyrics =
            "Unexpected lyrics, lyrics should only appear at the end of a bar for not more than once";

        public const string Warning_UnexpectedBarVoice =
            "Unexpected voice, rhythm or chord here, ignored. Please place them before lyrics";

        public const string Error_InvalidBarContent =
            "Unrecognizable bar content, voice, rhythm, chord or lyrics expected";

        public const string Error_InvalidBarContent_EndBarLineExpected =
            "Unrecognizable bar content, end bar line expected";

        public const string Warning_TiedLyricsNotEnclosed = "Tied lyrics not enclosed with ')'";
        public const string Error_RhythmSegmentMissingFingering = "Missing chord fingering";

        public const string Error_RhythmSegmentChordFingeringNotEnclosed =
            "Chord fingering is not enclosed with ')'";

        public const string Error_RhythmDefinitionExpected = "rhythm definition, chord name or anonymous chord fingering expected";
    }
}
