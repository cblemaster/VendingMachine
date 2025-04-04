
using VendingMachine.Snacks;

namespace VendingMachine.Transactions;

internal sealed class SnackPurchased : Transaction
{
    internal Snack Snack { get; }
    internal string Identifier { get; }

    internal SnackPurchased(Snack snack, string identifier) : base(DateTimeOffset.UtcNow)
    {
        Snack = snack;
        Identifier = identifier;
    }

    internal override string ToDisplayString => $"Snack purchased: {Snack.Label}, enjoy your {Snack.GetType()}...";
    internal override string ToReportString => "";
}
