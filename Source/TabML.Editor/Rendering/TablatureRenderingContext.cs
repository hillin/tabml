using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    class TablatureRenderingContext : RenderingContextBase
    {
        public PrimitiveRenderer PrimitiveRenderer { get; }
        public TablatureStyle Style { get; }
        public DocumentState PreviousDocumentState { get; set; }    // used for bar rendering to determine if the document state is changed
                                                                    // note while rendering a row, this value is pinned to the document state
                                                                    // of the last bar of previous row

        public TablatureRenderingContext(RenderingContext root, PrimitiveRenderer primitiveRenderer, TablatureStyle style)
            : base(root)
        {
            this.PrimitiveRenderer = primitiveRenderer;
            this.Style = style;
        }

    }
}
