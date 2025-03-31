
namespace VendingMachine.Snacks;

internal sealed class Drink(decimal price, string label) : Snack(price, label)
{
    internal override string MessageWhenSold() => "May your thirst begone.";
}
