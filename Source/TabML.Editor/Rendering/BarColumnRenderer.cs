using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    class BarColumnRenderer : ElementRenderer<BarColumn, BarRenderingContext>
    {


        public BarColumnRenderer(ElementRenderer owner, BarColumn element)
            : base(owner, element)
        {
        }

        public void Render(BarColumnRenderingInfo renderingInfo)
        {
            //todo: draw chord and lyrics
        }
    }
}
