using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Parser.AST;
using TabML.Parser.Document;

namespace TabML.Parser
{
    class TablatureContext
    {
        public TablatureNode Tablature { get; }
        public DocumentState DocumentState { get; private set; }

        private readonly List<Bar> _bars = new List<Bar>();
        public IReadOnlyList<Bar> Bars => _bars;

        public TablatureContext(TablatureNode tablature)
        {
            this.Tablature = tablature;
            this.DocumentState = new DocumentState();
        }

        public void AddBar(Bar bar)
        {
            if (!this.DocumentState.BarAppeared)
            {
                using (var state = this.AlterDocumentState())
                    state.BarAppeared = true;
            }

            bar.DocumentState = this.DocumentState;
            _bars.Add(bar);
        }

        public EditableDocumentState AlterDocumentState()
        {
            var state = new EditableDocumentState(this.DocumentState);
            this.DocumentState = state;
            return state;
        }
    }
}
