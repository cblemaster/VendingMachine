
namespace VendingMachine.Domain.Transactions;

internal abstract class Transaction(DateTimeOffset timestamp)
{
    internal DateTimeOffset Timestamp { get; } = timestamp;
}
