namespace VendingMachine.Inventory;

internal sealed class Chips : Snack
{
    internal override string SendMessageWhenSold() => "Crunch, crunch, crunch.";
}
