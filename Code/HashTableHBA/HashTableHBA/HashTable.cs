using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HashTableHBA
{
    public class HashTable<Key, Value> : IHashTable<Key, Value>
    {
        private LinkedList<Bucket<Key, Value>>[] buckets;
        private int count;

        public int Capacity { get; set; }

        public HashTable(int capacity)
        {
            Capacity = capacity;
            buckets = new LinkedList<Bucket<Key, Value>>[capacity];
            count = 0;
        }
        public int Count()
        {
            return count;
        }

        public bool Delete(Key key)
        {
            int bucketIndex = HashFunction.Hash(key, Capacity);
            LinkedList<Bucket<Key, Value>> bucket = buckets[bucketIndex];
            if (bucket != null)
            {
                foreach (Bucket<Key, Value> pair in bucket)
                {
                    if (pair.key.Equals(key))
                    {
                        bucket.Remove(pair);
                        count--;
                        return true;
                    }
                }
            }
            return false;
        }

        public double GetLoadFactor()
        {
           return (double)count / Capacity;
        }

        public bool Insert(Key key, Value value)
        {
            if(GetLoadFactor() >= 0.9)
            {
                Console.WriteLine("Rehash!");
            }

            // Do a check
            if (key == null)
                throw new ArgumentNullException("key");

            int bucketIndex = HashFunction.Hash(key, Capacity);
            
            LinkedList <Bucket<Key, Value >> bucket = buckets[bucketIndex];

            if (bucket == null)
            {
                bucket = new LinkedList<Bucket<Key, Value>>();
                buckets[bucketIndex] = bucket;
            }
            else
            {
                foreach(Bucket<Key, Value> pair in bucket)
                {
                    if (pair.key.Equals(key))
                    {         
                        return false;
                    }
                }           
            }
            bucket.AddLast(new Bucket<Key, Value>(key, value));
            count++;
            return true;
        }

        public Value Search(Key key)
        {
            int bucketIndex = HashFunction.Hash(key, Capacity);
            LinkedList<Bucket<Key, Value>> bucket = buckets[bucketIndex];
            if(bucket != null)
            {
                foreach (Bucket<Key, Value> pair in bucket)
                {
                    if (pair.key.Equals(key))
                    {
                        return pair.value;
                    }
                }
            }
            throw new KeyNotFoundException("Key not found: " + key.ToString());
        }

        public bool Update(Key key, Value newValue)
        {
            int bucketIndex = HashFunction.Hash(key, Capacity);
            LinkedList<Bucket<Key, Value>> bucket = buckets[bucketIndex];

            if(bucket != null)
            {
                foreach (Bucket<Key, Value> pair in bucket)
                {
                    if (pair.key.Equals(key))
                    {
                        pair.value = newValue;
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
