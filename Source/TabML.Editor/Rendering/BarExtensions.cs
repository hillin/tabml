using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;

namespace TabML.Editor.Rendering
{
    static class BarExtensions
    {
        public static double GetMinWidth(this Bar bar, TablatureStyle style)
        {
            var minDuration = Enumerable.Min(bar.Columns, c => c.GetDuration());

            return bar.Columns.Sum(column => bar.GetColumnMinWidthInBar(column, style, minDuration));
        }

        public static double GetColumnMinWidthInBar(this Bar bar, BarColumn column, TablatureStyle style,
                                                    PreciseDuration minDurationInBar)
        {
            var columnRegularWidth = Math.Min(style.MaximumBeatSizeWithoutLyrics,
                                              style.MinimumBeatSize*column.GetDuration()/minDurationInBar);
            var columnMinWidth = column.GetMinWidth(style);

            return Math.Max(columnRegularWidth, columnMinWidth);
        }
    }
}
