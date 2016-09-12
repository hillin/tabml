using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Core.MusicTheory
{
    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class KnownTuningAttribute : Attribute
    {
    }
}
