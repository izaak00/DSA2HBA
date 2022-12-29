﻿
using HashTableHBA;


var hashTable = new HashTable<int, string>(16);

hashTable.Insert(125, "Turbo");
hashTable.Insert(22, "Injectors");
hashTable.Insert(326, "Pistons");
hashTable.Insert(44, "Connecting Rods");
hashTable.Insert(1, "Pump");
hashTable.Insert(550, "Turbo");
hashTable.Insert(226, "Injectors");
hashTable.Insert(342, "Pistons");
hashTable.Insert(45, "Connecting Rods");
hashTable.Insert(2, "Pump");
hashTable.Insert(3, "Turbo");
hashTable.Insert(4, "Injectors");
hashTable.Insert(5, "Pistons");
hashTable.Insert(6, "Connecting Rods");
hashTable.Insert(7, "Pump");
hashTable.Insert(8, "Pump");

string part1 = hashTable.Search(1);
string part2 = hashTable.Search(44);

hashTable.Update(125, "OilFilter");

string partUpdated = hashTable.Search(125);

hashTable.Delete(4);
hashTable.Delete(3);
hashTable.Delete(2);
hashTable.Delete(1);

string part3 = hashTable.Search(3);
string part4 = hashTable.Search(4);