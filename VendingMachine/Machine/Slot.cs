
using VendingMachine.Snacks;

namespace VendingMachine.Machine;

internal class Slot
{
    internal const int SLOT_CAPACITY = 5;

    internal string Identifier { get; }
    internal Snack[] Snacks { get; }

    internal Slot(string identifier, Snack[] snacks)
    {
        Identifier = identifier;
        Snacks = snacks;
    }
}
