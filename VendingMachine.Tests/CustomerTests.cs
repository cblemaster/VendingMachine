using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace VendingMachine.Models.Tests
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void DepositMoneyTest_BalanceIncreases()
        {
            //Arrange
            decimal expected = 5M;

            //Act
            Vendomatic vm = new();
            Customer.DepositMoney(vm, 5);
            decimal actual = vm.CustomerBalance;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DepositMoneyTest_ZeroAmount()
        {
            //Arrange            
       
            //Act
            Vendomatic vm = new();
            
            //Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Customer.DepositMoney(vm, 0));
        
        }
        
        [TestMethod]
        public void DepositMoneyTest_NegativeAmount()
        {
            //Arrange
            
            //Act
            Vendomatic vm = new();

            //Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Customer.DepositMoney(vm, -10));
        }

        [TestMethod]
        public void PurchaseProductTest()
        {
            //Arrange
            decimal expectedCustomerBalance = 5 - 3.05M;
            decimal expectedDailySales = 3.05M;
            Dictionary<string, List<Product>> expectedProductsSoldToday = new()
            {
                { "A1", new List<Product>() { new Chip() { Name = "Potato Crisps", Price = 3.05M } } }
            };        

            //Act
            Vendomatic vm = new();
            Owner.UpdateVendingMachineInventory(vm);
            Customer.DepositMoney(vm, 5);
            Customer.PurchaseProduct(vm, "A1");

            //Assert
            Assert.AreEqual(expectedCustomerBalance, vm.CustomerBalance);
            Assert.AreEqual(expectedDailySales, vm.DailySales);
            CollectionAssert.AreEqual(expectedProductsSoldToday, vm.ProductsSoldToday);   //TODO: Fix this failing collectionassert
            Assert.AreEqual(4, vm.Products.Count);
        }

        [TestMethod]
        public void PurchaseProductTest_CustomerBalanceZero()
        {
            //Arrange

            //Act
            Vendomatic vm = new()
            {
                CustomerBalance = 0M
            };

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Customer.PurchaseProduct(vm, "A1" ));
        }

        [TestMethod]
        public void PurchaseProductTest_ProductQuantityZero()
        {
            //Arrange
            
            //Act
            Vendomatic vm = new();
            foreach (KeyValuePair<string, List<Product>> slot in vm.Products)
            {
                slot.Value.Clear();
            }
    
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Customer.PurchaseProduct(vm, "A1"));
        }

        [TestMethod]
        public void PurchaseProductTest_SelectedSlotInvalid()
        {
            //Arrange
            Vendomatic vm = new();
            Owner.UpdateVendingMachineInventory(vm);

            //Act

            //Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Customer.PurchaseProduct(vm,"7"));

        }

        [TestMethod]
        public void PurchaseProductTest_AllProductsEmpty()
        {
            //Arrange
            Vendomatic vm = new();
            vm.Products.Clear();

            //Act

            //Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Customer.PurchaseProduct(vm, "A1"));
        }

    }
}