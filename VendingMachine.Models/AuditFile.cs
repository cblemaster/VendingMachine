using System.Text;

namespace VendingMachine.Models
{
    public class AuditFile
    {
        private readonly string AUDIT_FILE_NAME = ("AuditFile_" + DateTime.Now.ToString("M-d-yyyy H.m.s") + ".txt").Replace(" ", "_");
        private const string AUDIT_FILE_PATH = @"..\..\..\..\Reports";

        private StringBuilder AuditLog { get; set; } = new();

        //These methods are tested visually in Audit File output
        //Difficult to unit test these due to void return types and
        //Datetimes in formatted strings

        public void WriteDepositToAuditLog(int amount, decimal customerBalance)
        {
            this.AuditLog.Append(string.Format("{0}|{1}|{2}|{3}", DateTime.Now.ToString("M-d-yyyy H:m:s"), "FEED MONEY", "AMOUNT DEPOSITED: " + amount.ToString("C"), "CUSTOMER BALANCE: " + customerBalance.ToString("C")));
            this.AuditLog.Append('\n');
        }

        public void WriteProductSoldToAuditLog(Product product, decimal customerBalance, string slotLocation)
        {
            this.AuditLog.Append(string.Format("{0}|{1}|{2}|{3}|{4}", DateTime.Now.ToString("M-d-yyyy H:m:s"), "PRODUCT NAME: " + product.Name, "SLOT LOCATION : " + slotLocation, "PRODUCT PRICE: " + product.Price.ToString("C"), "CUSTOMER BALANCE: " + customerBalance.ToString("C")));
            this.AuditLog.Append('\n');
        }

        public void WriteChangeDispensedToAuditLog(decimal amountOfChange, decimal customerBalance)
        {
            this.AuditLog.Append(string.Format("{0}|{1}|{2}", DateTime.Now.ToString("M-d-yyyy H:m:s"), "CHANGE DISPENSED: " + amountOfChange.ToString("C"), "REMAINING BALANCE: " + customerBalance.ToString("C")));
            this.AuditLog.Append('\n');
        }

        public void WriteAuditLogToFile()
        {
            string currentDirectory = Environment.CurrentDirectory;
            string fullAuditFilePath = Path.Combine(currentDirectory, AUDIT_FILE_PATH, AUDIT_FILE_NAME);

            try
            {
                using StreamWriter sw = new(fullAuditFilePath, false);
                sw.WriteLine(this.AuditLog);
            }
            catch (IOException ex)
            {
                // TODO: Exception handling
            }
            catch (Exception ex)
            {
                // TODO: Exception handling
            }
        }        
    }
}