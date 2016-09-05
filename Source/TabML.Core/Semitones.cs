using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public static class Semitones
    {


        public static bool TrySnapToDegree(this int semitones, int degree, out BaseNoteName noteName)
        {
            semitones = semitones % 12;
            degree = degree % 7;

        }
    }
}
