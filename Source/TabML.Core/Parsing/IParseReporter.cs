using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.Parsing
{
    interface IParseReporter
    {
        void Report(ParserReportLevel level, TextRange position, string message, params object[] args);
    }
}
