namespace VendingMachine.Models
{
    public class VendingMachineProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public const int INITIAL_STARTING_QUANTITY = 5;
        internal const string SOLD_OUT_MESSAGE = "SOLD OUT";

        public virtual string ShowMessageToCustomerAfterSale()
        {
            return "Base class message...";
        }

    }
}