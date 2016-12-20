using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    interface IRootElementRenderer
    {
        TRenderer GetRenderer<TElement, TRenderer>(TElement element)
            where TElement : ElementBase
            where TRenderer : ElementRenderer<TElement>;

        void RegisterRenderer<TElement>(TElement element, ElementRenderer<TElement> renderer)
            where TElement : ElementBase;

        RenderingContext RenderingContext { get; }
    }
}
