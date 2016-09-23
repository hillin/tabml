using System;
using TabML.Core.Document;
using TabML.Parser.AST;

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

            while (!_terminatorPredicate(scanner))
            {
                var str = scanner.Read(@"\d+|[xX\-]");

                if (string.IsNullOrEmpty(str))
                {
                    this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                ParseMessages.Error_ChordFingeringInvalidFingering);
                    result = null;
                    return false;
                }

                switch (str)
                {
                    case "x":
                    case "X":
                    case "-":
                        result.Fingerings.Add(new LiteralNode<int>(ChordDefinition.FingeringSkipString, scanner.LastReadRange));
                        break;
                    default:
                        int fretNumber;
                        if (!int.TryParse(str, out fretNumber)) // todo: prevent too large fret number
                        {
                            this.Report(ParserReportLevel.Warning, scanner.LastReadRange,
                                        ParseMessages.Error_ChordFingeringInvalidFingering);
                            result = null;
                            return false;
                        }
                        result.Fingerings.Add(new LiteralNode<int>(fretNumber, scanner.LastReadRange));
                        break;
                }

                scanner.SkipWhitespaces();
            }

            result.Range = anchor.Range;
            return true;
        }
    }
}
