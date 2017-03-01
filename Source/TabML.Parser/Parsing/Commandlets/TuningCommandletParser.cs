using System.Collections.Generic;
using TabML.Core;
using TabML.Core.Logging;
using TabML.Core.MusicTheory;
using TabML.Core.String;
using TabML.Parser.AST;

namespace TabML.Parser.Parsing.Commandlets
{
    [CommandletParser("tuning")]
    class TuningCommandletParser : CommandletParserBase<TuningCommandletNode>
    {
        private bool TryParseExplicitTuning(Scanner scanner, List<PitchNode> stringTunings, string name = null)
        {
            while (!scanner.EndOfLine)
            {
                PitchNode pitchNode;
                if (!new PitchParser().TryParse(scanner, out pitchNode))
                {
                    return false;
                }

                stringTunings.Add(pitchNode);
                scanner.SkipOptional(',', true);
            }

            return true;
        }


        public override bool TryParse(Scanner scanner, out TuningCommandletNode commandlet)
        {
            scanner.SkipOptional(':', true);

            commandlet = new TuningCommandletNode();

            var tuningString = scanner.ReadToLineEnd().Trim();
            if (string.IsNullOrEmpty(tuningString))
            {
                this.Report(LogLevel.Suggestion, scanner.LastReadRange, Messages.Suggestion_TuningNotSpecified);
                return true;
            }

            var colonIndex = tuningString.LastIndexOf(':');

            if (colonIndex >= 0)
            {
                var namePart = tuningString.Substring(0, colonIndex);

                var namePartIsEmpty = namePart == string.Empty;
                if (namePartIsEmpty)
                {
                    this.Report(LogLevel.Hint, scanner.LastReadRange.From.AsRange(scanner.Source),
                                Messages.Hint_RedundantColonInTuningSpecifier);
                }
                else
                {
                    commandlet.Name = new LiteralNode<string>(namePart,
                                                              new TextRange(scanner.LastReadRange.From,
                                                                            namePart.Length,
                                                                            scanner.Source));
                }

                var tuningPart = tuningString.Substring(colonIndex + 1).Trim();
                if (tuningPart == string.Empty)
                {
                    if (namePartIsEmpty)
                    {
                        this.Report(LogLevel.Suggestion, scanner.LastReadRange,
                                    Messages.Suggestion_TuningNotSpecified);
                        return true;
                    }

                    this.Report(LogLevel.Hint,
                                scanner.LastReadRange.From.OffsetColumn(colonIndex).AsRange(scanner.Source),
                                Messages.Hint_RedundantColonInTuningSpecifier);
                }
                else
                {
                    scanner.SetPointer(scanner.LastReadRange.From.OffsetColumn(colonIndex + 1));
                    if (this.TryParseExplicitTuning(scanner, commandlet.StringTunings, namePart))
                    {
                        // todo: validate
                        //if (!namePartIsEmpty)
                        //{
                        //    var namedTuning = Tunings.GetKnownTuning(namePart);
                        //    if (namedTuning != null && namedTuning.InOctaveEquals(explicitTuning))
                        //    {
                        //        this.Report(ReportLevel.Hint, scanner.LastReadRange,
                        //                    ParseMessages.Hint_RedundantKnownTuningSpecifier, namedTuning.Name);
                        //    }
                        //}
                        return true;
                    }

                    return false;
                }
            }
            else
            {
                var tuning = Tunings.GetKnownTuning(tuningString);
                if (tuning != null)
                {
                    commandlet.Name = new LiteralNode<string>(tuningString, scanner.LastReadRange);
                    return true;
                }

                scanner.SetPointer(scanner.LastReadRange.From);
                if (this.TryParseExplicitTuning(scanner, commandlet.StringTunings))
                {
                    return true;
                }
            }

            this.Report(LogLevel.Error, scanner.LastReadRange, Messages.Error_InvalidTuning);
            commandlet = null;
            return false;
        }

    }
}
