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
        Ghost = 1 << 0,
        NaturalHarmonic = 1 << 1,
        ArtificialHarmonic = 1 << 2
    }
}
