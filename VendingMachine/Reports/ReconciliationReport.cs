
using VendingMachine.Machine;

namespace VendingMachine.Reports;

internal sealed class ReconciliationReport(IEnumerable<Transaction> transactions)
{
    private readonly List<Transaction> _transactions = transactions.ToList();
}
