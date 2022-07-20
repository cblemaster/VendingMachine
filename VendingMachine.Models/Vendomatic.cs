namespace VendingMachine.Models
{
    public class Vendomatic
    {
        private const string VENDING_MACHINE_MANUFACTURER = "Umbrella Corp";
        private const string VENDING_MACHINE_MODEL = "Vendo-Matic 600";

        private Customer Customer { get; set; }
        public decimal CustomerBalance { get; set; }
        private Owner Owner { get; set; }
        public Dictionary<string, List<Product>> Products { get; set; } = new();        
        public decimal DailySales { get; set; }
        public Dictionary<string, List<Product>> ProductsSoldToday { get; set; } = new();
        public bool IsOn { get; set; }

        public string DisplayCustomerBalance => string.Format("Current balance: {0:C}", CustomerBalance);
        
        public bool AreAllProductsEmpty()
        {
            foreach (KeyValuePair<string, List<Product>> slot in Products)
            {
                if (slot.Value.Any())
                {
                    return false;
                }
            }
            return true;
        }

    }
}