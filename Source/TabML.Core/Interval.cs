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

            var normalizedNumber = (number - 1) % 7;
            if (normalizedNumber == 0 || normalizedNumber == 3 || normalizedNumber == 4)
                return quality != IntervalQuality.Major && quality != IntervalQuality.Minor;
            else
                return quality != IntervalQuality.Perfect;
        }

        public int Number { get; }
        public IntervalQuality Quality { get; }

        public int Octaves => (this.Number - 1) / 7;
        public int NormalizedNumber => (this.Number - 1) % 7;

        public bool CouldBePerfect
        {
            get
            {
                var normalizedNumber = this.NormalizedNumber;
                return normalizedNumber == 0 || normalizedNumber == 3 || normalizedNumber == 4;
            }
        }

        public Interval(int number, IntervalQuality quality)
        {
            if (!Interval.IsValid(number, quality))
                throw new ArgumentException("Number and quality mismatch");

            this.Number = number;
            this.Quality = quality;
        }

        public int GetSemitoneOffset()
        {
            int baseValue;
            var normalizedNumber = this.NormalizedNumber;
            switch (normalizedNumber)
            {
                case 0: baseValue = 0; break;
                case 1: baseValue = 1; break;
                case 2: baseValue = 3; break;
                case 3: baseValue = 5; break;
                case 4: baseValue = 7; break;
                case 5: baseValue = 8; break;
                case 6: baseValue = 10; break;
                default: throw new NotImplementedException();
            }

            switch (this.Quality)
            {
                case IntervalQuality.Major:
                    baseValue += 1; break;
                case IntervalQuality.Augmented:
                    if (this.CouldBePerfect)
                        baseValue += 1;
                    else
                        baseValue += 2;
                    break;
                case IntervalQuality.Dimished:
                    baseValue -= 1; break;
            }

            return this.Octaves * 12 + baseValue;
        }

        public static NoteName operator +(NoteName noteName, Interval interval)
        {
            var degrees = noteName.GetAbsoluteDegree() + interval.NormalizedNumber;
            return NoteNames.FromAbsoluteDegree(degrees, noteName.GetAccidental());
        }
    }
}
