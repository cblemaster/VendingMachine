using VendingMachine.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Assert.AreEqual(4, vm.Products["A1"].Count);

            //Do the collections have the same count?
            Assert.AreEqual(expectedProductsSoldToday.Count, vm.ProductsSoldToday.Count);

            //Are the product lists the same?
            foreach (KeyValuePair<string, List<Product>> kvp in expectedProductsSoldToday)
            {
                Assert.AreEqual(expectedProductsSoldToday[kvp.Key].Count, vm.ProductsSoldToday[kvp.Key].Count);
                Assert.AreEqual(expectedProductsSoldToday[kvp.Key].GetType(), vm.Products[kvp.Key].GetType());
                foreach (Product product in expectedProductsSoldToday[kvp.Key])
                {
                    int productIndex = expectedProductsSoldToday[kvp.Key].IndexOf(product);
                    Assert.AreEqual(product.GetType(), vm.Products[kvp.Key][productIndex].GetType());
                    Assert.AreEqual(product.Name, vm.Products[kvp.Key][productIndex].Name);
                    Assert.AreEqual(product.Price, vm.Products[kvp.Key][productIndex].Price);
                }
            }
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

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Customer.PurchaseProduct(vm, "A1"));
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
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Customer.PurchaseProduct(vm, "7"));

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

        [TestMethod]
        public void PurchaseProductTest_PurchaseAllProductsThenTryToPurchase()
        {
            //Arrange

            //Act
            Vendomatic vm = new();
            Owner.UpdateVendingMachineInventory(vm);
            Customer.DepositMoney(vm, 250);
            foreach (KeyValuePair<string, List<Product>> kvp in vm.Products)
            {
                while (kvp.Value.Any())
                {
                    Customer.PurchaseProduct(vm, kvp.Key);
                }
            }

            //Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Customer.PurchaseProduct(vm, "A1"));
        }

        [TestMethod]
        public void PurchaseProductTest_PurchaseAllOfOneProductThenTryToPurchaseThatProduct()
        {
            //Arrange

            //Act
            Vendomatic vm = new();
            Owner.UpdateVendingMachineInventory(vm);
            Customer.DepositMoney(vm, 25);
            while (vm.Products["A1"].Any())
            {
                Customer.PurchaseProduct(vm, "A1");
            }

            //Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Customer.PurchaseProduct(vm, "A1"));

        }



        // NOTE that the FinishTransaction tests also test these methods:
        //      Customer.CalculateCoinCount()
        //      Customer.FormatChangeOutput()
        // FinishTransactions() is the only method that calls these two

        [TestMethod]
        public void FinishTransactionTest()
        {
            //Arrange
            string expected = "Your change is: 17 quarters, 1 dime, 1 nickel";

            //Act
            Vendomatic vm = new() { CustomerBalance = 4.40M };
            string actual = Customer.FinishTransaction(vm);

            //Assert            
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void FinishTransactionTest_BalanceZero()
        {
            //Arrange

            //Act
            Vendomatic vm = new() { CustomerBalance = 0M };

            //Assert            
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Customer.FinishTransaction(vm));
        }

        [TestMethod()]
        public void FinishTransactionTest_BalanceNegative()
        {
            //Arrange

            //Act
            Vendomatic vm = new() { CustomerBalance = -7.69M };

            //Assert            
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Customer.FinishTransaction(vm));
        }

        [TestMethod()]
        public void FinishTransactionTest_ChangeIsAllQuarters()
        {
            //Arrange
            string expected = "Your change is: 7 quarters";

            //Act
            Vendomatic vm = new() { CustomerBalance = 1.75M };
            string actual = Customer.FinishTransaction(vm);

            //Assert            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FinishTransactionTest_ChangeIsAllDimes()
        {
            //Arrange
            string expected = "Your change is: 2 dimes";

            //Act
            Vendomatic vm = new() { CustomerBalance = 0.20M };
            string actual = Customer.FinishTransaction(vm);

            //Assert            
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void FinishTransactionTest_ChangeIsAllNickels()
        {
            //Arrange
            string expected = "Your change is: 1 nickel";

            //Act
            Vendomatic vm = new() { CustomerBalance = 0.05M };
            string actual = Customer.FinishTransaction(vm);

            //Assert            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FinishTransactionTest_ChangeIsQuartersAndDimes()
        {
            //Arrange
            string expected = "Your change is: 3 quarters, 2 dimes";

            //Act
            Vendomatic vm = new() { CustomerBalance = 0.95M };
            string actual = Customer.FinishTransaction(vm);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FinishTransactionTest_ChangeIsQuartersAndNickels()
        {
            //Arrange
            string expected = "Your change is: 4 quarters, 1 nickel";   // can there ever be more than one nickel in any given change combination?

            //Act
            Vendomatic vm = new() { CustomerBalance = 1.05M };
            string actual = Customer.FinishTransaction(vm);

            //Assert            
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void FinishTransactionTest_ChangeIsDimesAndNickels()
        {
            //Arrange
            string expected = "Your change is: 1 dime, 1 nickel";

            //Act
            Vendomatic vm = new() { CustomerBalance = 0.15M };
            string actual = Customer.FinishTransaction(vm);

            //Assert            
            Assert.AreEqual(expected, actual);

        }

    }
}