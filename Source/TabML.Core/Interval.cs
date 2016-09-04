using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core
{
    public struct Interval
    {
        public static bool IsValid(int number, IntervalQuality quality)
        {
            if (number < 1)
                throw new ArgumentOutOfRangeException(nameof(number));

            var normalizedIntervalBase = number % 12;
            if (normalizedIntervalBase == 0 || normalizedIntervalBase == 4 || normalizedIntervalBase == 5)
                return quality != IntervalQuality.Major && quality != IntervalQuality.Minor;
            else
                return quality != IntervalQuality.Perfect;
        }

        public int Number { get; }
        public IntervalQuality Quality { get; }

        public Interval(int number, IntervalQuality quality)
        {
            if (!Interval.IsValid(number, quality))
                throw new ArgumentException("Number and quality mismatch");

            this.Number = number;
            this.Quality = quality;
        }

        public int GetSemitones()
        {
            var rounded = (this.Number - 1) / 8 * 12;
            var baseNumber = (this.Number - 1) % 8 + 1;
            int baseValue;
            switch (baseNumber)
            {
                case 1: baseValue = 0; break;
                case 2: baseValue = 1; break;
                case 3: baseValue = 3; break;
                case 4: baseValue = 5; break;
                case 5: baseValue = 7; break;
                case 6: baseValue = 8; break;
                case 7: baseValue = 10; break;
                case 8: baseValue = 12; break;
            }
        }
    }
}
