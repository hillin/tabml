using System;
using System.Collections.Generic;
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


        private static Tuning ParseExplicitTuning(string tuningString)
        {
            var scanner = new Scanner(tuningString);
            var pitches = new List<Pitch>();
            while (!scanner.EndOfFile)
            {
                var noteNameChar = scanner.Read();
                BaseNoteName noteName;
                if (!BaseNoteNames.TryParse(noteNameChar, out noteName))
                    return null;

                var accidentalText = scanner.Read("[#b♯♭\u1d12a\u1d12b]*");
                Accidental accidental;
                Accidentals.TryParse(accidentalText, out accidental);

                int octave;
                if (!scanner.TryReadInteger(out octave))
                    octave = Pitch.NeutralOctaveGroup;

                pitches.Add(new Pitch(new NoteName(noteName, accidental), octave));

                scanner.SkipOptional(',');
            }

            return new Tuning(pitches.ToArray());
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

                var explicitTuning = TuningCommandletParser.ParseExplicitTuning(tuningPart);
                if (explicitTuning != null)
                {
                    var namedTuning = Tunings.GetNamedTuning(namePart);
                    commandlet = new TuningCommandletNode(explicitTuning);
                    return true;
                }
            }
            else if (parts.Length == 1)
            {
                var tuning = Tunings.GetNamedTuning(tuningString);
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
