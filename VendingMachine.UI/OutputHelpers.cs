using System.Text;
using VendingMachine.Models;

namespace VendingMachine.UI
{
    public class OutputHelpers
    {
        private const string NO_PRODUCTS_IN_INVENTORY_ERROR = "No products in inventory!";
        
        public static string DisplayProducts(Vendomatic vm)
        {
            try
            {
                if (vm.AreAllProductsEmpty()) throw new ArgumentOutOfRangeException(nameof(vm.Products), NO_PRODUCTS_IN_INVENTORY_ERROR);

                StringBuilder sb = new();

                foreach (KeyValuePair<string, List<Product>> slot in vm.Products.OrderBy(p => p.Key))
                {
                    sb.Append(string.Format("{0,-5}{1}", slot.Key.ToUpper(), slot.Value.Count > 0 ? slot.Value[0].ToString() : Product.SOLD_OUT_MESSAGE));
                    sb.Append('\n');
                }

                return sb.ToString();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw ex;
            }            
        }

        public static string DisplayCustomerBalance(Vendomatic vm) => string.Format("Current balance: {0:C}", vm.CustomerBalance);
    }
}