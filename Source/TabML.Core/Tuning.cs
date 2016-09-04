using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public class Tuning
    {
        public AbsoluteNoteName[] StringTunings { get; }

        public Tuning(params AbsoluteNoteName[] stringTunings)
        {
            StringTunings = stringTunings;
        }
    }
}
