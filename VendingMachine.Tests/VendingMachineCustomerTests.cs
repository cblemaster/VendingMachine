using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace VendingMachine.Models.Tests
{
    [TestClass()]
    public class VendingMachineCustomerTests
    {
        [TestMethod()]
        public void DepositMoneyTest_BalanceIncreases()
        {
            //Arrange
            decimal expected = 5;

            //Act
            VendingMachine vm = new();
            VendingMachineCustomer.DepositMoney(vm, 5);
            decimal actual = vm.CustomerBalance;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DepositMoneyTest_ZeroAmount()
        {
            //Arrange            
            //Act
            VendingMachine vm = new();
            
            //Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => VendingMachineCustomer.DepositMoney(vm, 0));
        
        }
        
        [TestMethod()]
        public void DepositMoneyTest_NegativeAmount()
        {
            //Arrange
            //Act
            VendingMachine vm = new();

            //Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => VendingMachineCustomer.DepositMoney(vm, -10));
        }

        [TestMethod()]
        public void PurchaseProductTest()
        {
            //Assert.Fail();  **Pick up here; need several test for this method
        }

        [TestMethod()]
        public void PurchaseProductTest_CustomerBalanceZero()
        {
            //Arrange
            //Act
            VendingMachine vm = new();
            vm.CustomerBalance = 0M;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => VendingMachineCustomer.PurchaseProduct(vm, new VendingMachineProduct { Name= "Potato Crisps", Price = 3.05M, Quantity= 1}) );
        }

        [TestMethod()]
        public void PurchaseProductTest_ProductQuantityZero()
        {
            //Arrange
            //Act
            VendingMachine vm = new();
    
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => VendingMachineCustomer.PurchaseProduct(vm, new VendingMachineProduct { Name = "Potato Crisps", Price = 3.05M, Quantity = 0 }));
        }

    }
}