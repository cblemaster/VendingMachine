
using System.Text;
using VendingMachine.Domain.Transactions;

namespace VendingMachine.Domain.Reports;

internal sealed class TransactionReport(IEnumerable<Transaction> transactions)
{
    private readonly List<Transaction> _transactions = [.. transactions];

    internal void GenerateReport()
    {
        StringBuilder sb = new();
        sb = sb.AppendLine($"TRANSACTION REPORT FOR {DateTimeOffset.UtcNow.ToString("F")}\n");

        decimal deposit = 0m;

        foreach (Transaction transaction in _transactions.OrderBy(t => t.Timestamp))
        {
            sb = sb.AppendLine($"{transaction.GetType()}, {transaction.ToReportString}, deposit: {deposit}");
            if (transaction is ChangeReturned change)
            {
                deposit -= change.ChangeAmount;
            }
            else if (transaction is SnackPurchased snack)
            {
                deposit -= snack.Snack.Price;
            }
            else if (transaction is DepositMade depositMade)
            {
                deposit += depositMade.DepositAmount;
            }
        }
    }
}
