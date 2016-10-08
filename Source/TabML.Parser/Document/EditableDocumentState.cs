using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Parser.Document;

namespace TabML.Parser
{
    class EditableDocumentState : DocumentState, IDisposable
    {
        public EditableDocumentState(DocumentState state)
        {
            DocumentState.Clone(state, this);
        }
        
        public void Dispose()
        {
            this.Seal();
        }
    }
}
