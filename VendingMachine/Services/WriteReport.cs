using System.Text;
using VendingMachine.Models;

namespace VendingMachine.Services;

internal static class WriteReport
{
    private static readonly string SalesReportName = ($"SalesReport_{Helpers.FormatedDateTimeNow}.txt").Replace(" ", "_");
    private const string REPORTS_FILE_PATH = @"..\..\..\..\Reports";
    private static readonly string AuditFileName = ($"AuditFile_{Helpers.FormatedDateTimeNow}.txt").Replace(" ", "_");
    private const string SALES_REPORT_HEADER = "*** SALES REPORT FOR VENDING MACHINE ***";
    private const string NO_SNACKS_SOLD_TODAY = "* No snacks sold today! *";

    internal static StringBuilder CreateSalesReport(VendingMachine.Models.Vendomatic vm)
    {
        StringBuilder sb = new();

        sb.AppendLine(SALES_REPORT_HEADER);

        if (vm.SnacksSoldToday.Count == 0 && vm.DailySales == 0M)
        {
            sb.AppendLine(NO_SNACKS_SOLD_TODAY);
        }

        else
        {
            sb.AppendLine(string.Format("{0,-25}{1,20}{2,20}", "Product Name", "Price", "Qty Sold"));
            
            foreach (Snack snack in vm.SnacksSoldToday.Distinct())
            {
                sb.AppendLine(string.Format("{0,-25}{1,20:C}{2,20}", snack.Label, snack.Price, vm.SnacksSoldToday.Count(s => s.Label == snack.Label)));
            }

            sb.Append("** TOTAL SALES ** " + vm.DailySales.ToString("C"));
        }

        return sb;
    }
    internal static void WriteSalesReport(StringBuilder salesReport)
    {
        string currentDirectory = Environment.CurrentDirectory;
        string fullSalesReportPath = Path.Combine(currentDirectory, REPORTS_FILE_PATH, SalesReportName);

        try
        {
            using StreamWriter sw = new(fullSalesReportPath, false);
            sw?.WriteLine(salesReport);
        }
        catch (IOException) { throw; }
    }
    internal static void WriteAuditFile(StringBuilder auditReport)
    {
        string currentDirectory = Environment.CurrentDirectory;
        string fullAuditFilePath = Path.Combine(currentDirectory, REPORTS_FILE_PATH, AuditFileName);

        try
        {
            using StreamWriter sw = new(fullAuditFilePath, false);
            sw?.WriteLine(auditReport);
        }
        catch (IOException) { throw; }
    }
}
