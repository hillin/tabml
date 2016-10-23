using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.MusicTheory
{
    public static class PreciseDurationExtensions
    {
        public static PreciseDuration Sum(this IEnumerable<PreciseDuration> durations)
        {
            return new PreciseDuration(durations.Sum(duration => duration.FixedPointValue));
        }

        public static PreciseDuration Sum<T>(this IEnumerable<T> items, Func<T, PreciseDuration> selector)
        {
            return new PreciseDuration(items.Sum(item => selector(item).FixedPointValue));
        }

        public static PreciseDuration Min(this IEnumerable<PreciseDuration> durations)
        {
            return new PreciseDuration(durations.Min(duration => duration.FixedPointValue));
        }

        public static PreciseDuration Min<T>(this IEnumerable<T> items, Func<T, PreciseDuration> selector)
        {
            return new PreciseDuration(items.Min(item => selector(item).FixedPointValue));
        }

        public static PreciseDuration Max(this IEnumerable<PreciseDuration> durations)
        {
            return new PreciseDuration(durations.Max(duration => duration.FixedPointValue));
        }

        public static PreciseDuration Max<T>(this IEnumerable<T> items, Func<T, PreciseDuration> selector)
        {
            return new PreciseDuration(items.Max(item => selector(item).FixedPointValue));
        }
    }
}
