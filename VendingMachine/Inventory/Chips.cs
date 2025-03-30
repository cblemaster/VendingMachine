
namespace VendingMachine.Inventory;

internal sealed class Chips(decimal price, string label) : Snack(price, label)
{
    internal override string MessageWhenSold() => "Crunch, crunch, crunch.";
}
