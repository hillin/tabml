using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;
using TabML.Parser.Parsing;

namespace TabML.Parser.AST
{
    class AlternateCommandletNode : CommandletNode
    {
        public List<LiteralNode<string>> AlternationTexts { get; }

        protected override IEnumerable<Node> CommandletChildNodes => this.AlternationTexts;

        public AlternateCommandletNode()
        {
            this.AlternationTexts = new List<LiteralNode<string>>();
        }

        internal override bool Apply(TablatureContext context, IReporter reporter)
        {
            using (var state = context.AlterDocumentState())
            {
                if (this.AlternationTexts.Count == 0)   // implicit
                {
                    if (state.AlternationTextExplicity == Explicity.Explicit)
                        reporter.Report(ReportLevel.Warning, this.Range.To.AsRange(),
                                        Messages.Warning_InconsistentAlternationTextExplicity);
                    else
                        state.AlternationTextExplicity = Explicity.Implicit;

                    var implicitIndex = state.DefinedAlternationIndices.Max() + 1;
                    state.DefinedAlternationIndices.Add(implicitIndex);
                    state.CurrentAlternation = this;

                    return true;
                }

                foreach (var alternationText in this.AlternationTexts)
                {
                    int index;
                    AlternationTextType type;
                    Debug.Assert(AlternationText.TryParse(alternationText.Value,
                                                          out index, out type));

                    if (state.AlternationTextType != type)
                    {
                        reporter.Report(ReportLevel.Warning, alternationText.Range,
                                        Messages.Warning_InconsistentAlternationTextType);
                    }
                    else
                        state.AlternationTextType = type;

                    if (state.DefinedAlternationIndices.Contains(index))
                    {
                        reporter.Report(ReportLevel.Error, alternationText.Range,
                                        Messages.Error_DuplicatedAlternationText, alternationText.Value);
                        return false;
                    }

                    state.DefinedAlternationIndices.Add(index);
                }

                return true;
            }
        }

    }
}
