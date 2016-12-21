using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Editor.Tablature.Layout;

namespace TabML.Editor.Rendering
{
    static class BeatElementRenderer
    {
        public static IBeatElementRenderer Create(ElementRenderer owner, IBeatElement element)
        {
            var beat = element as Beat;
            if (beat != null)
            {
                return new BeatRenderer(owner, beat);
            }

            var beam = element as Beam;
            if (beam != null)
            {
                return new BeamRenderer(owner, beam);
            }

            throw new InvalidOperationException();
        }
        
        public static BarRenderer FindOwnerBarRenderer(ElementRenderer renderer)
        {
            var owner = renderer.Owner;
            while (owner != null && !(owner is BarRenderer))
                owner = owner.Owner;

            return owner as BarRenderer;
        }
    }

    abstract class BeatElementRenderer<TElement> 
        : ElementRenderer<TElement, BarRenderingContext>, IBeatElementRenderer
        where TElement : ElementBase, IBeatElement
    {
        protected BeatElementRenderer(ElementRenderer owner, TElement element) 
            : base(owner, element)
        {
        }
        public abstract void Render();
    }
    

}
