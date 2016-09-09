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

        public const string Error_InvalidStringCount =
            "Unsupported string count, should be between 4 and 8 strings. 6 strings assumed";

        
    }
}
