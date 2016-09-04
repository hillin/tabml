﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public static class AccidentalExtensions
    {
        public static int GetTET12Offset(this Accidental accidental)
        {
            switch (accidental)
            {
                case Accidental.Natural:
                    return 0;
                case Accidental.Sharp:
                    return 1;
                case Accidental.Flat:
                    return -1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(accidental), accidental, null);
            }
        }

    }
}