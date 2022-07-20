using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace VendingMachine.Models.Tests
{
    [TestClass()]
    public class OwnerTests
    {
        [TestMethod()]
        public void TurnOnVendingMachineTest()
        {
            //Arrange
            bool expected = true;

            //Act
            Vendomatic vm = new();
            Owner.TurnOnVendingMachine(vm);
            bool actual = vm.IsOn;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TurnOffVendingMachineTest()
        {
            //Arrange
            bool expected = false;

            //Act
            Vendomatic vm = new();
            Owner.TurnOffVendingMachine(vm);
            bool actual = vm.IsOn;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void UpdateVendingMachineInventoryTest()
        {
            // TODO: Find a better way of asserting dictionary
            //       equality, or equivalency, or whatever.
            //       This test is probably slow!


            //Arrange
            Dictionary<string, Product> expectedProducts = new()
            {
                { "A1", new Chip { Name = "Potato Crisps", Price = 3.05M, Quantity = Product.INITIAL_STARTING_QUANTITY } },
                { "A2", new Chip { Name = "Stackers", Price = 1.45M, Quantity = Product.INITIAL_STARTING_QUANTITY } },
                { "A3", new Chip { Name = "Grain Waves", Price = 2.75M, Quantity = Product.INITIAL_STARTING_QUANTITY } },
                { "A4", new Chip { Name = "Cloud Popcorn", Price = 3.65M, Quantity = Product.INITIAL_STARTING_QUANTITY } },
                { "B1", new Candy { Name = "Moonpie", Price = 1.80M, Quantity = Product.INITIAL_STARTING_QUANTITY } },
                { "B2", new Candy { Name = "Cowtales", Price = 1.50M, Quantity = Product.INITIAL_STARTING_QUANTITY } },
                { "B3", new Candy { Name = "Wonka Bar", Price = 1.50M, Quantity = Product.INITIAL_STARTING_QUANTITY } },
                { "B4", new Candy { Name = "Crunchie", Price = 1.75M, Quantity = Product.INITIAL_STARTING_QUANTITY } },
                { "C1", new Drink { Name = "Cola", Price = 1.25M, Quantity = Product.INITIAL_STARTING_QUANTITY } },
                { "C2", new Drink { Name = "Dr. Salt", Price = 1.50M, Quantity = Product.INITIAL_STARTING_QUANTITY } },
                { "C3", new Drink { Name = "Mountain Melter", Price = 1.50M, Quantity = Product.INITIAL_STARTING_QUANTITY } },
                { "C4", new Drink { Name = "Heavy", Price = 1.50M, Quantity = Product.INITIAL_STARTING_QUANTITY } },
                { "D1", new Gum { Name = "U-Chews", Price = 0.85M, Quantity = Product.INITIAL_STARTING_QUANTITY } },
                { "D2", new Gum { Name = "Little League Chew", Price = 0.95M, Quantity = Product.INITIAL_STARTING_QUANTITY } },
                { "D3", new Gum { Name = "Chiclets", Price = 0.75M, Quantity = Product.INITIAL_STARTING_QUANTITY } },
                { "D4", new Gum { Name = "Triplemint", Price = 0.75M, Quantity = Product.INITIAL_STARTING_QUANTITY } },

            };

            //Act
            Vendomatic vm = new();
            bool actualBoolResult = Owner.UpdateVendingMachineInventory(vm);
            Dictionary<string, Product> actualProducts = vm.Products;

            //Assert
            //Did the method return true?
            Assert.AreEqual(true, actualBoolResult);

            //Do the collections have the same count?
            Assert.AreEqual(expectedProducts.Count, actualProducts.Count);

            //Are the products the same?
            foreach (KeyValuePair<string, Product> kvp in expectedProducts)
            {
                Assert.AreEqual(kvp.Value.Name, actualProducts[kvp.Key].Name);
                Assert.AreEqual(kvp.Value.Price, actualProducts[kvp.Key].Price);
                Assert.AreEqual(kvp.Value.Quantity, actualProducts[kvp.Key].Quantity);
            }
        }
    }
}