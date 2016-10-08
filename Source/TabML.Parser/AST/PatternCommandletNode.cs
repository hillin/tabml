using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (this.InstanceBars.Bars.Count < this.TemplateBars.Bars.Count)
            {
                reporter.Report(ReportLevel.Warning, this.InstanceBars.Range,
                                Messages.Warning_PatternInstanceBarsLessThanTemplateBars);
            }


        }
    }
}
