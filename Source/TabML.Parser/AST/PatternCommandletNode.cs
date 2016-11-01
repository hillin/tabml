using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Logging;
using TabML.Core.Document;
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
            var templateBarNodes = this.TemplateBars.Bars;
            var instanceBarNodes = this.InstanceBars.Bars;
            if (instanceBarNodes.Count < templateBarNodes.Count)
            {
                reporter.Report(ReportLevel.Warning, this.InstanceBars.Range,
                                Messages.Warning_PatternInstanceBarsLessThanTemplateBars);
            }

            var templateBars = new List<Bar>();
            foreach (var barNode in templateBarNodes)
            {
                if (barNode.Lyrics != null)
                {
                    reporter.Report(ReportLevel.Warning, barNode.Lyrics.Range,
                                    Messages.Warning_TemplateBarCannotContainLyrics);
                }

                Bar bar;
                if (!barNode.ToDocumentElement(context, reporter, out bar))
                    return false;

                templateBars.Add(bar);
            }

            var templateIndex = 0;
            foreach (var barNode in instanceBarNodes)
            {
                var templateBar = templateBars[templateIndex];

                Bar instanceBar;
                if (this.ApplyTemplateBar(templateBar, barNode, out instanceBar, context, reporter))
                {
                    context.AddBar(instanceBar);
                }

                ++templateIndex;
                if (templateIndex == templateBarNodes.Count)
                    templateIndex = 0;
            }

            return true;
        }


        public bool ApplyTemplateBar(Bar template, BarNode instanceNode, out Bar instanceBar, TablatureContext context, IReporter reporter)
        {
            if (instanceNode == null)
            {
                instanceBar = template;
                return true;
            }

            if (!instanceNode.ToDocumentElement(context, reporter, out instanceBar))
                return false;

            if (instanceBar.Rhythm != null && instanceBar.Rhythm.Segments.Count > 0) // rhythm already defined
                return true;

            instanceBar.Rhythm = template.Rhythm.Clone();

            return true;
        }
    }
}
