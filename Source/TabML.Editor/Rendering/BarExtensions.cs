using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;

namespace TabML.Editor.Rendering
{
    static class BarExtensions
    {
        public static double GetMinWidth(this Bar bar, TablatureStyle style)
        {
            switch (style.BeatLayout)
            {
                case BeatLayout.SizeByNoteValue:
                    if (style.FlexibleBeatSize)
                        throw new NotImplementedException(); //todo
                    else
                    {
                        // size is equally and explicitly divided by NoteValue
                        var minWidth = 0.0;
                        foreach (var column in bar.Columns)
                        {
                            var columnMinWidth = column.GetMinWidth(style);
                            minWidth = Math.Max(minWidth,
                                                columnMinWidth / column.GetDuration() * bar.Duration);
                        }

                        return minWidth;
                    }
                case BeatLayout.DivideInBeats:
                    throw new NotImplementedException();    //todo
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
