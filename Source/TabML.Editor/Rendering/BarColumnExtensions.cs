using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    static class BarColumnExtensions
    {
        public static double GetMinWidth(this BarColumn column, TablatureStyle style)
        {
            return Math.Max(style.MinimumBeatSize,
                            column.Lyrics == null ? 0.0 : style.MakeFormattedLyrics(column.Lyrics.Text).Width);
        }

        public static double GetPosition(this BarColumn column, BarDrawingContext drawingContext)
        {
            return drawingContext.ColumnRenderingInfos[column.ColumnIndex].Position;
        }
    }
}
