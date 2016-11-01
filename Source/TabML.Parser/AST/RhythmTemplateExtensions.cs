using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.Logging;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    static class RhythmTemplateExtensions
    {

        public static Rhythm Apply(this RhythmTemplate template, Rhythm rhythm, ILogger logger)
        {
            var templateInstance = template.Instantialize();

            if (rhythm == null)
                return templateInstance;

            if (rhythm.Segments.Count == 0) // empty rhythm, should be filled with rest
                return rhythm;

            if (rhythm.Segments.Any(s => s.FirstVoice != null))  // rhythm already defined
                return rhythm;

            if (rhythm.Segments.Count > templateInstance.Segments.Count)
            {

                logger.Report(LogLevel.Warning, rhythm.Range,
                              Messages.Warning_TooManyChordsToMatchRhythmTemplate);

                RhythmTemplateExtensions.CopyVoices(rhythm, templateInstance);

                for (var i = templateInstance.Segments.Count; i < rhythm.Segments.Count; ++i)
                {
                    rhythm.Segments[i].IsOmittedByTemplate = true;
                }
            }
            else if (rhythm.Segments.Count < templateInstance.Segments.Count && rhythm.Segments.Count != 1)
            {
                logger.Report(LogLevel.Warning, rhythm.Range,
                              Messages.Warning_InsufficientChordsToMatchRhythmTemplate);

                var lastChord = rhythm.Segments[rhythm.Segments.Count - 1].Chord;

                RhythmTemplateExtensions.CopyVoices(rhythm, templateInstance);

                for (var i = rhythm.Segments.Count; i < templateInstance.Segments.Count; ++i)
                {
                    var segment = templateInstance.Segments[i];
                    segment.Chord = lastChord;
                    rhythm.Segments.Add(segment);
                }
            }
            else
            {
                RhythmTemplateExtensions.CopyVoices(rhythm, templateInstance);
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
