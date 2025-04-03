
using VendingMachine.Machine;

namespace VendingMachine.Reports;

internal sealed class SummaryReport(IEnumerable<Transaction> snacksSold)
{
    private readonly List<Transaction> _snacksSold = snacksSold.ToList();
}
