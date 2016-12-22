using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabML.Core.Document;
using TabML.Core.MusicTheory;

namespace TabML.Editor.Rendering
{
    static class TiePositionExtensions
    {
        public static OffBarDirection ToOffBarDirection(this TiePosition tiePosition)
        {
            switch (tiePosition)
            {
                case TiePosition.Above:
                    return OffBarDirection.Top;
                case TiePosition.Under:
                    return OffBarDirection.Bottom;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tiePosition), tiePosition, null);
            }
        }
    }
}
