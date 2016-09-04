using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TabML.Core.AbsoluteNoteNames;

namespace TabML.Core
{
    public static class Tunings
    {
        public static Tuning Standard = new Tuning(E(2), A(2), D(3), G(3), B(3), E(4));
        public static Tuning DropD = new Tuning(D(2), A(2), D(3), G(3), B(3), E(4));
    }
}
