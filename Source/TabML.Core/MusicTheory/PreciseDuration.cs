using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.MusicTheory
{
    [DebuggerDisplay("{Duration}")]
    public struct PreciseDuration : IEquatable<PreciseDuration>, IComparable<PreciseDuration>, IEquatable<int>, IEquatable<double>
    {
        public const long Unit = 256L * 3 * 5 * 7 * 11 * 13 * 17 * 19 * 23 * 29;

        public static readonly PreciseDuration Zero = new PreciseDuration(0L);

        public long FixedPointValue { get; }
        public double Duration => (double)this.FixedPointValue / Unit;

        public PreciseDuration(long fixedPointValue)
        {
            this.FixedPointValue = fixedPointValue;
        }

        public PreciseDuration(double duration)
        {
            this.FixedPointValue = (long)(duration * Unit);
        }

        public static PreciseDuration operator +(PreciseDuration d1, PreciseDuration d2)
        {
            return new PreciseDuration(d1.FixedPointValue + d2.FixedPointValue);
        }

        public static PreciseDuration operator -(PreciseDuration d1, PreciseDuration d2)
        {
            return new PreciseDuration(d1.FixedPointValue - d2.FixedPointValue);
        }

        public static PreciseDuration operator *(PreciseDuration d, double multiplier)
        {
            return new PreciseDuration((long)(d.FixedPointValue * multiplier));
        }

        public static double operator /(PreciseDuration d1, PreciseDuration d2)
        {
            return (double)d1.FixedPointValue / d2.FixedPointValue;
        }


        public static bool operator ==(PreciseDuration d1, PreciseDuration d2)
        {
            return d1.FixedPointValue == d2.FixedPointValue;
        }

        public static bool operator !=(PreciseDuration d1, PreciseDuration d2)
        {
            return d1.FixedPointValue != d2.FixedPointValue;
        }

        public static bool operator <(PreciseDuration d1, PreciseDuration d2)
        {
            return d1.FixedPointValue < d2.FixedPointValue;
        }

        public static bool operator >(PreciseDuration d1, PreciseDuration d2)
        {
            return d1.FixedPointValue > d2.FixedPointValue;
        }

        public static bool operator <=(PreciseDuration d1, PreciseDuration d2)
        {
            return d1.FixedPointValue <= d2.FixedPointValue;
        }

        public static bool operator >=(PreciseDuration d1, PreciseDuration d2)
        {
            return d1.FixedPointValue >= d2.FixedPointValue;
        }

        public bool Equals(PreciseDuration other)
        {
            return other.FixedPointValue == this.FixedPointValue;
        }

        public int CompareTo(PreciseDuration other)
        {
            return this.FixedPointValue.CompareTo(other.FixedPointValue);
        }

        public bool Equals(int other)
        {
            return this.FixedPointValue == other * Unit;
        }

        public static bool operator ==(PreciseDuration d, int i)
        {
            return d.Equals(i);
        }

        public static bool operator !=(PreciseDuration d, int i)
        {
            return !d.Equals(i);
        }

        public static bool operator <(PreciseDuration d1, int i)
        {
            return d1.FixedPointValue < i * Unit;
        }

        public static bool operator >(PreciseDuration d1, int i)
        {
            return d1.FixedPointValue > i * Unit;
        }

        public bool Equals(double other)
        {
            return this.FixedPointValue == (long)(other * Unit);
        }


        public static bool operator ==(PreciseDuration d, double i)
        {
            return d.Equals(i);
        }

        public static bool operator !=(PreciseDuration d, double i)
        {
            return !d.Equals(i);
        }

        public static bool operator <(PreciseDuration d1, double i)
        {
            return d1.FixedPointValue < i * Unit;
        }

        public static bool operator >(PreciseDuration d1, double i)
        {
            return d1.FixedPointValue > i * Unit;
        }

        public static implicit operator double(PreciseDuration d)
        {
            return d.Duration;
        }

        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            return obj is PreciseDuration && this.Equals((PreciseDuration)obj);
        }

        public override int GetHashCode()
        {
            return this.FixedPointValue.GetHashCode();
        }

        public override string ToString()
        {
            return this.Duration.ToString(CultureInfo.CurrentCulture);
        }
    }
}
