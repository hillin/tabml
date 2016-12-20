using System.Collections.Generic;
using System.Linq;
using TabML.Core.Document;
using TabML.Editor.Tablature.Layout;

namespace TabML.Editor.Rendering
{
    internal class BeamRenderer : BeatElementRenderer<Beam>
    {

        private BarRenderer _ownerBar;
        public BarRenderer OwnerBar => _ownerBar ?? (_ownerBar = BeatElementRenderer.FindOwnerBarRenderer(this));

        public Beam Beam { get; }

        private readonly List<IBeatElementRenderer> _beatElementRenderers;

        public BeamRenderer(ElementRenderer owner, Beam beam)
            : base(owner, beam)
        {
            this.Beam = beam;
            _beatElementRenderers = new List<IBeatElementRenderer>();
        }

        public override void Initialize()
        {
            base.Initialize();

            _beatElementRenderers.AddRange(this.Beam.Elements.Select(e => BeatElementRenderer.Create(this, e)));
            _beatElementRenderers.ForEach(e => e.Initialize());
        }

        protected override void OnAssignRenderingContext(BarRenderingContext renderingContext)
        {
            base.OnAssignRenderingContext(renderingContext);
            _beatElementRenderers.AssignRenderingContexts(renderingContext);
        }

        public override void Render(BeamSlope beamSlope)
        {
            var firstBeat = this.Beam.GetFirstBeat();
            var lastBeat = this.Beam.GetLastBeat();
            var x0 = this.RenderingContext.ColumnRenderingInfos[firstBeat.OwnerColumn.ColumnIndex].Position
                + firstBeat.GetAlternationOffset(this.RenderingContext);
            var x1 = this.RenderingContext.ColumnRenderingInfos[lastBeat.OwnerColumn.ColumnIndex].Position
                + lastBeat.GetAlternationOffset(this.RenderingContext);

            if (beamSlope == null)
            {
                var y0 = firstBeat.GetStemTailPosition(this.RenderingContext);
                var y1 = lastBeat.GetStemTailPosition(this.RenderingContext);
                beamSlope = new BeamSlope(x0, y0, (y1 - y0) / (x1 - x0));
            }

            _beatElementRenderers.ForEach(r => r.Render(beamSlope));

            this.RenderingContext.DrawBeam(this.Beam.BeatNoteValue, x0, beamSlope.GetY(x0), x1, beamSlope.GetY(x1), this.Beam.VoicePart);
            if (this.Beam.Tuplet != null
                && this.Beam.Elements.Any(e => (e as Beam)?.Tuplet != this.Beam.Tuplet))
            {
                var tupletX = (x1 + x0) / 2;
                this.RenderingContext.DrawTuplet(this.Beam.Tuplet.Value, tupletX, beamSlope.GetY(tupletX), this.Beam.VoicePart);
            }
        }

    }
}