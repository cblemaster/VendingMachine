
using System.Text;
using VendingMachine.Transactions;

namespace VendingMachine.Reports;

internal sealed class SalesReport(IEnumerable<Transaction> transactions)
{
    private readonly IEnumerable<Transaction> _transactions = transactions;
    private List<SnackPurchased> _snacksPurchased => _transactions.OfType<SnackPurchased>().ToList();

    internal string GenerateReport()
    {
        StringBuilder sb = new();
        sb = sb.AppendLine($"SALES REPORT FOR {DateTimeOffset.UtcNow.ToString("F")}\n");

        foreach (SnackPurchased snack in _snacksPurchased)
        {
            int countOfSnackPurchased = _snacksPurchased.Count(s => s.Snack.Label == snack.Snack.Label);
            decimal price = snack.Snack.Price;
            decimal extended = countOfSnackPurchased * price;
            sb = sb.AppendLine(snack.ToReportString);
        }
    
        sb = sb.AppendLine($"TOTAL SALES: {_transactions.OfType<SnackPurchased>().Sum(s => s.Snack.Price)}");

        return sb.ToString();
    }
}
