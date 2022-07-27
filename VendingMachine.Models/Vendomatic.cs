namespace VendingMachine.Models
{
    public class Vendomatic
    {
        public Vendomatic()
        { 
        }

        public Vendomatic(Owner owner, Customer customer)
        {
            Owner = owner;
            Customer = customer;
        }
        
        public const string VENDING_MACHINE_MANUFACTURER = "Umbrella Corp";
        public const string VENDING_MACHINE_MODEL = "Vendo-Matic 600";

        private Customer? Customer { get;  set; }
        public decimal CustomerBalance { get; set; }
        private Owner? Owner { get; set; }
        public Dictionary<string, List<Product>> Products { get; set; } = new();        
        public decimal DailySales { get; set; }
        public Dictionary<string, List<Product>> ProductsSoldToday { get; set; } = new();
        public bool IsOn { get; set; }

        public bool AreAllProductsEmpty()
        {
            if (! Products.Any()) return true;
            
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