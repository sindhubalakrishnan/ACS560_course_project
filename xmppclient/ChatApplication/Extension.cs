using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication
{
    internal static class Extension
    {
        internal static void ThrowIfNull<T>(this T data, string name)
          where T : class
        {
            if (data == null)
                throw new ArgumentNullException(name);
        }
    }
}
