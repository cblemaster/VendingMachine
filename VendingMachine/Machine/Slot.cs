using VendingMachine.Inventory;

namespace VendingMachine.Machine;

internal sealed class Slot
{
    internal const string SNACK_SOLD_OUT = "Snack sold out.";

    internal required string Identifier { get; set; }
    internal Stack<Snack> Snacks { get; set; } = new Stack<Snack>();
    internal bool IsSoldOut => Snacks.Count == 0;

    public override string ToString() =>
        string.Format("{0,-5}{1}", Identifier, Snacks.Count > 0 ? Snacks.First().ToString() : SNACK_SOLD_OUT);
}
