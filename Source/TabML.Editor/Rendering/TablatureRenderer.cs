using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CoreTablature = TabML.Core.Document.Tablature;

namespace TabML.Editor.Rendering
{
    class TablatureRenderer
    {
        public PrimitiveRenderer PrimitiveRenderer { get; }
        public TablatureStyle Style { get; }

        public TablatureRenderer(PrimitiveRenderer primitiveRenderer, TablatureStyle style)
        {
            this.PrimitiveRenderer = primitiveRenderer;
            this.Style = style;
        }


        public void Render(CoreTablature tablature, Point location, Size availableSize)
        {
            this.PrimitiveRenderer.Clear();
            new BarRenderer(this.PrimitiveRenderer, this.Style).Render(tablature.Bars[0], location, availableSize);
        }
    }
}
