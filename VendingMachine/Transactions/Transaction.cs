
namespace VendingMachine.Transactions;

internal abstract class Transaction(DateTimeOffset timestamp)
{
    internal DateTimeOffset Timestamp { get; } = timestamp;

    internal abstract string ToDisplayString { get; }
    internal abstract string ToReportString { get; }
}
