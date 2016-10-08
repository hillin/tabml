using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser.AST
{
    interface IValueEquatable<in T>
    {
        bool ValueEquals(T other);
    }

    static class ValueEquatable
    {
        public static bool ValueEquals<T>(IValueEquatable<T> v1, IValueEquatable<T> v2)
        {
            if (v1 == null && v2 == null)
                return true;

            if (v1 != null && v2 == null)
                return false;

            if (v1 == null /*&& v2 != null*/)
                return false;

            return v1.ValueEquals((T)v2);
        }
    }
}
