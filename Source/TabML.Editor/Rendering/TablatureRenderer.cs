using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CoreTablature = TabML.Core.Document.Tablature;

namespace TabML.Editor.Rendering
{
    class TablatureRenderer : RendererBase
    {
        private readonly CoreTablature _tablature;

        public TablatureRenderer(PrimitiveRenderer primitiveRenderer, TablatureStyle style, CoreTablature tablature)
            : base(primitiveRenderer, style)
        {
            _tablature = tablature;
        }

        public override void Render(Point location, Size availableSize)
        {
            //foreach (var bar in _tablature.Bars)
            //{
            new BarRenderer(this.PrimitiveRenderer, this.Style, _tablature.Bars[0]).Render(location, availableSize);
            //}
        }
    }
}
