namespace VendingMachine.Models
{
    public class VendingMachineCustomer
    {
        public static void PurchaseProduct(VendingMachine vendingMachine, VendingMachineProduct product)
        {
            if (vendingMachine.CustomerBalance <= 0) throw new ArgumentOutOfRangeException(nameof(vendingMachine.CustomerBalance),"Must deposit money before making a selection!");
            if (product.Quantity <= 0) throw new ArgumentOutOfRangeException(nameof(product.Quantity), "Product is sold out!");
            
            // update customer balance
            vendingMachine.CustomerBalance -= product.Price;
            // update vending machine balance
            vendingMachine.DailySales += product.Price;
            // update product quantity
            product.Quantity--;            
        }

        public static void DepositMoney(VendingMachine vendingMachine, int amountDeposited)
        {
            if (amountDeposited <= 0) throw new ArgumentOutOfRangeException(nameof(amountDeposited), "Amount to deposit must be grater than zero!");

            vendingMachine.CustomerBalance += amountDeposited;
        }
    }

}