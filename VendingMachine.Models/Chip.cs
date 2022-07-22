namespace VendingMachine.Models
{
    public class Chip : Product
    {
        public override string ShowMessageToCustomerAfterSale() => "Crunch Crunch, Yum!";
    }
}