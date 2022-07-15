namespace VendingMachine.Models
{
    public class VendingMachineCustomer
    {
        public static void PurchaseProduct(VendingMachine vendingMachine, VendingMachineProduct product)
        {
            if (vendingMachine.CustomerBalance <= 0) return;  // TODO: Error handling
            if (product.Quantity <= 0) return;  // TODO: Error handling
            {
                // update customer balance
                vendingMachine.CustomerBalance -= product.Price;
                // update vending machine balance
                vendingMachine.DailySales += product.Price;
                // update product quantity
                product.Quantity--;
            }

        }

        internal static void DepositMoney(VendingMachine vendingMachine, int amountDeposited)
        {
            if (amountDeposited <= 0) return;  // TODO: Error handling
            vendingMachine.CustomerBalance += amountDeposited;
        }
    }

}