using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing
{
    class RhythmParser : ParserBase<RhythmNode>
    {
        public bool OptionalBrackets { get; }

        public RhythmParser(bool optionalBrackets = false)
        {
            this.OptionalBrackets = optionalBrackets;
        }

        public override bool TryParse(Scanner scanner, out RhythmNode result)
        {
            var hasBrackets = scanner.Expect('[');

            if (!this.OptionalBrackets)
            {
                this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                            ParseMessages.Error_RhythmNodeExpectOpeningBracket);
                result = null;
                return false;
            }


            result = new RhythmNode();

            RhythmUnitNode unit;
            scanner.SkipWhitespaces();
            while (new RhythmUnitParser().TryParse(scanner, out unit))
            {
                result.Units.Add(unit);
                scanner.SkipWhitespaces();
            }

            if (hasBrackets)
            {
                if (scanner.Expect(']'))
                    return true;

                if (this.OptionalBrackets)
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Warning_RhythmCommandletMissingCloseBracket);
                else
                {
                    this.Report(ParserReportLevel.Error, scanner.LastReadRange,
                                ParseMessages.Error_RhythmCommandletMissingCloseBracket);
                    result = null;
                    return false;
                }
            }

            return true;
        }

    }
}
