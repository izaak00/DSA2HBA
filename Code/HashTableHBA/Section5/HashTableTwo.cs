using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace HashTableHBA
{
    public class HashTableTwo<Key, Value> : IHashTable<Key, Value>
    {
        //private LinkedList<Bucket<Key, Value>>[] KeyValuePair;
        private Bucket<int, string>[] KeyValuePair;
        volatile private int count;
        public int Capacity { get; set; }
        private readonly object _lock = new object();


        //Carter Wegman Hash Function
        public IHashFunctionProvider HashFunctionPovider;
        public IHashFunction CWHashFunction;

        //Constructor
        public HashTableTwo(int capacity, IHashFunctionProvider provider)
        {
            Capacity = capacity;
            HashFunctionPovider = provider;
            CWHashFunction = HashFunctionPovider.GetHashFunction(Capacity);
            KeyValuePair = new Bucket<int, string>[capacity];
            count = 0;
        }

        public HashTableTwo(int capacity)
        {
            Capacity = capacity;
            KeyValuePair = new Bucket<int, string>[capacity];
            count = 0;
        }

        public int Count()
        {
            return count;
        }

        public bool ContainsKey(int key)
        {
            int index = key % KeyValuePair.Length;
            Bucket<int,string> current = KeyValuePair[index];

            while (current != null)
            {
                if (current.key == key)
                {
                    return true;
                }
                current = current.nextBucket;
            }
            return false;
        }

        public bool Delete(int key)
        {
            lock(_lock)
            {
                if(!ContainsKey(key))
                {
                    return false;
                }

                int bucketIndex = HashFunction.Hash(key, Capacity);
                //int bucketIndex = CarterHashFunction(key);
                Bucket<int, string> bucket = KeyValuePair[bucketIndex];
                Bucket<int, string> previous = null;

                while (bucket != null)
                {
                    if(bucket.key == key)
                    {
                        //bucket = bucket.nextBucket;
                        if(previous == null)
                        {
                            if (Interlocked.CompareExchange(ref KeyValuePair[bucketIndex], bucket.nextBucket, bucket) != bucket)
                            {     
                                return false;
                            }
                        }
                        else
                        {
                            if (Interlocked.CompareExchange(ref previous.nextBucket, bucket.nextBucket, bucket) != bucket)
                            {    
                                return false;
                            }
                        }

                        Interlocked.Decrement(ref (count));
                        break;
                    }
                }  
                previous = bucket;
                bucket = bucket.nextBucket;                  
            }
            return true;
        }

        public double GetLoadFactor()
        {
           return (double)count / Capacity;
        }

        public int CarterHashFunction(int key)
        {    
            return CWHashFunction.Hash(key);      
        }

        public bool InsertFirst(int key, string value)
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

            Bucket<int, string > head = KeyValuePair[bucketIndex];

            Bucket<int, string> newBucket = new Bucket<int,string>(key,value);
            newBucket.nextBucket = head;

            if (ContainsKey(key))
            {
                return false;
            }

            if (Interlocked.CompareExchange(ref KeyValuePair[bucketIndex], newBucket, head) != head)
            {
                return false;
            }
  
            Interlocked.Increment(ref (count));
            return true;
        }

        public string Search(int key)
        {
            int bucketIndex = HashFunction.Hash(key, Capacity);
            //int bucketIndex = CarterHashFunction(key);

            Bucket<int, string> bucket = KeyValuePair[bucketIndex];

            while(bucket != null)
            {
                if(bucket.key.Equals(key))
                {
                    return bucket.value.ToString();
                }
                bucket = bucket.nextBucket;  
            }
    
            throw new KeyNotFoundException("Key not found: " + key.ToString());
        }

        public bool Update(int key, string newValue)
        {
            if(key == null)
            {
                return false;
            }

            if(!ContainsKey(key))
            {
                return false;
            }

            int bucketIndex = HashFunction.Hash(key, Capacity);
            //int bucketIndex = CarterHashFunction(key);
            Bucket<int, string> bucket = KeyValuePair[bucketIndex];

            while (bucket != null)
            {
                string originalValue = bucket.value;

                if (bucket.key.Equals(key))
                {
                    bucket.value = newValue;
                    return true;
                } 
                
               
                bucket = bucket.nextBucket;

                if (Interlocked.CompareExchange(ref bucket.value, newValue, originalValue) != originalValue)
                {
                    Console.WriteLine("Update failed due to concurrent modification.");
                }

                break;

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

            Bucket<int, string>[] ResizedList = new Bucket<int, string>[Capacity];

            for (int i = 0; i < KeyValuePair.Length; i++)
            {
                Bucket<int, string> entry = KeyValuePair[i];
                
                while (entry != null)
                {
                    Bucket<int, string> next = entry.nextBucket;
                    //int index = CarterHashFunction(entry.key);
                    int index = HashFunction.Hash(entry.key, Capacity);
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
