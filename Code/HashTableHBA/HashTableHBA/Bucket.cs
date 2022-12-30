using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HashTableHBA
{
    public class Bucket<Key,Value>
    {
        //Key key;
        public Key key { get; set; }
        public Value value { get; set; }

        public Bucket<Key, Value> nextBucket { get; set; }
        

        public Bucket(Key _key, Value _value)
        {
            key = _key;
            value = _value;
        }
       
    }
}

   
