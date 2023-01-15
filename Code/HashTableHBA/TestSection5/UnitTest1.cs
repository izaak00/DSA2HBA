using HashTableHBA;

namespace TestSection5
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestABAProblem()
        {
            var hashtable = new HashTableTwo<int,string>(100);

            // Insert a key-value pair into the hashtable
            hashtable.InsertFirst(1, "car");
            Assert.AreEqual("car", hashtable.Search(1));

            // Create a list of tasks that will update the value of the key-value pair
            var updateTasks = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                updateTasks.Add(Task.Run(() => hashtable.Update(1, "boat")));
            }

            // Wait for all tasks to complete
            Task.WaitAll(updateTasks.ToArray());

            // Assert that the value of the key-value pair has been updated to 20
            Assert.AreEqual("boat", hashtable.Search(1));

            // Create a list of tasks that will update the value of the key-value pair back to its original value
            updateTasks.Clear();
            for (int i = 0; i < 100; i++)
            {
                updateTasks.Add(Task.Run(() => hashtable.Update(1, "car")));
            }

            // Wait for all tasks to complete
            Task.WaitAll(updateTasks.ToArray());

            // Assert that the value of the key-value pair has been updated back to its original value
            Assert.AreEqual("car", hashtable.Search(1));

            // Create a list of tasks that will try to delete the key-value pair
            var deleteTasks = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                deleteTasks.Add(Task.Run(() => hashtable.Delete(1)));
            }

            // Wait for all tasks to complete
            Task.WaitAll(deleteTasks.ToArray());

            // Assert that the key-value pair has been deleted
            Assert.IsFalse(hashtable.ContainsKey(1));
        }
    }
}