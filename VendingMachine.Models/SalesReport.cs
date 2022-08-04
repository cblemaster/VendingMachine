using System.Text;

namespace VendingMachine.Models
{
    public class SalesReport
    {
        private readonly string SALES_REPORT_NAME = ("SalesReport_" + DateTime.Now.ToString("M-d-yyyy H.mm.ss") + ".txt").Replace(" ", "_");
        private const string SALES_REPORT_PATH = @"..\..\..\..\Reports";

        private StringBuilder SalesLog { get; set; } = new();

        //These methods are tested visually in Sales Report output
        //Difficult to unit test these due to void return types

        public void CreateSalesLogFromVendingMachineDailySales(Vendomatic vm)
        {
            if (! vm.ProductsSoldToday.Any()) throw new ArgumentOutOfRangeException(nameof(vm.ProductsSoldToday), "No products sold today!");     //TODO: Exception handling
            if (vm.DailySales <= 0) throw new ArgumentOutOfRangeException(nameof(vm.DailySales), "No products sold today!");     //TODO: Exception handling

            SalesLog.Append(string.Format("{0,-10}{1,25}{2,20}{3,20}", "Slot", "Product Name", "Price", "Qty Sold"));
            SalesLog.Append('\n');

            foreach (KeyValuePair<string, List<Product>> slot in vm.ProductsSoldToday.OrderBy(p => p.Key))
            {
                if (slot.Value.Any())
                {
                    SalesLog.Append(string.Format("{0,-10}{1,25}{2,20}{3,20}",slot.Key, slot.Value[0].Name, slot.Value[0].Price.ToString("C"), slot.Value.Count));
                    SalesLog.Append('\n');
                }
            }
            
            SalesLog.Append("\n**TOTAL SALES** " + vm.DailySales.ToString("C"));
        }

        public void WriteSalesReportToFile()
        {
            string currentDirectory = Environment.CurrentDirectory;
            string fullSalesReportPath = Path.Combine(currentDirectory, SALES_REPORT_PATH, SALES_REPORT_NAME);

            try
            {
                using StreamWriter sw = new(fullSalesReportPath, false);
                sw.WriteLine(this.SalesLog);
                SalesLog.Clear();
            }
            catch (IOException ex)
            {
                // TODO: Exception handling
            }
            catch (Exception ex)
            {
                // TODO: Exception handling
            }
            finally
            {
                
            }
        }
    }
}