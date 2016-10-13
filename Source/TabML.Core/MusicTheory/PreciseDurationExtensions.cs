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
            long sum = 0;
            foreach (var duration in durations)
                sum += duration.FixedPointValue;

            return new PreciseDuration(sum);
        }

        public static PreciseDuration Sum<T>(this IEnumerable<T> items, Func<T, PreciseDuration> selector)
        {
            long sum = 0;
            foreach (var item in items)
                sum += selector(item).FixedPointValue;

            return new PreciseDuration(sum);
        }
    }
}
