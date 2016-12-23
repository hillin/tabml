using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Editor.Tablature.Layout;

namespace TabML.Editor.Rendering
{
    interface IBeatElementRenderer : IElementRendererWithContext<BarRenderingContext>
    {
        Task Render();
    }
}
