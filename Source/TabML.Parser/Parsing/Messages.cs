// ReSharper disable InconsistentNaming

namespace TabML.Parser.Parsing
{
    internal static class Messages
    {
        public const string Error_InstructionExpected = "An instruction is expected";
        public const string Error_UnknownInstruction = "Unrecognizable instruction";

        public const string Error_NoteValueExpected = "Note value expected";
        public const string Error_InvalidReciprocalNoteValue = "Reciprocal value must be power of 2 (e.g. 4, 8, 16 etc.)";
        public const string Error_TupletValueExpected = "Tuplet value expected";
        public const string Error_InvalidTuplet = "Tuplet value must be between 3 and 63, and cannot be power of 2";

        public const string Error_TooManyDotsInNoteValueAugment = "At most three dots supported as note value augment";

        public const string Error_InvalidNoteName = "Unrecognizable note name";
        public const string Error_InvalidAccidental = "Unrecognizable accidental";

        public const string Suggestion_TuningNotSpecified = "Redundant empty tuning specifier (standard tuning used)";
        public const string Error_InvalidTuning = "Unrecognizable tuning specifier, standard tuning assumed";

        public const string Hint_RedundantKnownTuningSpecifier =
            "\"{0}\" is a well-known tuning, so you don't have to explicitly define it";

        public const string Hint_RedundantColonInTuningSpecifier = "Redundant ':' in tuning specifier";
        
        
        public const string Error_MissingChordName = "Please specify a chord name";

        public const string Error_ChordDisplayNameNotEnclosed =
            "Chord display name specifier is not enclosed with '>'";

        public const string Error_ChordCommandletMissingFingering = "Missing chord fingering";

        public const string Error_ChordFingeringInvalidFingering = "Unrecognizable chord fingering";

        public const string Error_ChordFingeringNotMatchingStringCount =
            "{0} fingering specifiers is required for a chord";

        public const string Warning_ChordAlreadyDefined =
            "A chord with the same name is already defined, this one will be ignored";

        public const string Suggestion_UnknownChord =
            "'{0}' is not a known chord, please define it using the +chord instruction";

        public const string Error_InvalidTimeSignature =
            "Unrecognizable time signature, please use a time signature like 4/4. 4/4 assumed.";

        public const string Error_UnsupportedBeatsInTimeSignature =
            "Time signature with more than 32 beats per bar is not supported. 4/4 assumed.";

        public const string Error_UnsupportedNoteValueInTimeSignature =
            "Time signature with a note value shorter than 1/32 is not supported. 4/4 assumed.";

        public const string Error_IrrationalNoteValueInTimeSignatureNotSupported =
            "Time signature with an irrational note value is not supported. 4/4 assumed.";

        public const string Error_InvalidKeySignature = "Unrecognizable key signature, key signature ignored";

        public const string Suggestion_RedundantKeySignature =
            "Redundant key signature, the score is already in this key";

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

        public const string Error_CapoStringsSpecifierInvalidStringNumber =
            "Invalid string number, all strings assumed";

        public const string Warning_RedundantCapoStringSpecifier = "String #{0} is specified for more than once";

        public const string Error_CapoInstructionAfterBarAppeared = "Capo instruction must appear before all bars";

        public const string Suggestion_UselessCapoInstruction =
            "This capo instruction is useless because it's overridden by other capo instructions";

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

        public const string Error_BeatBodyExpected =
            "Note value, strings specification or all-string strum technique expected";
        public const string Error_BeatModifierExpected =
            "Strum technique, note effect technique, duration effect, accent or connection expected";
        public const string Warning_BeatStrumTechniqueAlreadySpecified =
            "Strum technique is already specified for this note, this one will be ignored";
        public const string Warning_BeatNoteEffectTechniqueAlreadySpecified =
            "Note effect technique is already specified for this note, this one will be ignored";
        public const string Warning_BeatNoteDurationEffectAlreadySpecified =
            "Duration effect is already specified for this note, this one will be ignored";
        public const string Warning_BeatAccentAlreadySpecified =
            "Accent is already specified for this note, this one will be ignored";
        public const string Warning_BeatConnectionAlreadySpecified =
            "Connection is already specified for this note, this one will be ignored";
        
        public const string Warning_BeatsNotMatchingTimeSignature = "Beats in this bar does not match time signature";

        public const string Error_ArtificialHarmonicFretSpecifierNotEnclosed =
            "Artificial harmonic fret specifier is not enclosed with '>'";

        public const string Warning_SectionNameMissingCloseQuoteMark = "Missing close quote mark";
        public const string Warning_EmptySectionName = "Empty section name, ignored";

        public const string Warning_AlternationTextExpectedAfterColon = "Alternation text expected. If you want to use implicit alternation text, omit the colon";
        public const string Hint_EmptyAlternationText = "Empty alternation text, will use automatic index";
        public const string Error_InvalidAlternationText= "Unrecognizable alternation text, use 1 to 9 (arabic numerals) or I to IX (roman numerals)";

        public const string Warning_StaffCommandletUnknownStaffType =
            "Unknown or unsupported staff type, guitar assumed";
        
        public const string Error_UnexpectedLyrics =
            "Unexpected lyrics, lyrics should only appear at the end of a bar for not more than once";

        public const string Warning_UnexpectedBarVoice =
            "Unexpected voice, rhythm or chord here, ignored. Please place them before lyrics";
        
        public const string Warning_TiedLyricsNotEnclosed = "Tied lyrics not enclosed with ')'";
        public const string Error_RhythmSegmentMissingFingering = "Missing chord fingering";

        public const string Error_RhythmSegmentChordFingeringNotEnclosed =
            "Chord fingering is not enclosed with ')'";

        public const string Error_RhythmDefinitionExpected = "rhythm definition, chord name or anonymous chord fingering expected";

        public const string Warning_PatternBodyNotEnclosed =
            "pattern body is not enclosed with '}'";

        public const string Warning_PatternInstanceBarsLessThanTemplateBars =
            "this pattern contains less bars than it's defined in its template";

        public const string Error_InvalidBarLineInPattern = "Only standard bar line allowed in patterns";

        public const string Warning_RedundantModifiersInRestBeat =
            "This beat is a rest, so all other specifiers are omitted";

        public const string Hint_RedundantModifiersInTiedBeat =
            "This is a tied beat a rest, you don't need to write it again";

        public const string Warning_InconsistentAlternationTextExplicity =
            "Either specify all alternation texts explicitly, or leave them empty at all";

        public const string Warning_InconsistentAlternationTextType =
            "Inconsistent alternation text type (arabic or roman number representation), the first representation type will be adopted";

        public const string Error_DuplicatedAlternationText = "Alternation {0} is already defined";
        public const string Error_MissingAlternationTexts = "The following alternation(s) is/are missing: {0}";

        public const string Hint_FirstOpenBarLineMissing = "Missing first open bar line, standard line assumed";
        public const string Hint_LastCloseBarLineMissing = "Missing close bar line, end line assumed";
        public const string Warning_BarLineMissing = "Missing bar line, standard line assumed";
    }
}
