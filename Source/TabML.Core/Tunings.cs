using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TabML.Core.Pitch;

namespace TabML.Core
{
    public static class Tunings
    {
        public static Tuning Standard = new Tuning(Pitches.E(2), Pitches.A(2), Pitches.D(3), Pitches.G(3), Pitches.B(3), Pitches.E(4));
        public static Tuning DropD = new Tuning(Pitches.D(2), Pitches.A(2), Pitches.D(3), Pitches.G(3), Pitches.B(3), Pitches.E(4));
    }
}
