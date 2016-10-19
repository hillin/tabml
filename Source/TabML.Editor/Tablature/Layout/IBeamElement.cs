using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Editor.Tablature.Layout
{
    interface IBeamElement
    {
        PreciseDuration GetDuration();
    }
}
