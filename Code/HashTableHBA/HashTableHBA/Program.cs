
using HashTableHBA;

IHashFunctionProvider provider = new CarterWegmanHashFunctionProvider(1);
var hashTable = new HashTable<int, string>(16, provider);



hashTable.Insert(125, "Turbo");
hashTable.Insert(125, "Turbo");
hashTable.Insert(2, "Injectors");
hashTable.Insert(326, "Pistons");
hashTable.Insert(44, "Connecting Rods");
hashTable.Insert(1, "Pump");
hashTable.Insert(550, "Turbo");
hashTable.Insert(226, "Injectors");
hashTable.Insert(342, "Pistons");
//hashTable.Insert(45, "Connecting Rods");
//hashTable.Insert(2, "Pump");
//hashTable.Insert(3, "Turbo");
//hashTable.Insert(4, "Injectors");
//hashTable.Insert(5, "Pistons");
//hashTable.Insert(6, "Connecting Rods");
//hashTable.Insert(7, "Pump");
//hashTable.Insert(8, "Pump");
//hashTable.Insert(9, "Pump");
//hashTable.Insert(10, "Turbo");
//hashTable.Insert(11, "Injectors");
//hashTable.Insert(12, "Pistons");
//hashTable.Insert(13, "Connecting Rods");
//hashTable.Insert(14, "Pump");
//hashTable.Insert(15, "Pump");
//hashTable.Insert(44, "Pump");
//hashTable.Insert(55, "Pump");
//hashTable.Insert(0, "Pump");
//hashTable.Insert(155, "Pump");
//hashTable.Insert(255, "Pump");
//hashTable.Insert(355, "Pump");

string part1 = hashTable.Search(125);
//string part2 = hashTable.Search(44);

hashTable.Update(125, "OilFilter");

string partUpdated = hashTable.Search(125);

hashTable.Delete(4);
hashTable.Delete(3);
hashTable.Delete(2);
hashTable.Delete(1);

string part3 = hashTable.Search(3);
string part4 = hashTable.Search(4);