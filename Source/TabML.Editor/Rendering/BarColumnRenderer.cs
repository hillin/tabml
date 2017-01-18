using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;

namespace TabML.Editor.Rendering
{
    class BarColumnRenderer : ElementRenderer<BarColumn, BarRenderingContext>
    {

        public BarColumnRenderer(ElementRenderer owner, BarColumn element)
            : base(owner, element)
        {
        }

        public async Task PreRender()
        {
            var columnInfo = this.RenderingContext.ColumnRenderingInfos[this.Element.ColumnIndex];
            if (columnInfo.HasBrushlikeTechnique)
            {
                var technique = this.Element.VoiceBeats[0].StrumTechnique;
                int minString = int.MaxValue, maxString = int.MinValue;
                foreach (var note in this.Element.VoiceBeats.SelectMany(b => b.Notes))
                {
                    if (note.IsTied)
                        continue;

                    if (note.String < minString)
                        minString = note.String;
                    if (note.String > maxString)
                        maxString = note.String;
                }

                if (minString == int.MaxValue || maxString == int.MinValue)
                    return;

                double size;

                switch (technique)
                {
                    case StrumTechnique.BrushDown:
                        size = await this.RenderingContext.DrawInlineBrushDown(columnInfo.Position, minString, maxString);
                        break;
                    case StrumTechnique.BrushUp:
                        size = await this.RenderingContext.DrawInlineBrushUp(columnInfo.Position, minString, maxString);
                        break;
                    case StrumTechnique.ArpeggioDown:
                        size = await this.RenderingContext.DrawInlineArpeggioDown(columnInfo.Position, minString, maxString);
                        break;
                    case StrumTechnique.ArpeggioUp:
                        size = await this.RenderingContext.DrawInlineArpeggioUp(columnInfo.Position, minString, maxString);
                        break;
                    case StrumTechnique.Rasgueado:
                        size = await this.RenderingContext.DrawInlineRasgueado(columnInfo.Position, minString, maxString);
                        break;
                    default:
                        throw new NotSupportedException();
                }

                columnInfo.BrushlikeTechniqueSize = size;
            }
        }

        public async Task PostRender()
        {

            if (this.Element.Lyrics != null)
            {
                var columnPosition = this.RenderingContext.ColumnRenderingInfos[this.Element.ColumnIndex].Position;
                await this.RenderingContext.DrawLyrics(columnPosition, this.Element.Lyrics.Text);
            }
        }


        public async Task Render()
        {
            
        }
    }
}
