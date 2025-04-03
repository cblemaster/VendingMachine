
using VendingMachine.Machine;

namespace VendingMachine.Reports;

internal sealed class SalesReport(IEnumerable<Transaction> snacksSold)
{
    private readonly List<Transaction> _snacksSold = snacksSold.ToList();
}
