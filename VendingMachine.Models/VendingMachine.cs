namespace VendingMachine.Models
{
    public class VendingMachine
    {
        internal const string VENDING_MACHINE_MANUFACTURER = "Umbrella Corp";
        internal const string VENDING_MACHINE_MODEL = "Vendo-Matic 600";
        
        internal VendingMachineCustomer Customer { get; set; }
        public VendingMachineOwner Owner { get; set; }
        
        public Dictionary<string, VendingMachineProduct> Products = new();
        internal decimal CustomerBalance { get; set; }
        internal decimal DailySales { get; set; }
        internal bool IsOn { get; set; }

        public static void DisplayProductsAvailableForPurchase()
        {

        }

        internal static void DisplayCustomerBalance()
        {

        }


    }
}