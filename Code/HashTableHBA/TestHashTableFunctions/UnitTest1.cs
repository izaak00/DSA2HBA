using HashTableHBA;

namespace TestHashTableFunctions
{
    [TestClass]
    public class UnitTest1
    {
  
        [TestMethod]
        public void InsertTest()
        {  
            var hashTable = new HashTable<int, string>(16);
            int key = 1;
            string value = "Test_value";

            //testing the insert function
            bool insert = hashTable.Insert(key, value);
            Assert.IsTrue(insert);

            //Check that the search works and result matches the expected output
            //string search = hashTable.Search(key);
            //Assert.AreEqual(value, search);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var hashTable = new HashTable<int, string>(16);

            int key = 1;
            string value = "Test_value";

            //testing the insert function
            bool insert = hashTable.Insert(key, value);

            bool delete = hashTable.Delete(key);
            Assert.IsTrue(delete);
        }

        [TestMethod]
        public void SearchTest()
        {
            var hashTable = new HashTable<int, string>(16);
            int key = 1;
            string value = "Test_value";

            
            bool insert = hashTable.Insert(key, value);

            //Check that the search works and result matches the expected output
            string search = hashTable.Search(key);
            Assert.AreEqual(value, search);
        }

        [TestMethod]
        
        public void UpdateTest()
        {
            var hashTable = new HashTable<int, string>(16);
            int key = 1;
            string value = "Test_value";
            string updatedValue = "Test2";

            bool insert = hashTable.Insert(key, value);

            bool update = hashTable.Update(key, updatedValue);

            // search updated string
            string search = hashTable.Search(key);
            Assert.AreEqual(updatedValue, search);
        }
    }
}