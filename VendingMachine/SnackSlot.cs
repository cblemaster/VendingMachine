
namespace VendingMachine;

internal sealed class SnackSlot
{
    internal const int SNACK_SLOT_CAPACITY = 5;
    
    private List<Snack> _snacks;
    
    internal string Identifier { get; }
    internal List<Snack> Snacks => _snacks;

    internal SnackSlot(string identifier)
    {
        Identifier = identifier;
        _snacks = [];
    }

    internal void AddSnack(Snack snack) => _snacks.Add(snack);

    internal void AddSnacks(Snack[] snacks) => _snacks.AddRange(snacks);

    internal void RemoveSnack(Snack snack) => _snacks.Remove(snack);

    internal string ToDisplayString => $"Snack slot: {Identifier}{(Snacks.Count != 0 ? $", {Snacks.First().ToDisplayString}" : "SOLD OUT")}";
}
