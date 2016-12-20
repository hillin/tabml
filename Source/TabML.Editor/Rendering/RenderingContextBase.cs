using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Editor.Rendering
{
    abstract class RenderingContextBase
    {
        public RenderingContextBase Owner { get; }
        public RenderingContext Root => this as RenderingContext ?? this.Owner.Root;

        protected RenderingContextBase(RenderingContextBase owner)
        {
            this.Owner = owner;
        }
    }

    abstract class RenderingContextBase<TOwnerRenderingContext> : RenderingContextBase
        where TOwnerRenderingContext : RenderingContextBase
    {
        public new TOwnerRenderingContext Owner => (TOwnerRenderingContext)base.Owner;

        protected RenderingContextBase(TOwnerRenderingContext owner)
            : base(owner)
        {

        }
    }
}
