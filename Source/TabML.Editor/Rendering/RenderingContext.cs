using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    class RenderingContext : RenderingContextBase
    {
        private readonly Dictionary<ElementRenderer, RenderingContextBase> _renderingContextLookup;

        public RenderingContext()
            : base(null)
        {
            _renderingContextLookup = new Dictionary<ElementRenderer, RenderingContextBase>();
        }

        public virtual TRenderingContext GetRenderingContext<TRenderingContext>(ElementRenderer renderer)
            where TRenderingContext : RenderingContextBase
        {
            RenderingContextBase renderingContext;
            if (!_renderingContextLookup.TryGetValue(renderer, out renderingContext))
                return null;

            return (TRenderingContext)renderingContext;
        }

        public virtual void AssignRenderingContext(ElementRenderer renderer, RenderingContextBase renderingContext)
        {
            _renderingContextLookup[renderer] = renderingContext;
        }
    }
}
