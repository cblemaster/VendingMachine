
using _Customer = VendingMachine.Customer.Customer;
using _Owner = VendingMachine.Owner.Owner;

namespace VendingMachine.Machine;

internal sealed class VendTron
{
    private readonly string[] _slotRows = ["A", "B", "C", "D"];
    private readonly string[] _slotColumns = ["1", "2", "3", "4"];

    internal _Customer Customer { get; }
    internal _Owner Owner { get; }
    internal Slot[] Slots { get; }

    private VendTron(Slot[] slots)
    {
        Customer = new();
        Owner = new();
        Slots = slots;
    }
}
