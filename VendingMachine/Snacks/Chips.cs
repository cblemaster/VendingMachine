
namespace VendingMachine.Snacks;

internal sealed class Chips(decimal price, string label) : Snack(price, label)
{
    internal override string MessageWhenSold() => "Crunch, crunch, crunch.";
}
