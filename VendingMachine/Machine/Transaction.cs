
namespace VendingMachine.Machine;

internal record Transaction(TransactionType TransactionType, decimal Amount, string Text, DateTimeOffset Timestamp);
