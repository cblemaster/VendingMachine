using System.Text;

namespace VendingMachine.Models
{
    public class Customer
    {
        private const string INVALID_SLOT_LOCATION_ERROR = "Selected slot location is invalid!";
        private const string CUSTOMER_BALANCE_ZERO_OR_NEGATIVE_ERROR = "Must deposit money before making a selection!";
        private const string ALL_PRODUCTS_EMPTY_ERROR = "Vending machine is sold out of all products!";
        private const string SELECTED_PRODUCT_EMPTY_ERROR = "Selected product is sold out!";
        private const string DEPOSIT_AMOUNT_ZERO_OR_NEGATIVE_ERROR = "Amount to deposit must be greater than zero!";
        private const string NO_CHANGE_TO_DISPENSE_ERROR = "No change to dispense!";
        private const string CALCULATING_CHANGE_ERROR = "Error calculating change!";
        
        private enum Coins
        {
            Quarter = 25,
            Dime = 10,
            Nickel = 5,
        }
        
        public static string PurchaseProduct(Vendomatic vm, string selectedSlotLocation)
        {
            if (! vm.Products.ContainsKey(selectedSlotLocation)) throw new ArgumentOutOfRangeException(nameof(selectedSlotLocation), INVALID_SLOT_LOCATION_ERROR);     //TODO: Exception handling
            if (vm.CustomerBalance <= 0) throw new ArgumentOutOfRangeException(nameof(vm.CustomerBalance), CUSTOMER_BALANCE_ZERO_OR_NEGATIVE_ERROR);
            if (vm.AreAllProductsEmpty()) throw new ArgumentOutOfRangeException(nameof(vm.Products), ALL_PRODUCTS_EMPTY_ERROR);
            if (vm.Products[selectedSlotLocation].Count <= 0) throw new ArgumentOutOfRangeException(nameof(vm.Products), SELECTED_PRODUCT_EMPTY_ERROR);

            var purchasedProduct = vm.Products[selectedSlotLocation][0];

            // update customer balance
            vm.CustomerBalance -= purchasedProduct.Price;
            
            // update vending machine daily sales
            vm.DailySales += purchasedProduct.Price;

            // update product quantity
            vm.Products[selectedSlotLocation].Remove(purchasedProduct);

            // update vending machine products sold today
            if (vm.ProductsSoldToday.ContainsKey(selectedSlotLocation))
            {
                vm.ProductsSoldToday[selectedSlotLocation].Add(purchasedProduct);
            }
            else
            {
                vm.ProductsSoldToday.Add(selectedSlotLocation, new() { purchasedProduct });
            }

            AuditFile af = new();
            af.FormatProductSoldForAuditFile(purchasedProduct, vm.CustomerBalance, selectedSlotLocation);
                 
            return purchasedProduct.ShowMessageToCustomerAfterSale();            
        }

        public static void DepositMoney(Vendomatic vm, int amountDeposited)
        {
            if (amountDeposited <= 0) throw new ArgumentOutOfRangeException(nameof(amountDeposited), DEPOSIT_AMOUNT_ZERO_OR_NEGATIVE_ERROR);

            vm.CustomerBalance += amountDeposited;

            AuditFile af = new();
            af.FormatDepositForAuditFile(amountDeposited, vm.CustomerBalance);
        }

        public static string FinishTransaction(Vendomatic vm)
        {
            if (vm.CustomerBalance <= 0) throw new ArgumentOutOfRangeException(nameof(vm.CustomerBalance), NO_CHANGE_TO_DISPENSE_ERROR);

            decimal changeDispensed = vm.CustomerBalance;
            
            int numQuarters = CalculateCoinCount(vm.CustomerBalance, Coins.Quarter);
            vm.CustomerBalance -= numQuarters * (((int)Coins.Quarter) / 100M);

            int numDimes = CalculateCoinCount(vm.CustomerBalance, Coins.Dime);
            vm.CustomerBalance -= numDimes * (((int)Coins.Dime) / 100M);

            int numNickels = CalculateCoinCount(vm.CustomerBalance, Coins.Nickel);
            vm.CustomerBalance -= numNickels * (((int)Coins.Nickel) / 100M);

            if (vm.CustomerBalance != 0) throw new ArgumentOutOfRangeException(nameof(vm.CustomerBalance), CALCULATING_CHANGE_ERROR);

            AuditFile af = new();
            af.FormatChangeDispensedForAuditFile(changeDispensed, vm.CustomerBalance);
            
            return FormatChangeOutput(numQuarters, numDimes, numNickels);
        }

        private static int CalculateCoinCount(decimal amount, Coins coin)
        {
            return (int)(amount / ((int)coin / 100M));
        }

        private static string FormatChangeOutput(int numQuarters, int numDimes, int numNickels)
        {            
            StringBuilder sb = new();
            sb.Append("Your change is: ");
            if (numQuarters > 0)
            {
                if (numQuarters == 1) { sb.Append(string.Format("{0} quarter", numQuarters)); }
                if (numQuarters > 1) { sb.Append(string.Format("{0} quarters", numQuarters)); }
                if (numDimes > 0 || numNickels > 0) { sb.Append(", "); }
            }
            if (numDimes > 0)
            {
                if (numDimes == 1) { sb.Append(string.Format("{0} dime", numDimes)); }
                if (numDimes > 1) { sb.Append(string.Format("{0} dimes", numDimes)); }
                if (numNickels > 0) { sb.Append(", "); }
            }
            if (numNickels > 0)
            {
                if (numNickels == 1) { sb.Append(string.Format("{0} nickel", numNickels)); }
                if (numNickels > 1) { sb.Append(string.Format("{0} nickels", numNickels)); }
            }                  
            
            return sb.ToString();
        }

    }

}