using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Parser.Document;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    partial class PatternCommandletNode : CommandletNode
    {
        public TemplateBarsNode TemplateBars { get; set; }
        public InstanceBarsNode InstanceBars { get; set; }


        protected override IEnumerable<Node> CommandletChildNodes
        {
            get
            {
                yield return this.TemplateBars;
                yield return this.InstanceBars;
            }
        }

        internal override bool Apply(TablatureContext context, IReporter reporter)
        {
            var templateBars = this.TemplateBars.Bars;
            var instanceBars = this.InstanceBars.Bars;
            if (instanceBars.Count < templateBars.Count)
            {
                reporter.Report(ReportLevel.Warning, this.InstanceBars.Range,
                                Messages.Warning_PatternInstanceBarsLessThanTemplateBars);
            }

            foreach (var bar in templateBars)
            {
                if (bar.Lyrics != null)
                {
                    reporter.Report(ReportLevel.Warning, bar.Lyrics.Range,
                                    Messages.Warning_TemplateBarCannotContainLyrics);
                }

                bar.ApplyRhythmTemplate
            }

            

            var templateIndex = 0;
            foreach (var instanceBar in instanceBars)
            {
                var templateBar = templateBars[templateIndex];
                
                var bar 

                ++templateIndex;
                if (templateIndex == templateBars.Count)
                    templateIndex = 0;
            }
        }


        public Rhythm ApplyRhythmTemplate(Rhythm template, Rhythm rhythm, IReporter reporter)
        {
            if (rhythm == null)
                return template;

            if (rhythm.Segments.Count == 0) // empty rhythm, should be filled with rest
                return rhythm;

            if (rhythm.Segments.Any(s => s.Voices.Count != 0))  // rhythm already defined
                return rhythm;

            if (rhythm.Segments.Count > template.Segments.Count)
            {
                reporter.Report(ReportLevel.Warning, rhythm.Range,
                                Messages.Warning_TooManyChordsToMatchRhythmTemplate);

                for (var i = 0; i < template.Segments.Count; ++i)
                {
                    rhythm.Segments[i].Voices.AddRange(template.Segments[i].Voices);
                }

                for (var i = template.Segments.Count; i < rhythm.Segments.Count; ++i)
                {
                    rhythm.Segments[i].IsOmittedByTemplate = true;
                }
            }
            else if (rhythm.Segments.Count < template.Segments.Count && rhythm.Segments.Count != 1)
            {
                reporter.Report(ReportLevel.Warning, rhythm.Range,
                                Messages.Warning_InsufficientChordsToMatchRhythmTemplate);

                var lastChord = rhythm.Segments[rhythm.Segments.Count - 1].Chord;

                for (var i = 0; i < rhythm.Segments.Count; ++i)
                {
                    rhythm.Segments[i].Voices.AddRange(template.Segments[i].Voices);
                }

                for (var i = rhythm.Segments.Count; i < template.Segments.Count; ++i)
                {
                    var segment = template.Segments[i].Clone();
                    segment.Chord = lastChord;
                    rhythm.Segments.Add(segment);
                }
            }
            else
            {
                for (var i = 0; i < template.Segments.Count; ++i)
                {
                    rhythm.Segments[i].Voices.AddRange(template.Segments[i].Voices);
                }
            }

            return rhythm;
        }
    }
}
