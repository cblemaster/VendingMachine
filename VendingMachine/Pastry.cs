
namespace VendingMachine;

internal sealed class Pastry : Snack
{
    internal Pastry(decimal price, string label) : base(price, label) { }

    internal override void SomethingPolymorphic() => base.SomethingPolymorphic();
}
