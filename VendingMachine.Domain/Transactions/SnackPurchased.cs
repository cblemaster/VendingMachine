
using VendingMachine.Domain.Snacks;

namespace VendingMachine.Domain.Transactions;

internal sealed class SnackPurchased : Transaction
{
    private readonly string _identifier;
    
    internal Snack Snack { get; }
    
    internal SnackPurchased(Snack snack, string identifier) : base(DateTimeOffset.UtcNow)
    {
        Snack = snack;
        _identifier = identifier;
    }
}
