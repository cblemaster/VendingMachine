
using System.Text;
using VendingMachine.Domain.Transactions;

namespace VendingMachine.Domain.Reports;

internal sealed class SummaryReport(IEnumerable<Transaction> snacksSold)
{
    private readonly List<Transaction> _snacksSold = [.. snacksSold];

    internal string GenerateReport()
    {
        int countOfSnacksPurchased = _snacksSold.Count;
        decimal sumOfSnacksPurchasedPrice = _snacksSold.OfType<SnackPurchased>().Sum(s => s.Snack.Price);
        decimal avgPriceSnacksPurchased = sumOfSnacksPurchasedPrice / countOfSnacksPurchased;

        StringBuilder sb = new();
        sb = sb.AppendLine($"SUMMARY REPORT FOR {DateTimeOffset.UtcNow.ToString("F")}\n");
        sb = sb.AppendLine($"Count of snacks purchased: {countOfSnacksPurchased}");
        sb = sb.AppendLine($"Sum of snacks purchased price: {sumOfSnacksPurchasedPrice:C}");
        sb = sb.AppendLine($"Average price of snack purchase: {avgPriceSnacksPurchased:C}");
        return sb.ToString();
    }
}
