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

        internal override bool Apply(TablatureContext context, ILogger logger)
        {
            var templateBarNodes = this.TemplateBars.Bars;
            var instanceBarNodes = this.InstanceBars.Bars;
            if (instanceBarNodes.Count < templateBarNodes.Count)
            {
                logger.Report(LogLevel.Warning, this.InstanceBars.Range,
                                Messages.Warning_PatternInstanceBarsLessThanTemplateBars);
            }

            var templateBars = new List<Bar>();
            foreach (var barNode in templateBarNodes)
            {
                if (barNode.Lyrics != null)
                {
                    logger.Report(LogLevel.Warning, barNode.Lyrics.Range,
                                    Messages.Warning_TemplateBarCannotContainLyrics);
                }

                Bar bar;
                if (!barNode.ToDocumentElement(context, logger, out bar))
                    return false;

                templateBars.Add(bar);
            }

            var templateIndex = 0;
            foreach (var barNode in instanceBarNodes)
            {
                var templateBar = templateBars[templateIndex];

                Bar instanceBar;
                if (this.ApplyTemplateBar(templateBar, barNode, out instanceBar, context, logger))
                {
                    context.AddBar(instanceBar);
                }

                ++templateIndex;
                if (templateIndex == templateBarNodes.Count)
                    templateIndex = 0;
            }

            return true;
        }


        public bool ApplyTemplateBar(Bar template, BarNode instanceNode, out Bar instanceBar, TablatureContext context, ILogger logger)
        {
            if (instanceNode == null)
            {
                instanceBar = template;
                return true;
            }

            if (!instanceNode.ToDocumentElement(context, logger, out instanceBar))
                return false;

            if (instanceBar.Rhythm != null && instanceBar.Rhythm.Segments.Count > 0) // rhythm already defined
                return true;

            instanceBar.Rhythm = template.Rhythm.Clone();

            return true;
        }
    }
}
