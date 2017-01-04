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
        public static double GetPosition(this BarColumn column, BarRenderingContext rc)
        {
            return rc.ColumnRenderingInfos[column.ColumnIndex].Position;
        }

        public static double GetPositionInRow(this BarColumn column, BarRenderingContext rc)
        {
            return rc.Owner.GetRelativeX(rc.ColumnRenderingInfos[column.ColumnIndex].Position + rc.Location.X);
        }
    }
}
