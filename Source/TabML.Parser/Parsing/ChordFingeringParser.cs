using System;
using System.Linq;
using TabML.Core;
using TabML.Parser.AST;
using TabML.Parser.Document;

namespace TabML.Parser.Parsing
{
    class ChordFingeringParser : ParserBase<ChordFingeringNode>
    {
        private readonly Predicate<Scanner> _terminatorPredicate;

        public ChordFingeringParser(Predicate<Scanner> terminatorPredicate)
        {
            _terminatorPredicate = terminatorPredicate;
        }

        public override bool TryParse(Scanner scanner, out ChordFingeringNode result)
        {
            result = new ChordFingeringNode();
            var anchor = scanner.MakeAnchor();
            var containsDelimiter = scanner.RemainingLine.Trim().Any(char.IsWhiteSpace);

            while (!_terminatorPredicate(scanner))
            {
                var str = containsDelimiter ? scanner.ReadAny(@"\d+", @"[xX\-]") : scanner.Read(@"[\dxX\-]");

                if (string.IsNullOrEmpty(str))
                {
                    this.Report(ReportLevel.Warning, scanner.LastReadRange,
                                Messages.Error_ChordFingeringInvalidFingering);
                    result = null;
                    return false;
                }

                switch (str)
                {
                    case "x":
                    case "X":
                    case "-":
                        result.Fingerings.Add(new LiteralNode<int>(ChordFingeringNode.FingeringSkipString, scanner.LastReadRange));
                        break;
                    default:
                        int fretNumber;
                        if (!int.TryParse(str, out fretNumber)) // todo: prevent too large fret number
                        {
                            this.Report(ReportLevel.Warning, scanner.LastReadRange,
                                        Messages.Error_ChordFingeringInvalidFingering);
                            result = null;
                            return false;
                        }
                        result.Fingerings.Add(new LiteralNode<int>(fretNumber, scanner.LastReadRange));
                        break;
                }

                scanner.SkipWhitespaces();
            }

            if (result.Fingerings.Count != Defaults.Strings)
            {
                this.Report(ReportLevel.Error, scanner.LastReadRange,
                            Messages.Error_ChordFingeringNotMatchingStringCount, Defaults.Strings);
                result = null;
                return false;
            }

            result.Range = anchor.Range;
            return true;
        }
    }
}
