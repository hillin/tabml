﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public static class BaseNoteValueExtensions
    {
        public static double GetDuration(this BaseNoteValue value)
        {
            return Math.Pow(2, (int)value);
        }

        public static int GetInvertedDuration(this BaseNoteValue value)
        {
            var intValue = (int)value;
            if (intValue > 0)
                throw new ArgumentOutOfRangeException(nameof(value), "only whole or shorter values are supported");

            return (int)Math.Pow(2, -intValue);
        }
    }
}
