using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    class BarVoiceRenderer : ElementRenderer<Voice, BarRenderingContext>
    {
        public Voice Voice { get; }

        public BarVoiceRenderer(BarRenderer owner, Voice voice)
            : base(owner, voice)
        {
            this.Voice = voice;
        }

        public void Render()
        {
            foreach (var beatElement in this.Voice.BeatElements)
            {
                BeatElementRenderer.Render(this, this.RenderingContext, beatElement, null);
            }
        }

    }
}
