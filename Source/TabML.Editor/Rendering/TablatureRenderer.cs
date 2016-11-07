using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTablature = TabML.Core.Document.Tablature;

namespace TabML.Editor.Rendering
{
    class TablatureRenderer : RendererBase
    {
        private readonly CoreTablature _tablature;

        public TablatureRenderer(PrimitiveRenderer primitiveRenderer, CoreTablature tablature)
            : base(primitiveRenderer)
        {
            _tablature = tablature;
        }
    }
}
