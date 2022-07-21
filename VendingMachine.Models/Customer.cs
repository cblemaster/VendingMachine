namespace VendingMachine.Models
{
    public class Customer
    {
        private const string INVALID_SLOT_LOCATION_ERROR = "Selected slot location is invalid!";
        private const string CUSTOMER_BALANCE_ZERO_OR_NEGATIVE_ERROR = "Must deposit money before making a selection!";
        private const string ALL_PRODUCTS_EMPTY_ERROR = "Vending machine is sold out of all products!";
        private const string SELECTED_PRODUCT_EMPTY_ERROR = "Selected product is sold out!";
        private const string DEPOSIT_AMOUNT_ZERO_OR_NEGATIVE_ERROR = "Amount to deposit must be greater than zero!";


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
                 
            return purchasedProduct.ShowMessageToCustomerAfterSale();            
        }

        public static void DepositMoney(Vendomatic vm, int amountDeposited)
        {
            if (amountDeposited <= 0) throw new ArgumentOutOfRangeException(nameof(amountDeposited), DEPOSIT_AMOUNT_ZERO_OR_NEGATIVE_ERROR);

            vm.CustomerBalance += amountDeposited;
            // TODO: Caller needs to call outputhelper.displaycustomerbalance after this!
        }
    }

}