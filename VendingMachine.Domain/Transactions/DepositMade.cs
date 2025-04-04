namespace VendingMachine.Domain.Transactions;

internal sealed class DepositMade : Transaction
{
    internal decimal DepositAmount { get; }

    internal DepositMade(decimal depositAmount) : base(DateTimeOffset.UtcNow) => DepositAmount = depositAmount;
}
