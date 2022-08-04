using System.Text;

namespace VendingMachine.Models
{
    public class AuditFile
    {
        private readonly string AUDIT_FILE_NAME = ("AuditFile_" + DateTime.Now.ToString("M-d-yyyy H.mm.ss") + ".txt").Replace(" ", "_");
        private const string AUDIT_FILE_PATH = @"..\..\..\..\Reports";

        //These methods are tested visually in Audit File output
        //Difficult to unit test these due to void return types and
        //Datetimes in formatted strings

        public void FormatDepositForAuditFile(int amount, decimal customerBalance)
        {
            StringBuilder sb = new();
            sb.Append(string.Format("{0}|{1}|{2}|{3}", DateTime.Now.ToString("M-d-yyyy H:mm:ss"), "FEED MONEY", "AMOUNT DEPOSITED: " + amount.ToString("C"), "CUSTOMER BALANCE: " + customerBalance.ToString("C")));
            sb.Append('\n');
            WriteLineToAuditFile(sb.ToString());
        }

        public void FormatProductSoldForAuditFile(Product product, decimal customerBalance, string slotLocation)
        {
            StringBuilder sb = new();
            sb.Append(string.Format("{0}|{1}|{2}|{3}|{4}", DateTime.Now.ToString("M-d-yyyy H:mm:ss"), "PRODUCT NAME: " + product.Name, "SLOT LOCATION : " + slotLocation, "PRODUCT PRICE: " + product.Price.ToString("C"), "CUSTOMER BALANCE: " + customerBalance.ToString("C")));
            sb.Append('\n');
            WriteLineToAuditFile(sb.ToString());
        }

        public void FormatChangeDispensedForAuditFile(decimal amountOfChange, decimal customerBalance)
        {
            StringBuilder sb = new();
            sb.Append(string.Format("{0}|{1}|{2}", DateTime.Now.ToString("M-d-yyyy H:mm:ss"), "CHANGE DISPENSED: " + amountOfChange.ToString("C"), "REMAINING BALANCE: " + customerBalance.ToString("C")));
            sb.Append('\n');
            WriteLineToAuditFile(sb.ToString());
        }

        public void WriteLineToAuditFile(string line)
        {
            string currentDirectory = Environment.CurrentDirectory;
            string fullAuditFilePath = Path.Combine(currentDirectory, AUDIT_FILE_PATH, AUDIT_FILE_NAME);

            try
            {
                using StreamWriter sw = new(fullAuditFilePath, true);
                sw.WriteLine(line);
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