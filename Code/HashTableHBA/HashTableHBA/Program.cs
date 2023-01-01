
using HashTableHBA;

IHashFunctionProvider provider = new CarterWegmanHashFunctionProvider(1);
var hashTable = new HashTable<int, string>(16, provider);


for(int i = 0; i<900; i++)
{
    hashTable.Insert(i, "Turbo");
}


for (int i = 900; i < 1800; i++)
{
    hashTable.Insert(i, "Injectors");
}


string part1 = hashTable.Search(125);
//string part2 = hashTable.Search(44);

hashTable.Update(125, "OilFilter");

string partUpdated = hashTable.Search(125);

hashTable.Delete(125);
hashTable.Delete(326);
hashTable.Delete(2);
hashTable.Delete(1);

string part3 = hashTable.Search(125);
string part4 = hashTable.Search(4);