namespace VendingMachine.Models
{
    public class Candy : Product
    {
        public override string ShowMessageToCustomerAfterSale() => "Munch Munch, Yum!";        
    }
}