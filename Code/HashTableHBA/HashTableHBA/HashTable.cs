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
        //private LinkedList<Bucket<Key, Value>>[] KeyValuePair;
        private Bucket<Key, Value>[] KeyValuePair;
        private int count;
        public int Capacity { get; set; }

        //Carter Wegman Hash Function
        public IHashFunctionProvider HashFunctionPovider;
        public IHashFunction HashFunction;

        //Constructor
        public HashTable(int capacity, IHashFunctionProvider provider)
        {
            Capacity = capacity;
            HashFunctionPovider = provider;
            HashFunction = HashFunctionPovider.GetHashFunction(Capacity);
            KeyValuePair = new Bucket<Key, Value>[capacity];
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
            Bucket<Key, Value> bucket = KeyValuePair[bucketIndex];

            while(bucket != null)
            { 
                if (bucket.key.Equals(key))
                {
                    KeyValuePair[bucketIndex] = bucket.nextBucket;
                    count--;
                    return true;
                }
                else if (bucket.nextBucket.key.Equals(key))
                {
                    bucket.nextBucket = bucket.nextBucket.nextBucket;
                    count--;
                    return true;
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
            //if(GetLoadFactor() >= 0.9)
            //{
            //    rehash();
            //}

            // Do a check
            if (key == null)
                throw new ArgumentNullException("key");

            //int bucketIndex = HashFunction.Hash(key, Capacity);

            int bucketIndex = CarterHashFunction(key);
            
            Bucket<Key, Value > bucket = new Bucket<Key, Value>(key,value);

            if (KeyValuePair[bucketIndex] == null)
            {
                bucket.nextBucket = KeyValuePair[bucketIndex];
                KeyValuePair[bucketIndex] = bucket;      
            }
           
            else
            { 
                return false;
            }
    
            count++;
            return true;
        }

        public Value Search(Key key)
        {
            //int bucketIndex = HashFunction.Hash(key, Capacity);
            int bucketIndex = CarterHashFunction(key);

            Bucket<Key, Value> bucket = KeyValuePair[bucketIndex];

            while(bucket != null)
            {
                if(bucket.key.Equals(key))
                {
                    return bucket.value;
                }
                bucket = bucket.nextBucket;  
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
            Bucket<Key, Value> bucket = KeyValuePair[bucketIndex];

            while (bucket != null)
            {          
                if (bucket.key.Equals(key))
                {
                    bucket.value = newValue;
                    return true;
                }   
            }
            return false;
        }

        //public bool rehash()
        //{
        //    // New list with double the size of the original
        //    Capacity *=  2;

        //    var newList = new LinkedList<Bucket<Key, Value>>[Capacity];
           
        //    KeyValuePair.CopyTo(newList, 0);

        //    KeyValuePair = newList;

        //    return true;        
        //}

    }
}
