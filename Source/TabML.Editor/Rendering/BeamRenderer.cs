using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Editor.Tablature.Layout;

namespace TabML.Editor.Rendering
{
    internal class BeamRenderer : BeatElementRenderer<Beam>
    {

        private BarRenderer _ownerBar;
        public BarRenderer OwnerBar => _ownerBar ?? (_ownerBar = BeatElementRenderer.FindOwnerBarRenderer(this));

        private readonly List<IBeatElementRenderer> _beatElementRenderers;

        public BeamRenderer(ElementRenderer owner, Beam beam)
            : base(owner, beam)
        {
            _beatElementRenderers = new List<IBeatElementRenderer>();
        }

        public override void Initialize()
        {
            base.Initialize();

            _beatElementRenderers.AddRange(this.Element.Elements.Select(e => BeatElementRenderer.Create(this, e)));
            _beatElementRenderers.ForEach(e => e.Initialize());
        }

        protected override void OnAssignRenderingContext(BarRenderingContext renderingContext)
        {
            base.OnAssignRenderingContext(renderingContext);
            _beatElementRenderers.AssignRenderingContexts(renderingContext);
        }

        public override async Task Render()
        {
            var firstBeat = this.Element.GetFirstBeat();
            var lastBeat = this.Element.GetLastBeat();
            var x0 = this.RenderingContext.ColumnRenderingInfos[firstBeat.OwnerColumn.ColumnIndex].Position
                + firstBeat.GetAlternationOffset(this.RenderingContext);
            var x1 = this.RenderingContext.ColumnRenderingInfos[lastBeat.OwnerColumn.ColumnIndex].Position
                + lastBeat.GetAlternationOffset(this.RenderingContext);

            var beamSlope = this.RenderingContext.GetBeamSlope(this.Element);

            if (beamSlope == null)
            {
                var beamOffset = await this.CalculateBeamOffset();

                var y0 = firstBeat.GetStemTailPosition(this.RenderingContext) + beamOffset;
                var y1 = lastBeat.GetStemTailPosition(this.RenderingContext) + beamOffset;
                beamSlope = new BeamSlope(x0, y0, (y1 - y0) / (x1 - x0));
                this.RenderingContext.SetBeamSlope(this.Element, beamSlope);
            }

            // beam must be rendered first, so the height map will be updated correctly
            await this.RenderingContext.DrawBeam(this.Element.BeatNoteValue, x0, beamSlope.GetY(x0), x1, beamSlope.GetY(x1), this.Element.GetStemRenderVoicePart());

            foreach (var renderer in _beatElementRenderers)
                await renderer.Render();

            if (this.Element.Tuplet != null)
            {
                if (this.Element.IsRoot || this.Element.RootBeam.Tuplet != this.Element.Tuplet)
                {
                    var tupletX = (x1 + x0) / 2;
                    await this.RenderingContext.DrawTuplet(this.Element.Tuplet.Value, tupletX, this.Element.GetStemRenderVoicePart());
                }
            }
        }

        private async Task<double> CalculateBeamOffset()
        {
            var shortestRest = (Beat)null;
            foreach (var restBeat in _beatElementRenderers.OfType<BeatRenderer>().Select(b => b.Element).Where(b => b.IsRest))
            {
                if (shortestRest == null || shortestRest.NoteValue.Base > restBeat.NoteValue.Base)
                    shortestRest = restBeat;
            }

            var beamOffset = 0.0;
            if (shortestRest != null)
            {
                var restBounds = await this.RenderingContext.MeasureRest(shortestRest.NoteValue.Base);
                var halfBarSpaceHeight = this.RenderingContext.Style.BarLineHeight / 2;
                beamOffset = this.Element.GetStemRenderVoicePart() == VoicePart.Treble
                    ? restBounds.Top - halfBarSpaceHeight
                    : restBounds.Bottom + halfBarSpaceHeight;
            }
            return beamOffset;
        }
    }
}