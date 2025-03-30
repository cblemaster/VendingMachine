namespace VendingMachine.Inventory;

internal sealed class Drink : Snack
{
    internal override string SendMessageWhenSold() => "May your thirst begone.";
}
