using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;
using TabML.Core.Style;

namespace TabML.Editor.Rendering
{
    static class TiePositionExtensions
    {
        public static VerticalDirection ToOffBarDirection(this Core.Style.VerticalDirection tiePosition)
        {
            switch (tiePosition)
            {
                case Core.Style.VerticalDirection.Above:
                    return VerticalDirection.Above;
                case Core.Style.VerticalDirection.Under:
                    return VerticalDirection.Under;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tiePosition), tiePosition, null);
            }
        }
    }
}
