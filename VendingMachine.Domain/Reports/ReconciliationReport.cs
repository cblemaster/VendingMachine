
using System.Text;
using VendingMachine.Domain.Transactions;

namespace VendingMachine.Domain.Reports;

internal sealed class ReconciliationReport(IEnumerable<Transaction> transactions)
{
    private readonly List<Transaction> _transactions = [.. transactions];

    internal string GenerateReport()
    {
        decimal sumOfDeposits = _transactions.OfType<DepositMade>().Sum(d => d.DepositAmount);
        decimal sumOfSnacksPurchasedPrice = _transactions.OfType<SnackPurchased>().Sum(s => s.Snack.Price);
        decimal sumOfChangeReturned = _transactions.OfType<ChangeReturned>().Sum(c => c.ChangeAmount);

        StringBuilder sb = new();
        sb = sb.AppendLine($"RECONCILIATION REPORT FOR {DateTimeOffset.UtcNow.ToString("F")}\n");
        sb = sb.AppendLine($"Sum of deposits: {sumOfDeposits:C}");
        sb = sb.AppendLine($"Sum of snacks purchased price: {sumOfSnacksPurchasedPrice}");
        sb = sb.AppendLine($"Sum of change returned: {sumOfChangeReturned}");

        decimal reconcile = sumOfDeposits - sumOfSnacksPurchasedPrice - sumOfChangeReturned;
        sb = sb.AppendLine($"Reconciliation amount: {reconcile:C}");

        sb = reconcile == 0
            ? sb.AppendLine($"RECONCILATION SUCCESS")
            : sb.AppendLine($"RECONCILATION FAILURE, see Transaction report to reconcile transaction line items.");

        return sb.ToString();
    }
}
