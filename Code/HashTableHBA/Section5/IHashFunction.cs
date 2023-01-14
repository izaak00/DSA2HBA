using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableHBA
{  public interface IHashFunction
    {
        /// <summary>
        /// Apply the hashfunction to the object o passed as a parameter.  Uses the GetHashCode method from the object o.
        /// </summary>
        /// <param name="o">The object to be hashed</param>
        /// <returns>The result of the hash function</returns>
        int Hash(object o);
    }
}
