namespace VendingMachine.Models
{
    public class Gum : Product
    {
        public override string ShowMessageToCustomerAfterSale() => "Chew Chew, Yum!";        
    }
}