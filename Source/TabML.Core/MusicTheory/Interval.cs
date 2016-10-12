using System;

namespace TabML.Core.MusicTheory
{
    public struct Interval : IEquatable<Interval>
    {
        
        /// <remarks>interval number is zero based, thus a second interval has a number of 1</remarks>
        public static bool IsValid(int number, IntervalQuality quality)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException(nameof(number));

            var normalizedNumber = number  % 7;
            if (normalizedNumber == 0 || normalizedNumber == 3 || normalizedNumber == 4)
                return quality != IntervalQuality.Major && quality != IntervalQuality.Minor;
            else
                return quality != IntervalQuality.Perfect;
        }

        public int Number { get; }
        public IntervalQuality Quality { get; }

        public int Octaves => this.Number / 7;
        public int NormalizedNumber => this.Number % 7;

        public bool CouldBePerfect
        {
            get
            {
                var normalizedNumber = this.NormalizedNumber;
                return normalizedNumber == 0 || normalizedNumber == 3 || normalizedNumber == 4;
            }
        }

        /// <remarks>interval number is zero based, thus a second interval has a number of 1</remarks>
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
                case IntervalQuality.Diminished:
                    baseValue -= 1; break;
            }

            return this.Octaves * 12 + baseValue;
        }

        public static NoteName operator +(NoteName noteName, Interval interval)
        {
            if (noteName.Accidental.GetIsDoubleAccidental())
                throw new ArgumentException(
                    "NoteName + Interval operator is not available to note names with double accidental",
                    nameof(noteName));
            var semitones = noteName.GetSemitones() + interval.GetSemitoneOffset();
            var degrees = noteName.BaseName.GetAbsoluteDegrees() + interval.NormalizedNumber;
            return NoteName.FromSemitones(semitones, degrees);
        }

        public bool Equals(Interval other)
        {
            return this.Number == other.Number && this.Quality == other.Quality;
        }

        public static bool operator ==(Interval i1, Interval i2)
        {
            return i1.Equals(i2);
        }

        public static bool operator !=(Interval i1, Interval i2)
        {
            return !i1.Equals(i2);
        }

        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            return obj is Interval && this.Equals((Interval)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Number * 397) ^ (int)this.Quality;
            }
        }
    }
}
