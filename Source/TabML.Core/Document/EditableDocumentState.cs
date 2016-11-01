using System;

namespace TabML.Core.Document
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
