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
    public class VendomaticTests
    {
        [TestMethod()]
        public void DisplayCustomerBalanceTest()
        {
            //Arrange
            string expected = "Current balance: $6.66";

            //Act
            Vendomatic vm = new();
            vm.CustomerBalance = 6.66M;
            var actual = vm.DisplayCustomerBalance();
            
            Assert.AreEqual(expected, actual);
            
        }
    }
}