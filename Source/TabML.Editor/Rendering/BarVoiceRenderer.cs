using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    class BarVoiceRenderer
    {
        public void Render(BarDrawingContext drawingContext, Voice voice)
        {
            foreach (var beatElement in voice.BeatElements)
            {
                new BeatElementRenderer().Render(drawingContext, beatElement, null);
            }
        }
    }
}
