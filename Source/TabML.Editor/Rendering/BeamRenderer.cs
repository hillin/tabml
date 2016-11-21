using System.Linq;
using TabML.Core.Document;
using TabML.Editor.Tablature.Layout;

namespace TabML.Editor.Rendering
{
    internal class BeamRenderer
    {
        public void Render(BarDrawingContext drawingContext, Beam beam, BeamSlope beamSlope)
        {
            var firstBeat = beam.GetFirstBeat();
            var lastBeat = beam.GetLastBeat();
            var x0 = drawingContext.ColumnRenderingInfos[firstBeat.OwnerColumn.ColumnIndex].Position
                + firstBeat.GetAlternationOffset(drawingContext);
            var x1 = drawingContext.ColumnRenderingInfos[lastBeat.OwnerColumn.ColumnIndex].Position
                + lastBeat.GetAlternationOffset(drawingContext);

            if (beamSlope == null)
            {
                var y0 = firstBeat.GetStemTailPosition(drawingContext);
                var y1 = lastBeat.GetStemTailPosition(drawingContext);
                beamSlope = new BeamSlope(x0, y0, (y1 - y0)/(x1 - x0));
            }

            foreach (var element in beam.Elements)
            {
                new BeatElementRenderer().Render(drawingContext, element, beamSlope);
            }
            
            drawingContext.DrawBeam(beam.BeatNoteValue, x0, beamSlope.GetY(x0), x1, beamSlope.GetY(x1), beam.VoicePart);
            if (beam.Tuplet != null
                && beam.Elements.Any(e => (e as Beam)?.Tuplet != beam.Tuplet))
            {
                var tupletX = (x1 + x0) / 2;
                drawingContext.DrawTuplet(beam.Tuplet.Value, tupletX, beamSlope.GetY(tupletX), beam.VoicePart);
            }
        }
    }
}