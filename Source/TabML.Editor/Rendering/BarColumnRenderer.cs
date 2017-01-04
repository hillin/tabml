using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    class BarColumnRenderer : ElementRenderer<BarColumn, BarRenderingContext>
    {


        public BarColumnRenderer(ElementRenderer owner, BarColumn element)
            : base(owner, element)
        {
        }

        public void PreRender()
        {
            //todo: draw chord and lyrics
        }

        public async Task PostRender()
        {
            if (this.Element.Lyrics != null)
            {
                var columnPosition = this.RenderingContext.ColumnRenderingInfos[this.Element.ColumnIndex].Position;
                await this.RenderingContext.DrawLyrics(columnPosition, this.Element.Lyrics.Text);
            }
        }
    }
}
