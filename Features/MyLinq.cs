using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Explicitly different namespace
namespace Features.Linq
{
    public static class MyLinq
    {
        // Extension method to see how Linq is build
        public static int Count<T>(this IEnumerable<T> sequence)
        {
            int count = 0;
            foreach (var item in sequence)
            {
                count += 1;
            }
            return count;
        }
    }
}
