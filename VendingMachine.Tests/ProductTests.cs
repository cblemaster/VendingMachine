using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachine.Models.Tests
{
    [TestClass()]
    public class ProductTests
    {
        [TestMethod()]
        public void ShowMessageToCustomerAfterSaleTest()
        {
            //TODO: Is this really testing anything...?
            //Arrange
            string expected = "Base class message...";

            //Act
            Product product = new();
            string actual = product.ShowMessageToCustomerAfterSale();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            //TODO: Is this really testing anything...?
            //Arrange
            string expected = string.Format("{0,-20} {1:C}", "Potato Crisps", 3.05M);

            //Act
            Product vmp = new() { Name = "Potato Crisps", Price = 3.05M, Quantity = 1 };
            string actual = vmp.ToString();

            //Assert
            Assert.AreEqual(expected, actual);

        }
    }
}