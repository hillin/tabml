using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TabML.Core.Document;
using TabML.Editor.Tablature.Layout;

namespace TabML.Editor.Rendering
{
    class BarRenderer : RendererBase
    {
        private readonly ArrangedBar _bar;

        public BarRenderer(PrimitiveRenderer primitiveRenderer, TablatureStyle style, Bar bar)
            : base(primitiveRenderer, style)
        {
            _bar = new BarArranger().Arrange(bar);
        }

        public override void Render(Point location, Size availableSize)
        {
            var context = new BarRenderContext(location, availableSize, this.PrimitiveRenderer, this.Style);
            _bar.Draw(context, availableSize.Width);
        }
    }
}
