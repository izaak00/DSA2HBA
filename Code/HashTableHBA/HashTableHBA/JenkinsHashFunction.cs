using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.HashFunction.Jenkins;

namespace HashTableHBA
{
    internal static class JenkinsHashFunction
    {
        internal static uint JenkinsHashWithGetHashCode(object key, int N)
        {
            int hashCode = key.GetHashCode();
            int h2 = (hashCode & 0x7FFFFFFF) % N;

            // Convert the hash code to a byte array
            byte[] data = BitConverter.GetBytes(h2);

            uint hash = 0;
            int length = data.Length;

            for (int i = 0; i < length; i++)
            {
                hash += data[i];
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }

            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);

            return hash;

            // Use the Jenkins hash function to hash the byte array
            //uint hash = JenkinsHash(data);

            //return hash;
        }
    }
}
