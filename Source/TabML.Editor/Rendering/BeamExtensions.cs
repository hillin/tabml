using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    static class BeamExtensions
    {
        public static Beat GetFirstBeat(this Beam beam)
        {
            while (true)
            {
                if (beam.Elements.Count == 0)
                    return null;
                var beat = beam.Elements[0] as Beat;
                if (beat != null) return beat;
                beam = (Beam)beam.Elements[0];
            }
        }

        public static Beat GetLastBeat(this Beam beam)
        {
            while (true)
            {
                if (beam.Elements.Count == 0)
                    return null;
                var beat = beam.Elements[beam.Elements.Count - 1] as Beat;
                if (beat != null) return beat;
                beam = (Beam)beam.Elements[beam.Elements.Count - 1];
            }
        }
    }
}
