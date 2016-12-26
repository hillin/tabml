using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Editor.Rendering
{
    [Flags]
    enum NoteRenderingFlags
    {
        None = 0,
        HalfOrLonger = 1 << 0,
        Ghost = 1 << 1,
        NaturalHarmonic = 1 << 2,
        ArtificialHarmonic = 1 << 3
    }
}
