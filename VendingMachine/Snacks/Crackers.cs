
namespace VendingMachine.Snacks;

internal sealed class Crackers(decimal price, string label) : Snack(price, label)
{
    internal override string MessageWhenSold() => "Enjoy your toasty treat.";
}
