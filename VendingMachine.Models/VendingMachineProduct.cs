namespace VendingMachine.Models
{
    public class VendingMachineProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public const int INITIAL_STARTING_QUANTITY = 5;
        public const string SOLD_OUT_MESSAGE = "SOLD OUT";

        public virtual string ShowMessageToCustomerAfterSale() =>
            "Base class message...";

        public override string ToString() =>
            string.Format("{0,-20} {1:C}", Name, Price);

    }
}