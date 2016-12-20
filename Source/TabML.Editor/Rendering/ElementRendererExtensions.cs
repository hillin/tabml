using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Editor.Rendering
{
    static class ElementRendererExtensions
    {
        /// <summary>
        /// utility method to set all <c>RenderingContext</c>s of <para>renderers</para> to <para>renderingContext</para>
        /// </summary>
        public static void AssignRenderingContexts<TRenderingContext>(
            this IEnumerable<IElementRendererWithContext<TRenderingContext>> renderers,
            TRenderingContext renderingContext)
            where TRenderingContext : RenderingContextBase
        {
            foreach (var renderer in renderers)
                renderer.RenderingContext = renderingContext;
        }

        public static void Initialize(this IEnumerable<ElementRenderer> renderers)
        {
            foreach (var renderer in renderers)
                renderer.Initialize();
        }
    }
}
