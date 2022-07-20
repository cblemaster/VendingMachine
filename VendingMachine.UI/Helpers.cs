using System.Text;
using VendingMachine.Models;

namespace VendingMachine.UI
{
    public class Helpers
    {
        public static string DisplayProducts(Vendomatic vm)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm), "Unknown vending machine.");
            if (vm.AreAllProductsEmpty()) throw new ArgumentOutOfRangeException(nameof(vm.Products), "No items in inventory!");  //TODO: Make error message strings into consts?

            StringBuilder sb = new();

            foreach (KeyValuePair<string, List<Product>> slot in vm.Products.OrderBy(p => p.Key))
            {
                sb.Append(string.Format("{0,-5}{1}", slot.Key.ToUpper(), slot.Value.Count > 0 ? slot.Value[0].ToString() : Product.SOLD_OUT_MESSAGE));
                sb.Append('\n');
            }

            return sb.ToString();
        }
    }
}