namespace VendingMachine.Models
{
    public class Customer
    {
        public static string PurchaseProduct(Vendomatic vendingMachine, string selectedSlotLocation)
        {
            if (vendingMachine == null) throw new ArgumentNullException(nameof(vendingMachine), "Unknown vending machine.");
            if (! vendingMachine.Products.ContainsKey(selectedSlotLocation)) throw new ArgumentOutOfRangeException(nameof(selectedSlotLocation), "Selected slot location is invalid!");
            if (vendingMachine.CustomerBalance <= 0) throw new ArgumentOutOfRangeException(nameof(vendingMachine.CustomerBalance), "Must deposit money before making a selection!");
            if (vendingMachine.AreAllProductsEmpty()) throw new ArgumentOutOfRangeException(nameof(vendingMachine.Products), "Vending machine is sold out of all products!");
            if (vendingMachine.Products[selectedSlotLocation].Count <= 0) throw new ArgumentOutOfRangeException(nameof(vendingMachine.Products), "Product is sold out!");  //TODO: Use consts for error messages? Are error messages consistent?

            var purchasedProduct = vendingMachine.Products[selectedSlotLocation][0];
            if (purchasedProduct == null) throw new NullReferenceException("Product not found!");

            // update customer balance
            vendingMachine.CustomerBalance -= purchasedProduct.Price;
            
            // update vending machine daily sales
            vendingMachine.DailySales += purchasedProduct.Price;

            // update product quantity
            vendingMachine.Products[selectedSlotLocation].Remove(purchasedProduct);

            // update vending machine products sold today
            vendingMachine.ProductsSoldToday[selectedSlotLocation].Add(purchasedProduct);
                 
            return purchasedProduct.ShowMessageToCustomerAfterSale();            
        }

        public static void DepositMoney(Vendomatic vendingMachine, int amountDeposited)
        {
            if (vendingMachine == null) throw new ArgumentNullException(nameof(vendingMachine), "Unknown vending machine.");
            if (amountDeposited <= 0) throw new ArgumentOutOfRangeException(nameof(amountDeposited), "Amount to deposit must be greater than zero!");

            vendingMachine.CustomerBalance += amountDeposited;
            // TODO: Caller needs to call vendingmachine.displaycustomerbalance after this!
        }
    }

}