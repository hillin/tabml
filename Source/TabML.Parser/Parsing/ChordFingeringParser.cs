using System;
using System.Linq;
using TabML.Core;
using TabML.Core.Logging;
using TabML.Parser.AST;
using TabML.Core.Document;
using TabML.Parser.Parsing.Bar;

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
            var remainingLine = scanner.RemainingLine.Trim();
            var containsDelimiter = remainingLine.Any(char.IsWhiteSpace);
            var containsFingerIndexSpecifier = remainingLine.Any(c => c == '<' || c == '>');
            var isShortForm = !containsDelimiter && !containsFingerIndexSpecifier;

            while (!_terminatorPredicate(scanner))
            {
                var noteAnchor = scanner.MakeAnchor();

                var str = isShortForm ? scanner.Read(@"[\dxX\-]") : scanner.ReadAny(@"\d+", @"[xX\-]");

                if (string.IsNullOrEmpty(str))
                {
                    this.Report(LogLevel.Warning, scanner.LastReadRange,
                                Messages.Error_ChordFingeringInvalidFingering);
                    result = null;
                    return false;
                }

                switch (str)
                {
                    case "x":
                    case "X":
                    case "-":
                        {
                            var fret = new LiteralNode<int>(ChordFingeringNode.FingeringSkipString, scanner.LastReadRange);
                            result.Fingerings.Add(new ChordFingeringNoteNode { Fret = fret, Range = scanner.LastReadRange });
                            break;
                        }
                    default:
                        int fretNumber;
                        if (!int.TryParse(str, out fretNumber)) // todo: prevent too large fret number
                        {
                            this.Report(LogLevel.Warning, scanner.LastReadRange,
                                        Messages.Error_ChordFingeringInvalidFingering);
                            result = null;
                            return false;
                        }

                        if (fretNumber > 24)
                        {
                            this.Report(LogLevel.Warning, scanner.LastReadRange,
                                        Messages.Warning_ChordFingeringFretTooHigh);
                        }

                        var note = new ChordFingeringNoteNode
                        {
                            Fret = new LiteralNode<int>(fretNumber, scanner.LastReadRange)
                        };

                        if (fretNumber != 0)
                        {
                            scanner.SkipWhitespaces();
                            if (scanner.Expect('<'))
                            {
                                scanner.SkipWhitespaces();

                                var fingerIndexString = scanner.Read(@"[\dtT]");
                                if (string.IsNullOrEmpty(fingerIndexString))
                                {
                                    this.Report(LogLevel.Error, scanner.Pointer.AsRange(),
                                                Messages.Error_ChordFingerIndexExpected);
                                    return false;
                                }


                                LeftHandFingerIndex fingerIndex;
                                switch (fingerIndexString)
                                {
                                    case "t":
                                    case "T":
                                        fingerIndex = LeftHandFingerIndex.Thumb; break;
                                    case "1":
                                        fingerIndex = LeftHandFingerIndex.Index; break;
                                    case "2":
                                        fingerIndex = LeftHandFingerIndex.Middle; break;
                                    case "3":
                                        fingerIndex = LeftHandFingerIndex.Ring; break;
                                    case "4":
                                        fingerIndex = LeftHandFingerIndex.Pinky; break;
                                    default:
                                        this.Report(LogLevel.Error, scanner.LastReadRange,
                                                    Messages.Error_UnrecognizableFingerIndex);
                                        return false;
                                }


                                note.FingerIndex = new LiteralNode<LeftHandFingerIndex>(fingerIndex,
                                                                                        scanner.LastReadRange);

                                scanner.SkipWhitespaces();

                                ExistencyNode importancy;
                                if (new CharExistencyParser('!').TryParse(scanner, out importancy))
                                {
                                    note.Importancy = importancy;
                                    scanner.SkipWhitespaces();
                                }

                                if (!scanner.Expect('>'))
                                {
                                    this.Report(LogLevel.Error, scanner.Pointer.AsRange(),
                                                Messages.Error_ChordFingerIndexNotEnclosed);
                                    return false;
                                }

                            }
                        }

                        note.Range = noteAnchor.Range;
                        result.Fingerings.Add(note);

                        break;
                }

                scanner.SkipWhitespaces();
            }

            if (result.Fingerings.Count != Defaults.Strings)
            {
                this.Report(LogLevel.Error, scanner.LastReadRange,
                            Messages.Error_ChordFingeringNotMatchingStringCount, Defaults.Strings);
                result = null;
                return false;
            }

            result.Range = anchor.Range;
            return true;
        }
    }
}
