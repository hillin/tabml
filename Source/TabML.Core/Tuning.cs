using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public class Tuning
    {
        public Pitch[] StringTunings { get; }

        public Tuning(params Pitch[] stringTunings)
        {
            StringTunings = stringTunings;
        }
    }
}
