using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HashTableHBA
{
    public class HashTable<Key, Value> : IHashTable<Key, Value>
    {
        private LinkedList<Bucket<Key, Value>>[] KeyValuePair;
        private int count;
        public int Capacity { get; set; }
        public IHashFunctionProvider HashFunctionPovider;
        public IHashFunction HashFunction;
        public HashTable(int capacity, IHashFunctionProvider provider)
        {
            Capacity = capacity;
            HashFunctionPovider = provider;
            HashFunction = HashFunctionPovider.GetHashFunction(Capacity);
            KeyValuePair = new LinkedList<Bucket<Key, Value>>[capacity];
            count = 0;
        }
        public int Count()
        {
            return count;
        }

        public bool Delete(Key key)
        {
            //int bucketIndex = HashFunction.Hash(key, Capacity);
            int bucketIndex = CarterHashFunction(key);
            LinkedList<Bucket<Key, Value>> bucket = KeyValuePair[bucketIndex];
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

        public int CarterHashFunction(Key key)
        {    
            return HashFunction.Hash(key);      
        }

        public bool Insert(Key key, Value value)
        {
            if(GetLoadFactor() >= 0.9)
            {
                rehash();
            }

            // Do a check
            if (key == null)
                throw new ArgumentNullException("key");

            //int bucketIndex = HashFunction.Hash(key, Capacity);

            int bucketIndex = CarterHashFunction(key);
            
            LinkedList <Bucket<Key, Value >> bucket = KeyValuePair[bucketIndex];

            if (bucket == null)
            {
                bucket = new LinkedList<Bucket<Key, Value>>();
                KeyValuePair[bucketIndex] = bucket;
            }
           
            else
            { 
                return false;
            }
                     
            
            bucket.AddLast(new Bucket<Key, Value>(key, value));
            count++;
            return true;
        }

        public Value Search(Key key)
        {
            //int bucketIndex = HashFunction.Hash(key, Capacity);
            int bucketIndex = CarterHashFunction(key);

            LinkedList<Bucket<Key, Value>> bucket = KeyValuePair[bucketIndex];
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
            if(key == null)
            {
                return false;
            }

            //int bucketIndex = HashFunction.Hash(key, Capacity);
            int bucketIndex = CarterHashFunction(key);
            LinkedList<Bucket<Key, Value>> bucket = KeyValuePair[bucketIndex];

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

        public bool rehash()
        {
            // New list with double the size of the original
            Capacity *=  2;

            var newList = new LinkedList<Bucket<Key, Value>>[Capacity];
           
            KeyValuePair.CopyTo(newList, 0);

            KeyValuePair = newList;

            return true;        
        }

    }
}
