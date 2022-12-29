using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HashTableHBA
{
    internal class Bucket<Key,Value>
    {
        //Key key;
        public Key key { get; set; }
        public Value value { get; set; }

        public Bucket<Key, Value> nextBucket { get; set; }
        

        public Bucket(Key Key, Value Value)
        {
            key = Key;
            value = Value;
        }
       
    }
}

   
