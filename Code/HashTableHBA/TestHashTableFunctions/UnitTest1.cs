using HashTableHBA;

namespace TestHashTableFunctions
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        { 
            IHashFunctionProvider provider = new CarterWegmanHashFunctionProvider(1);
            var hashTable = new HashTable<int, string>(16, provider);

            int key = 1;
            string value = "Test_value";
            //testing the insert function
            bool insert = hashTable.Insert(key, value);
            Assert.IsTrue(insert);

            //Check that the search works and result matches the expected output
            string search = hashTable.Search(key);
            Assert.AreEqual(value, search);
        }
    }
}