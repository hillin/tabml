using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TabML.Editor.Rendering
{
    class BoundingBoxUnion
    {
        public Rect Bounds { get; private set; }
        public bool HasAnyBounds { get; private set; }

        public void AddBounds(Rect bounds)
        {
            if (!this.HasAnyBounds)
            {
                this.Bounds = bounds;
                this.HasAnyBounds = true;
            }
            else
                this.Bounds.Union(bounds);
        }
    }
}
