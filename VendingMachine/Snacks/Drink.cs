
namespace VendingMachine.Snacks;

internal sealed class Drink : Snack
{
    internal Drink(decimal price, string label) : base(price, label) { }

    internal override void SomethingPolymorphic() => base.SomethingPolymorphic();
}
