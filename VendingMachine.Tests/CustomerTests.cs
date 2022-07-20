using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Models.Tests
{
    [TestClass()]
    public class CustomerTests
    {
        [TestMethod()]
        public void DepositMoneyTest_BalanceIncreases()
        {
            //Arrange
            decimal expected = 5;

            //Act
            Vendomatic vm = new();
            Customer.DepositMoney(vm, 5);
            decimal actual = vm.CustomerBalance;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DepositMoneyTest_ZeroAmount()
        {
            //Arrange            
            //Act
            Vendomatic vm = new();
            
            //Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Customer.DepositMoney(vm, 0));
        
        }
        
        [TestMethod()]
        public void DepositMoneyTest_NegativeAmount()
        {
            //Arrange
            //Act
            Vendomatic vm = new();

            //Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Customer.DepositMoney(vm, -10));
        }

        [TestMethod()]
        public void PurchaseProductTest()
        {
            //Arrange
            decimal expectedCustomerBalance = 5 - 3.05M;
            decimal expectedDailySales = 3.05M;
            List<Product> expectedProductsSoldToday = new()
            {
                new Product()
                {
                    Name = "Potato Crisps",
                    Price = 3.05M,
                    SlotLocation = "A1",
                }
            };
            var expectedProductQuantity = 4;


            //Arrange
            Vendomatic vm = new();
            Customer.DepositMoney(vm, 5);
            Customer.PurchaseProduct(vm, new Chip {Name = "Potato Crisps", Price = 3.05M, SlotLocation = "A1" } new Chip() {  Price = 3.05M, SlotLocation = "A1" });
            
            Assert.AreEqual(expectedProductQuantity, vm.Products.Select(s => s.SlotLocation == "A1").FirstOrDefault().
            
        }

        [TestMethod()]
        public void PurchaseProductTest_CustomerBalanceZero()
        {
            //Arrange
            //Act
            Vendomatic vm = new();
            vm.CustomerBalance = 0M;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Customer.PurchaseProduct(vm, new Product { Name= "Potato Crisps", Price = 3.05M}) );
        }

        [TestMethod()]
        public void PurchaseProductTest_ProductQuantityZero()
        {
            //Arrange
            //Act
            Vendomatic vm = new();
    
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Customer.PurchaseProduct(vm, new Product { Name = "Potato Crisps", Price = 3.05M, Quantity = 0 }));
        }

    }
}