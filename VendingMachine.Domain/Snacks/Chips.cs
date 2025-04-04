
namespace VendingMachine.Domain.Snacks;

internal sealed class Chips : Snack
{
    internal Chips(decimal price, string label) : base(price, label) { }

    internal override void SomethingPolymorphic() => base.SomethingPolymorphic();
}
