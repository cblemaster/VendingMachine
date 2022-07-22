using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachine.Models.Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
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

        [TestMethod]
        public void ToStringTest()
        {
            //TODO: Is this really testing anything...?
            //Arrange
            string expected = string.Format("{0,-20} {1:C}", "Potato Crisps", 3.05M);

            //Act
            Product product = new() { Name = "Potato Crisps", Price = 3.05M };
            string actual = product.ToString();

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void GumTests_ShowMessageToCustomerAfterSaleTest()
        {
            //Arrange
            string expected = "Chew Chew, Yum!";

            //Act
            Gum gum = new();
            string actual = gum.ShowMessageToCustomerAfterSale();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CandyTests_ShowMessageToCustomerAfterSaleTest()
        {
            //Arrange
            string expected = "Munch Munch, Yum!";

            //Act
            Candy candy = new();
            string actual = candy.ShowMessageToCustomerAfterSale();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ChipTests_ShowMessageToCustomerAfterSaleTest()
        {
            //Arrange
            string expected = "Crunch Crunch, Yum!";

            //Act
            Chip chip = new();
            string actual = chip.ShowMessageToCustomerAfterSale();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        
        [TestMethod]
        public void DrinkTests_ShowMessageToCustomerAfterSaleTest()
        {
            //Arrange
            string expected = "Glug Glug, Yum!";

            //Act
            Drink drink = new();
            string actual = drink.ShowMessageToCustomerAfterSale();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}