
namespace VendingMachine.Snacks;

internal sealed class Poptart(decimal price, string label) : Snack(price, label)
{
    internal override string MessageWhenSold() => "Savor this delicious pastry.";
}
