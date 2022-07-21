using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachine.Models.Tests
{
    [TestClass]
    public class VendomaticTests
    {
        [TestMethod]
        public void AreAllProductsEmptyTest_ProductsAreNotEmpty()
        {
            //ARRANGE
            bool expected = false;

            //ACT
            Vendomatic vm = new()
            {
                Products = new()
            {
                { "A1", new()
                    { new Chip() { Name = "Potato Crisps", Price = 3.05M } }
                }
            }
            };

            bool actual = vm.AreAllProductsEmpty();

            //ASSERT
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void AreAllProductsEmptyTest_ProductsAreEmpty()
        {
            //ARRANGE
            bool expected = true;

            //ACT
            Vendomatic vm = new();
            vm.Products.Clear();
            bool actual = vm.AreAllProductsEmpty();

            //ASSERT
            Assert.AreEqual(expected, actual);
        }
    }
}