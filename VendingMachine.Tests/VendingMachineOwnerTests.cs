using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Models.Tests
{
    [TestClass()]
    public class VendingMachineOwnerTests
    {
        [TestMethod()]
        public void TurnOnVendingMachineTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateVendingMachineInventoryTest()
        {
            //Arrange
            const int STARTING_QTY = 5;
            
            //Act
            VendingMachine vm = new();
            VendingMachineOwner.UpdateVendingMachineInventory();
            Dictionary<string, VendingMachineProduct> actualProducts = vm.Products;

            Dictionary<string, VendingMachineProduct> expectedProducts = new()
            {
                {"A1", new Chip { Name = "Potato Crisps", Price = 3.05M, Quantity = STARTING_QTY} },
                {"A2", new Chip { Name = "Stackers", Price = 1.45M, Quantity = STARTING_QTY} },
                {"A3", new Chip { Name = "Grain Waves", Price = 2.75M, Quantity = STARTING_QTY} },
                {"A4", new Chip { Name = "Cloud Popcorn", Price = 3.65M, Quantity = STARTING_QTY} },
                {"B1", new Candy { Name = "Moonpie", Price = 1.80M, Quantity = STARTING_QTY} },
                {"B2", new Candy { Name = "Cowtales", Price = 1.50M, Quantity = STARTING_QTY} },
                {"B3", new Candy { Name = "Wonka Bar", Price = 1.50M, Quantity = STARTING_QTY} },
                {"B4", new Candy { Name = "Crunchie", Price = 1.75M, Quantity = STARTING_QTY} },
                {"C1", new Drink { Name = "Cola", Price = 1.25M, Quantity = STARTING_QTY} },
                {"C2", new Drink { Name = "Dr. Salt", Price = 1.50M, Quantity = STARTING_QTY} },
                {"C3", new Drink { Name = "Mountain Melter", Price = 1.50M, Quantity = STARTING_QTY} },
                {"C4", new Drink { Name = "Heavy", Price = 1.50M, Quantity = STARTING_QTY} },
                {"D1", new Gum { Name = "U-Chews", Price = 0.85M, Quantity = STARTING_QTY} },
                {"D2", new Gum { Name = "Little League Chew", Price = 0.95M, Quantity = STARTING_QTY} },
                {"D3", new Gum { Name = "Chiclets", Price = 0.75M, Quantity = STARTING_QTY} },
                {"D4", new Gum { Name = "Triplemint", Price = 0.75M, Quantity = STARTING_QTY} },

            };

            //Assert
            CollectionAssert.AreEquivalent(expectedProducts, actualProducts);
            
        }
    }
}