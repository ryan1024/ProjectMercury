using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercury.Backoffice.Helpers
{
    public static class StringHelper
    {
        public static string ConvertListToString<T>(this IEnumerable<T> l, string separator)
        {
            return "[" + String.Join(separator, l.Select(i => i.ToString()).ToArray()) + "]";
        }
    }
}