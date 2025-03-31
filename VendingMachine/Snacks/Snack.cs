
namespace VendingMachine.Snacks;

internal abstract class Snack(decimal price, string label)
{
    protected decimal Price { get; } = price;
    protected string Label { get; } = label;

    protected new string ToString() => string.Format("{0,-20} {1:C}", Label, Price);
    internal abstract string MessageWhenSold();
}
