using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Editor.Rendering
{
    interface IElementRendererWithContext<TRenderingContext>
        where TRenderingContext : RenderingContextBase
    {
        TRenderingContext RenderingContext { get; set; }

        void Initialize();
    }
}
