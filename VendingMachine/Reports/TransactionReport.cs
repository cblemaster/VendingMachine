

using VendingMachine.Machine;

namespace VendingMachine.Reports;

internal sealed class TransactionReport(IEnumerable<Transaction> transactions)
{
    private decimal _deposit;
    private readonly List<Transaction> _transactions = transactions;
}
