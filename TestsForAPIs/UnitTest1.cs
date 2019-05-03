using System.Collections.Generic;
using System.Threading.Tasks;
using DOS_Assessment.Controllers;
using DOS_Assessment.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DOS_Assessment.Tests
{
    [TestClass]
    public class TestSimpleProductController
    {

        //Tried to implement unit tests but haven't worked with them before.. Done some reading up on implementing them though

        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var testProducts = GetTestProducts();
            var controller = new QueuingController(testProducts);

            var result = controller.PendingMessages() as Task<People>;
           // Assert.AreEqual(testProducts.Count, result.Count);
        }

        private List<Person> GetTestProducts()
        {
            var testProducts = new List<Person>();
            testProducts.Add(new Person { FirstName = "Demo1", LastName = "Test1"});
            testProducts.Add(new Person { FirstName = "Demo2", LastName = "Test2" });
            testProducts.Add(new Person { FirstName = "Demo3", LastName = "Test3" });
            testProducts.Add(new Person { FirstName = "Demo4", LastName = "Test4" });

            return testProducts;
        }
    }
}