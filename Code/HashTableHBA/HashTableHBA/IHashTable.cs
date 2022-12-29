using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableHBA
{
    public interface IHashTable<Key,Value>
    {
        // The Insert operation adds a new key, value pair to the hash table
        // If the key already exists, the Insert operation fails
        // The operation returns true if the key, value pair is successfully added
        // The operation returns false if the operation fails
        // If the operation fails, no changes are made to the hash table
        bool Insert(Key key, Value value);

        // The Update 	operation updates the value of an existing key, value pair
        // If the key does not already exist in the hash table, the operation fails
        // If the key exists in the hash table, the value for the key, value pair that is
        // identified by the key is replaced by the new value.
        // The operation returns true if the operation succeeds and false if it fails
        // If the operation fails, no changes are made to the hash table
        bool Update(Key key, Value newValue);

        // The Search operation will return the value associated with the key
        // If the key is not found in the hash table, an Exception should be thrown
        // The Search operation should never make changes in the hash table
        Value Search(Key key);

        // The Delete operation will remove the key, value pair associated with the key
        // If the key is not found, the operation fails and nothing is removed
        // The Delete will return true if the key, value pair is successfully removed and 
        // it will return a false otherwise
        // If the operation fails, no changes are made to the hash table
        bool Delete(Key key);
        // Return the number of key, value pairs stored within the hash table
        int Count(); 
        // Returns the load factor of the hash table
        double GetLoadFactor();


    }
}
