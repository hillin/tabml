using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Editor.Rendering
{
    abstract class RendererBase
    {
        protected PrimitiveRenderer PrimitiveRenderer { get; }

        protected RendererBase(PrimitiveRenderer primitiveRenderer)
        {
            this.PrimitiveRenderer = primitiveRenderer;
        }
    }
}
