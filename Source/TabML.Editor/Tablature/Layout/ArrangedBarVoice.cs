using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Tablature.Layout
{
    class ArrangedBarVoice
    {
        public List<IBeatElement> BeatElements { get; }
        public VoicePart VoicePart { get; }

        public ArrangedBarVoice(VoicePart voicePart)
        {
            this.VoicePart = voicePart;
            this.BeatElements = new List<IBeatElement>();
        }

        public void Draw(IBarDrawingContext drawingContext, double[] columnPositions)
        {
            foreach (var beatElement in this.BeatElements)
            {
                beatElement.Draw(drawingContext, columnPositions, null);
            }
        }
    }
}
