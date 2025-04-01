
namespace VendingMachine;

internal sealed class VendTron
{
    private Snack[] _purchases = [];
    private decimal[] _deposits = [];
    private decimal[] _changeReturned = [];
    
    internal Inventory Inventory { get; }
    internal decimal Deposits { get; private set; }

    internal VendTron()
    {
        Inventory = default!;
        // LOAD INVENTORY!
    }
}
