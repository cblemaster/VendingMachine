
namespace VendingMachine;

internal class Snack
{
    internal decimal Price { get; }
    internal string Label { get; }

    internal Snack(decimal price, string label)
    {
        Price = price;
        Label = label;
    }

    internal virtual void SomethingPolymorphic() { }
}
