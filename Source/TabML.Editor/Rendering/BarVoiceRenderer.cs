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

        private readonly List<IBeatElementRenderer> _beatElementRenderers;

        public BarVoiceRenderer(BarRenderer owner, Voice voice)
            : base(owner, voice)
        {
            this.Voice = voice;
            _beatElementRenderers = new List<IBeatElementRenderer>();
        }

        public override void Initialize()
        {
            base.Initialize();

            _beatElementRenderers.AddRange(this.Voice.BeatElements.Select(e => BeatElementRenderer.Create(this, e)));
            _beatElementRenderers.ForEach(e => e.Initialize());
        }

        protected override void OnAssignRenderingContext(BarRenderingContext renderingContext)
        {
            base.OnAssignRenderingContext(renderingContext);
            _beatElementRenderers.AssignRenderingContexts(renderingContext);
        }

        public async Task Render()
        {
            foreach (var renderer in _beatElementRenderers)
                await renderer.Render();
        }

    }
}
