namespace VendingMachine.Models
{
    public class VendingMachineProduct
    {
        internal string Name { get; set; }
        internal decimal Price { get; set; }
        internal int Quantity { get; set; }

        internal const int INITIAL_STARTING_QUANTITY = 5;
        internal const string SOLD_OUT_MESSAGE = "SOLD OUT";

        public virtual string ShowMessageToCustomerAfterSale()
        {
            return "Base class message...";
        }

    }
}