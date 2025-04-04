
using VendingMachine.Domain.Snacks;

namespace VendingMachine.Domain.Inventory;

internal sealed class SnackSlot
{
    internal const int SNACK_SLOT_CAPACITY = 5;

    private readonly List<Snack> _snacks;

    internal string Identifier { get; }
    internal IReadOnlyCollection<Snack> Snacks => _snacks.AsReadOnly();

    internal SnackSlot(string identifier)
    {
        Identifier = identifier;
        _snacks = [];
    }

    internal void AddSnacks(Snack[] snacks) => _snacks.AddRange(snacks);

    internal void RemoveSnack(Snack snack) => _snacks.Remove(snack);

    internal string ToDisplayString => $"Snack slot: {Identifier}{(Snacks.Count != 0 ? $", {Snacks.First().ToDisplayString}" : "Sold out!")}";
}
