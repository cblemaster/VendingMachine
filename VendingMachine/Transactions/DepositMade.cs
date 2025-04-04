
namespace VendingMachine.Transactions;

internal sealed class DepositMade : Transaction
{
    internal decimal DepositAmount { get; }

    internal DepositMade(decimal depositAmount) : base(DateTimeOffset.UtcNow) => DepositAmount = depositAmount;

    internal override string ToDisplayString => $"Deposited: {DepositAmount:C}...";
    internal override string ToReportString => $"{Timestamp:O} Deposit made: {DepositAmount:C}";
}
