using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableHBA
{
    internal static class HashFunction
    {
        internal static int Hash(object key, int N)
        {
            int h1 = key.GetHashCode();
            int h2 = (h1 & 0x7FFFFFFF) % N;

            return h2;
        }
    }
}

