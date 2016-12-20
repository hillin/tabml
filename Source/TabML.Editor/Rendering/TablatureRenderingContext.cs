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


        public TablatureRenderingContext(RenderingContext root, PrimitiveRenderer primitiveRenderer, TablatureStyle style)
            : base(root)
        {
            this.PrimitiveRenderer = primitiveRenderer;
            this.Style = style;
        }

    }
}
