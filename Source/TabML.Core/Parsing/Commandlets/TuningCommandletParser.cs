using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;
using TabML.Core.Parsing.AST;

namespace TabML.Core.Parsing.Commandlets
{
    [CommandletParser("tuning")]
    class TuningCommandletParser : CommandletParserBase<TuningCommandletNode>
    {
        private static Tuning ParseExplicitTuning(string tuningString, string name = null)
        {
            var scanner = new Scanner(tuningString);
            var pitches = new List<Pitch>();
            while (!scanner.EndOfFile)
            {
                NoteName noteName;
                Debug.Assert(Common.TryParseNoteName(scanner, out noteName));

                int octave;
                if (!scanner.TryReadInteger(out octave))
                    octave = Pitch.NeutralOctaveGroup;

                pitches.Add(new Pitch(noteName, octave));

                scanner.SkipOptional(',');
            }

            return new Tuning(name, pitches.ToArray());
        }


        public override bool TryParse(Scanner scanner, out TuningCommandletNode commandlet)
        {
            scanner.SkipOptional(':', true);
            var tuningString = scanner.ReadToLineEnd().Trim();
            if (string.IsNullOrEmpty(tuningString))
            {
                this.Report(ParserReportLevel.Suggestion, scanner.LastReadRange, ParseMessages.Suggestion_TuningNotSpecified);
                commandlet = new TuningCommandletNode(Tunings.Standard);
                return true;
            }

            var parts = tuningString.Split(':');

            if (parts.Length >= 2)
            {
                var namePart = string.Join(":", parts.Take(parts.Length - 1));
                var tuningPart = parts[parts.Length - 1];

                var explicitTuning = TuningCommandletParser.ParseExplicitTuning(tuningPart, namePart);
                if (explicitTuning != null)
                {
                    var namedTuning = Tunings.GetKnownTuning(namePart);
                    if (namedTuning != null && namedTuning.InOctaveEquals(explicitTuning))
                    {
                        this.Report(ParserReportLevel.Hint, scanner.LastReadRange,
                                    ParseMessages.Hint_RedundantTuningSpecifier, namedTuning.Name);
                        commandlet = new TuningCommandletNode(namedTuning);
                    }
                    else
                        commandlet = new TuningCommandletNode(explicitTuning);
                    return true;
                }
            }
            else if (parts.Length == 1)
            {
                var tuning = Tunings.GetKnownTuning(tuningString);
                if (tuning != null)
                {
                    commandlet = new TuningCommandletNode(tuning);
                    return true;
                }

                tuning = TuningCommandletParser.ParseExplicitTuning(tuningString);
                if (tuning != null)
                {
                    commandlet = new TuningCommandletNode(tuning);
                    return true;
                }
            }

            this.Report(ParserReportLevel.Error, scanner.LastReadRange, ParseMessages.Error_InvalidTuning);
            commandlet = null;
            return false;
        }


        protected override CommandletNode Recover(Scanner scanner)
        {
            scanner.SkipLine();
            return new TuningCommandletNode(Tunings.Standard);
        }
    }
}
