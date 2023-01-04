
using HashTableHBA;

IHashFunctionProvider provider = new CarterWegmanHashFunctionProvider(1);
var hashTable = new HashTable<int, string>(16, provider);


for(int i = 0; i<30; i++)
{
    hashTable.Insert(i, "Turbo");
}


//for (int i = 900; i < 1800; i++)
//{
//    hashTable.Insert(i, "Injectors");
//}


string part1 = hashTable.Search(25);
//string part2 = hashTable.Search(44);

hashTable.Update(12, "OilFilter");

string partUpdated = hashTable.Search(12);

hashTable.Delete(5);
hashTable.Delete(6);
hashTable.Delete(7);
hashTable.Delete(8);

string part3 = hashTable.Search(10);
string part4 = hashTable.Search(13);