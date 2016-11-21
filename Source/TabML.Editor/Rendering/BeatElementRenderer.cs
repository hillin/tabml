using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Editor.Tablature.Layout;

namespace TabML.Editor.Rendering
{
    class BeatElementRenderer
    {
        public void Render(BarDrawingContext drawingContext, IBeatElement element, BeamSlope beamSlope)
        {
            var beat = element as Beat;
            if (beat != null)
            {
                new BeatRenderer().Render(drawingContext, beat, beamSlope);
                return;
            }

            var beam = element as Beam;
            if (beam != null)
            {
                new BeamRenderer().Render(drawingContext, beam, beamSlope);
                return;
            }

            throw new InvalidOperationException();
        }
    }
}
