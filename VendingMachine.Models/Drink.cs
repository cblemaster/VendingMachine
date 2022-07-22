namespace VendingMachine.Models
{
    public class Drink : Product
    {
        public override string ShowMessageToCustomerAfterSale() => "Glug Glug, Yum!";        
    }
}