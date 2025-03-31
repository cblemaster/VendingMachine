
namespace VendingMachine;

internal sealed class DepositFunds
{
    private decimal Amount { get; }
    private DateTimeOffset TimeStamp { get; }

    internal DepositFunds(decimal amount)
    {
        Amount = amount;
        TimeStamp = DateTimeOffset.UtcNow;
    }

    internal new string ToString() => $"{Amount:C} Deposited";
    internal string ToReportString => $"{TimeStamp:f} - ${Amount:C} Deposited";
}
