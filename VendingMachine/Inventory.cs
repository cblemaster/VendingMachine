
namespace VendingMachine;

internal sealed class Inventory
{
    private SnackSlot[] _snackSlots;

    internal SnackSlot[] SnackSlots => _snackSlots;

    internal Inventory(SnackSlot[] snackSlots)
    {
        _snackSlots = snackSlots;
    }
}
