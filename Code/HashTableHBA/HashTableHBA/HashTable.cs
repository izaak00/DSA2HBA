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
        public IHashFunction CWHashFunction;

        //Constructor
        public HashTable(int capacity, IHashFunctionProvider provider)
        {
            Capacity = capacity;
            HashFunctionPovider = provider;
            CWHashFunction = HashFunctionPovider.GetHashFunction(Capacity);
            KeyValuePair = new Bucket<Key, Value>[capacity];
            count = 0;
        }

        public HashTable(int capacity)
        {
            Capacity = capacity;
            KeyValuePair = new Bucket<Key, Value>[capacity];
            count = 0;
        }

        public int Count()
        {
            return count;
        }

        public bool Delete(Key key)
        {
            int bucketIndex = HashFunction.Hash(key, Capacity);
            //int bucketIndex = CarterHashFunction(key);
            Bucket<Key, Value> bucket = KeyValuePair[bucketIndex];

            while(bucket != null)
            { 

                if (bucket.key.Equals(key))
                {
                    //KeyValuePair[bucketIndex] = bucket.nextBucket;
                    //bucket.nextBucket = bucket.nextBucket.nextBucket;
                    bucket = bucket.nextBucket;
                    count--;
                    return true;
                }

                else if (bucket.nextBucket == null)
                {
                    return false;
                }

                else if (bucket.nextBucket.key.Equals(key))
                {
                    bucket.nextBucket = bucket.nextBucket.nextBucket;
                    count--;
                    return true;
                }

                bucket = bucket.nextBucket;                  
            }
            return false;
        }

        public double GetLoadFactor()
        {
           return (double)count / Capacity;
        }

        public int CarterHashFunction(Key key)
        {    
            return CWHashFunction.Hash(key);      
        }

        public bool Insert(Key key, Value value)
        {
            if (GetLoadFactor() >= 0.9)
            {
                rehash();
            }

            // Do a check
            if (key == null)
                throw new ArgumentNullException("key");

            int bucketIndex = HashFunction.Hash(key, Capacity);

            //int bucketIndex = CarterHashFunction(key);

            Bucket<Key, Value > bucket = KeyValuePair[bucketIndex];

            if (KeyValuePair[bucketIndex] == null)
            {
                Bucket<Key, Value> BucketChain = new Bucket<Key, Value>(key, value);
                BucketChain.nextBucket = KeyValuePair[bucketIndex];
                KeyValuePair[bucketIndex] = BucketChain;      
            }
           
            else
            {   
                if (KeyValuePair[bucketIndex].key.Equals(key))
                {
                    return false;
                }
      
                while (bucket.nextBucket != null)
                {
                    bucket = bucket.nextBucket;
                }
                
                bucket.nextBucket = new Bucket<Key, Value>(key,value);
            }
            count++;
            return true;
        }

        public Value Search(Key key)
        {
            int bucketIndex = HashFunction.Hash(key, Capacity);
            //int bucketIndex = CarterHashFunction(key);

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

            int bucketIndex = HashFunction.Hash(key, Capacity);
            //int bucketIndex = CarterHashFunction(key);
            Bucket<Key, Value> bucket = KeyValuePair[bucketIndex];

            while (bucket != null)
            {          
                if (bucket.key.Equals(key))
                {
                    bucket.value = newValue;
                    return true;
                } 
                
               
                bucket = bucket.nextBucket;

                if(bucket.key.Equals(key))
                {
                    bucket.value = newValue;
                    return true;
                }
               
            }
            return false;
        }

        public bool rehash()
        {
            if(KeyValuePair == null)
            {
                return false;
            }

            // New list with double the size of the original
            Capacity *= 2;

            CWHashFunction = HashFunctionPovider.GetHashFunction(Capacity);

            Bucket<Key, Value>[] ResizedList = new Bucket<Key, Value>[Capacity];

            for (int i = 0; i < KeyValuePair.Length; i++)
            {
                Bucket<Key, Value> entry = KeyValuePair[i];
                
                while (entry != null)
                {
                    Bucket<Key, Value> next = entry.nextBucket;
                    int index = CarterHashFunction(entry.key);
                    entry.nextBucket = ResizedList[index];
                    ResizedList[index] = entry;
                    entry = next;
                }
            }
            KeyValuePair = ResizedList;

            return true;
        }
    }
}
