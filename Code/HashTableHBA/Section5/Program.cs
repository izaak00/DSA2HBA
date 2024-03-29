﻿using HashTableHBA;

IHashFunctionProvider provider = new CarterWegmanHashFunctionProvider(1);
var hashTable = new HashTableTwo<int, string>(16, provider);


for (int i = 0; i <= 100; i++)
{
    hashTable.InsertFirst(i, "Turbo");
}


//for (int i = 900; i < 1800; i++)
//{
//    hashTable.Insert(i, "Injectors");
//}


string part1 = hashTable.Search(1);
//string part2 = hashTable.Search(44);

hashTable.Update(12, "OilFilter");

string partUpdated = hashTable.Search(12);
string part6 = hashTable.Search(6);
hashTable.Delete(70);
hashTable.Delete(6);
hashTable.Delete(70);
hashTable.Delete(8);

string part3 = hashTable.Search(70);
string part4 = hashTable.Search(13);