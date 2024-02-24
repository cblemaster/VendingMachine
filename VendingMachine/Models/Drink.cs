namespace VendingMachine.Models
{
    internal class Drink : Snack
    {
        internal override string SendMessageWhenSold() => "May your thirst begone.";
    }
}
