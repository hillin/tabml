using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace TabML.Core.Parsing
{
    static class ParseMessages
    {
        public const string Suggestion_TuningNotSpecified = "Redundant empty tuning specifier (standard tuning used)";
        public const string Error_InvalidTuning = "Unrecognizable tuning specifier, standard tuning assumed";
        public const string Hint_RedundantTuningSpecifier = "\"{0}\" is a well-known tuning, so you don't have to explicitly define it";

        public const string Error_InvalidStringCount =
            "Unsupported string count, should be between 4 and 8 strings. 6 strings assumed";

        public const string Error_MissingChordName = "Please specify a chord name";
        public const string Error_MissingChordDisplayNameNotEnclosed =
            "Chord display name specifier is not enclosed with '>'";
        public const string Error_MissingChordFingering = "Missing chord fingering";
        public const string Warning_BothChordFingeringDelimiterUsed =
            "Use either comma or whitespace to separate chord fingering numbers, don't use both";

        public const string Error_InvalidTimeSignature = "Unrecognizable time signature, please use a time signature format like 4/4";
        public const string Error_UnsupportedBeatsInTimeSignature =
            "Time signature with more than 32 beats per bar is not supported";
        public const string Error_UnsupportedNoteValueInTimeSignature =
            "Time signature with a note value shorter than 1/32 is not supported";
        public const string Error_IrrationalNoteValueInTimeSignatureNotSupported =
            "Time signature with an irrational note value is not supported";
    }
}
