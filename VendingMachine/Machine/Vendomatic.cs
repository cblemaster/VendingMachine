
using _Customer = VendingMachine.Customer.Customer;
using _Owner = VendingMachine.Owner.Owner;
using _Inventory = VendingMachine.Inventory.Inventory;

namespace VendingMachine.Machine;

internal sealed class Vendomatic
{
    internal _Customer Customer { get; }
    internal _Owner Owner { get; }
    internal _Inventory Inventory { get; }

    private Vendomatic()
    {
        Customer = new();
        Owner = new();
        Inventory = new();
    }
}
