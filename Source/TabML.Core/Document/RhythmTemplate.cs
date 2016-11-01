using System.Collections.Generic;
using System.Linq;
using TabML.Core.Logging;
using TabML.Parser.Parsing;

namespace TabML.Core.Document
{
    public class RhythmTemplate : Element
    {
        public List<RhythmTemplateSegment> Segments { get; }

        public RhythmTemplate()
        {
            this.Segments = new List<RhythmTemplateSegment>();
        }

        public Rhythm Instantialize()
        {
            var rhythm = new Rhythm();  // do not set Range
            rhythm.Segments.AddRange(this.Segments.Select(s => s.Instantialize()));
            return rhythm;
        }

        internal Rhythm Apply(Rhythm rhythm, IReporter reporter)
        {
            var templateInstance = this.Instantialize();

            if (rhythm == null)
                return templateInstance;

            if (rhythm.Segments.Count == 0) // empty rhythm, should be filled with rest
                return rhythm;

            if (rhythm.Segments.Any(s => s.FirstVoice != null))  // rhythm already defined
                return rhythm;

            if (rhythm.Segments.Count > templateInstance.Segments.Count)
            {
                //todo
                //reporter.Report(ReportLevel.Warning, rhythm.Range,
                //                Messages.Warning_TooManyChordsToMatchRhythmTemplate);

                RhythmTemplate.CopyVoices(rhythm, templateInstance);

                for (var i = templateInstance.Segments.Count; i < rhythm.Segments.Count; ++i)
                {
                    rhythm.Segments[i].IsOmittedByTemplate = true;
                }
            }
            else if (rhythm.Segments.Count < templateInstance.Segments.Count && rhythm.Segments.Count != 1)
            {
                //todo
                //reporter.Report(ReportLevel.Warning, rhythm.Range,
                //                Messages.Warning_InsufficientChordsToMatchRhythmTemplate);

                var lastChord = rhythm.Segments[rhythm.Segments.Count - 1].Chord;

                RhythmTemplate.CopyVoices(rhythm, templateInstance);

                for (var i = rhythm.Segments.Count; i < templateInstance.Segments.Count; ++i)
                {
                    var segment = templateInstance.Segments[i];
                    segment.Chord = lastChord;
                    rhythm.Segments.Add(segment);
                }
            }
            else
            {
                RhythmTemplate.CopyVoices(rhythm, templateInstance);
            }

            return rhythm;
        }

        private static void CopyVoices(Rhythm rhythm, Rhythm templateInstance)
        {
            for (var i = 0; i < templateInstance.Segments.Count; ++i)
            {
                rhythm.Segments[i].TrebleVoice = templateInstance.Segments[i].TrebleVoice;
                rhythm.Segments[i].BassVoice = templateInstance.Segments[i].BassVoice;
            }
        }
    }
}
