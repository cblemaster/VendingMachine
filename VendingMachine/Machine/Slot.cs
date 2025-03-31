
using VendingMachine.Snacks;

namespace VendingMachine.Machine;

internal class Slot
{
    private const int SLOT_CAPACITY = 5;

    internal string Identifier { get; }
    internal Snack[] Snacks { get; }
}
