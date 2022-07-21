using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace VendingMachine.Models.Tests
{
    [TestClass]
    public class OwnerTests
    {
        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void UpdateVendingMachineInventoryTest()
        {
            // TODO: Find a better way of asserting dictionary
            //       equality, or equivalency, or whatever.
            //       This test is probably slow!


            //Arrange
            Dictionary<string, List<Product>> expectedProducts = new()
            {
                { "A1", new List<Product>() { new Chip() { Name = "Potato Crisps", Price = 3.05M }, new Chip() { Name = "Potato Crisps", Price = 3.05M }, new Chip() { Name = "Potato Crisps", Price = 3.05M }, new Chip() { Name = "Potato Crisps", Price = 3.05M }, new Chip() { Name = "Potato Crisps", Price = 3.05M } } },
                { "A2", new List<Product>() { new Chip() { Name = "Stackers", Price = 1.45M }, new Chip() { Name = "Stackers", Price = 1.45M }, new Chip() { Name = "Stackers", Price = 1.45M }, new Chip() { Name = "Stackers", Price = 1.45M }, new Chip() { Name = "Stackers", Price = 1.45M } } },
                { "A3", new List<Product>() { new Chip () { Name = "Grain Waves", Price = 2.75M }, new Chip() { Name = "Grain Waves", Price = 2.75M }, new Chip() { Name = "Grain Waves", Price = 2.75M }, new Chip() { Name = "Grain Waves", Price = 2.75M }, new Chip() { Name = "Grain Waves", Price = 2.75M } } },
                { "A4", new List<Product>() { new Chip() { Name = "Cloud Popcorn", Price = 3.65M }, new Chip() { Name = "Cloud Popcorn", Price = 3.65M }, new Chip() { Name = "Cloud Popcorn", Price = 3.65M }, new Chip() { Name = "Cloud Popcorn", Price = 3.65M }, new Chip() { Name = "Cloud Popcorn", Price = 3.65M } } },
                { "B1", new List<Product>() { new Candy() { Name = "Moonpie", Price = 1.80M }, new Candy() { Name = "Moonpie", Price = 1.80M }, new Candy() { Name = "Moonpie", Price = 1.80M }, new Candy() { Name = "Moonpie", Price = 1.80M }, new Candy() { Name = "Moonpie", Price = 1.80M } } },
                { "B2", new List<Product>() { new Candy() { Name = "Cowtales", Price = 1.50M }, new Candy() { Name = "Cowtales", Price = 1.50M }, new Candy() { Name = "Cowtales", Price = 1.50M }, new Candy() { Name = "Cowtales", Price = 1.50M }, new Candy() { Name = "Cowtales", Price = 1.50M } } },
                { "B3", new List<Product>() { new Candy() { Name = "Wonka Bar", Price = 1.50M }, new Candy() { Name = "Wonka Bar", Price = 1.50M }, new Candy() { Name = "Wonka Bar", Price = 1.50M }, new Candy() { Name = "Wonka Bar", Price = 1.50M }, new Candy() { Name = "Wonka Bar", Price = 1.50M } } },
                { "B4", new List<Product>() { new Candy() { Name = "Crunchie", Price = 1.75M }, new Candy() { Name = "Crunchie", Price = 1.75M }, new Candy() { Name = "Crunchie", Price = 1.75M }, new Candy() { Name = "Crunchie", Price = 1.75M }, new Candy() { Name = "Crunchie", Price = 1.75M } } },
                { "C1", new List<Product>() { new Drink() { Name = "Cola", Price = 1.25M }, new Drink() { Name = "Cola", Price = 1.25M }, new Drink() { Name = "Cola", Price = 1.25M }, new Drink() { Name = "Cola", Price = 1.25M }, new Drink() { Name = "Cola", Price = 1.25M } } },
                { "C2", new List<Product>() { new Drink() { Name = "Dr. Salt", Price = 1.50M }, new Drink() { Name = "Dr. Salt", Price = 1.50M }, new Drink() { Name = "Dr. Salt", Price = 1.50M }, new Drink() { Name = "Dr. Salt", Price = 1.50M }, new Drink() { Name = "Dr. Salt", Price = 1.50M } } },
                { "C3", new List<Product>() { new Drink() { Name = "Mountain Melter", Price = 1.50M }, new Drink() { Name = "Mountain Melter", Price = 1.50M }, new Drink() { Name = "Mountain Melter", Price = 1.50M }, new Drink() { Name = "Mountain Melter", Price = 1.50M }, new Drink() { Name = "Mountain Melter", Price = 1.50M } } },
                { "C4", new List<Product>() { new Drink() { Name = "Heavy", Price = 1.50M }, new Drink() { Name = "Heavy", Price = 1.50M }, new Drink() { Name = "Heavy", Price = 1.50M }, new Drink() { Name = "Heavy", Price = 1.50M }, new Drink() { Name = "Heavy", Price = 1.50M } } },
                { "D1", new List<Product>() { new Gum() { Name = "U-Chews", Price = 0.85M }, new Gum() { Name = "U-Chews", Price = 0.85M }, new Gum() { Name = "U-Chews", Price = 0.85M }, new Gum() { Name = "U-Chews", Price = 0.85M }, new Gum() { Name = "U-Chews", Price = 0.85M } } },
                { "D2", new List<Product>() { new Gum() { Name = "Little League Chew", Price = 0.95M }, new Gum() { Name = "Little League Chew", Price = 0.95M }, new Gum() { Name = "Little League Chew", Price = 0.95M }, new Gum() { Name = "Little League Chew", Price = 0.95M }, new Gum() { Name = "Little League Chew", Price = 0.95M } } },
                { "D3", new List<Product>() { new Gum() { Name = "Chiclets", Price = 0.75M }, new Gum() { Name = "Chiclets", Price = 0.75M }, new Gum() { Name = "Chiclets", Price = 0.75M }, new Gum() { Name = "Chiclets", Price = 0.75M }, new Gum() { Name = "Chiclets", Price = 0.75M } } },
                { "D4", new List<Product>() { new Gum() { Name = "Triplemint", Price = 0.75M }, new Gum() { Name = "Triplemint", Price = 0.75M }, new Gum() { Name = "Triplemint", Price = 0.75M }, new Gum() { Name = "Triplemint", Price = 0.75M }, new Gum() { Name = "Triplemint", Price = 0.75M } } },
            };

            //Act
            Vendomatic vm = new();
            bool actualBoolResult = Owner.UpdateVendingMachineInventory(vm);
            Dictionary<string, List<Product>> actualProducts = vm.Products;

            //Assert
            //Did the method return true?
            Assert.AreEqual(true, actualBoolResult);

            //Do the collections have the same count?
            Assert.AreEqual(expectedProducts.Count, actualProducts.Count);

            //Are the product lists the same?
            foreach (KeyValuePair<string, List<Product>> kvp in expectedProducts)
            {
                Assert.AreEqual(expectedProducts[kvp.Key].Count, actualProducts[kvp.Key].Count);
                Assert.AreEqual(expectedProducts[kvp.Key].GetType(), actualProducts[kvp.Key].GetType());
                foreach (Product product in expectedProducts[kvp.Key])
                {
                    int productIndex = expectedProducts[kvp.Key].IndexOf(product);
                    Assert.AreEqual(product.GetType(), actualProducts[kvp.Key][productIndex].GetType());
                    Assert.AreEqual(product.Name, actualProducts[kvp.Key][productIndex].Name);
                    Assert.AreEqual(product.Price, actualProducts[kvp.Key][productIndex].Price);
                }
            }
        }
    }
}