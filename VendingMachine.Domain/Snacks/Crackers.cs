
namespace VendingMachine.Domain.Snacks;

internal sealed class Crackers : Snack
{
    internal Crackers(decimal price, string label) : base(price, label) { }

    internal override void SomethingPolymorphic() => base.SomethingPolymorphic();
}
