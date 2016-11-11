using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.MusicTheory;

namespace TabML.Editor.Tablature.Layout
{
    interface IBeatElement
    {
        PreciseDuration GetDuration();
        void Draw(IBarDrawingContext drawingContext, double[] columnPositions, BeamSlope beamSlope);
    }
}
