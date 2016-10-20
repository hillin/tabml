using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.MusicTheory
{
    [DebuggerDisplay("{Duration}")]
    public struct PreciseDuration : IEquatable<PreciseDuration>, IComparable<PreciseDuration>, IEquatable<int>, IEquatable<double>
    {
        public const long Precision = 100000000L;

        public static readonly PreciseDuration Zero = new PreciseDuration(0L);

        public long FixedPointValue { get; }
        public double Duration => (double)this.FixedPointValue / Precision;

        public PreciseDuration(long fixedPointValue)
        {
            this.FixedPointValue = fixedPointValue;
        }

        public PreciseDuration(double duration)
        {
            this.FixedPointValue = (long)(duration * Precision);
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
            unchecked
            {
                return (int)(this.FixedPointValue - other.FixedPointValue);
            }
        }

        public bool Equals(int other)
        {
            return this.FixedPointValue == other * Precision;
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
            return d1.FixedPointValue < i * Precision;
        }

        public static bool operator >(PreciseDuration d1, int i)
        {
            return d1.FixedPointValue > i * Precision;
        }

        public bool Equals(double other)
        {
            return this.FixedPointValue == (long)(other * Precision);
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
            return d1.FixedPointValue < i * Precision;
        }

        public static bool operator >(PreciseDuration d1, double i)
        {
            return d1.FixedPointValue > i * Precision;
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
    }
}
