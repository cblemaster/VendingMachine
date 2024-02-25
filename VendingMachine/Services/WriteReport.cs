using System.Text;
using VendingMachine.Models;

namespace VendingMachine.Services;

internal static class WriteReport
{
    #region constants
    private static readonly string SALES_REPORT_FILENAME = ($"SalesReport_{Helpers.FormatedDateTimeNow}.txt").Replace(" ", "_");
    private const string REPORTS_FILE_PATH = @"..\..\..\..\Reports";
    private static readonly string AUDIT_FILE_FILENAME = ($"AuditFile_{Helpers.FormatedDateTimeNow}.txt").Replace(" ", "_");
    private const string SALES_REPORT_HEADER = "*** SALES REPORT FOR VENDING MACHINE ***";
    private const string NO_SNACKS_SOLD_TODAY = "* No snacks sold today! *";
    #endregion
    internal static StringBuilder CreateSalesReport(Vendomatic vm)
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
        string pathToAuditFile = GetReportPath(SALES_REPORT_FILENAME);
        WriteReportToFile(pathToAuditFile, salesReport);
    }
    internal static void WriteAuditFile(StringBuilder auditFile)
    {
        string pathToAuditFile = GetReportPath(AUDIT_FILE_FILENAME);
        WriteReportToFile(pathToAuditFile, auditFile);
    }
    internal static string GetReportPath(string file)
    {
        string currentDirectory = Environment.CurrentDirectory;
        return Path.Combine(currentDirectory, REPORTS_FILE_PATH, file);
    }
    internal static void WriteReportToFile(string path, StringBuilder sb)
    {
        try
        {
            using StreamWriter sw = new(path, false);
            sw.WriteLine(sb);
        }
        catch (IOException) { throw; }
    }
}
