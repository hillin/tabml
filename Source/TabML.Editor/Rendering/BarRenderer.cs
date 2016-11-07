using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    class BarRenderer : RendererBase
    {
        private readonly Bar _bar;

        public BarRenderer(PrimitiveRenderer primitiveRenderer, Bar bar) 
            : base(primitiveRenderer)
        {
            _bar = bar;
        }
    }
}
