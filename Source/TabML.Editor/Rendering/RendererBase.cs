using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TabML.Editor.Rendering
{
    abstract class RendererBase
    {
        protected PrimitiveRenderer PrimitiveRenderer { get; }
        protected TablatureStyle Style { get; }

        protected RendererBase(PrimitiveRenderer primitiveRenderer, TablatureStyle style)
        {
            this.PrimitiveRenderer = primitiveRenderer;
            this.Style = style;
        }

        public abstract void Render(Point location, Size availableSize);
    }
}
