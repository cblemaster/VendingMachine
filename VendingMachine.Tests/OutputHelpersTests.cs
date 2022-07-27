using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VendingMachine.Models;
using VendingMachine.UI;

namespace VendingMachine.UI.Tests
{
    [TestClass]
    public class OutputHelpersTests
    {
        [TestMethod]
        public void DisplayProductsTest_AllProductsEmpty()
        {
            //This is mostly tested visually in UI output

            //Arrange
            
            //Act
            Vendomatic vm = new();
            vm.Products.Clear();

            //Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => OutputHelpers.DisplayProducts(vm));
        }

        [TestMethod]
        public void DisplayCustomerBalanceTest()
        {
            //Arrange
            string expected = "Current balance: $6.66";

            //Act
            Vendomatic vm = new()
            {
                CustomerBalance = 6.66M
            };
            var actual = OutputHelpers.DisplayCustomerBalance(vm);

            //Assert
            Assert.AreEqual(expected, actual);

        }
    }
    
}