using System.Text;

namespace VendingMachine.UI
{
    public class Helpers
    {
        //TODO: Straighten out namespace collisions that require fully qualified names here
        public static string DisplayItems(VendingMachine.Models.VendingMachine vm)
        {
            if (vm == null) return "Invalid vending machine?";
            if (!vm.Products.Any()) return "No items in inventory!";

            StringBuilder sb = new();

            foreach (KeyValuePair<string, VendingMachine.Models.VendingMachineProduct> product in vm.Products.OrderBy(p => p.Key))
            {
                sb.Append(string.Format("{0,-5}{1}", product.Key.ToUpper(), product.Value.Quantity > 0 ? product.Value.ToString() : VendingMachine.Models.VendingMachineProduct.SOLD_OUT_MESSAGE));
                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}