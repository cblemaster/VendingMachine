namespace VendingMachine.Models
{
    public class VendingMachineOwner
    {
        public static void TurnOnVendingMachine(VendingMachine vendingMachine)
        {
            vendingMachine.IsOn = true;
            UpdateVendingMachineInventory();
        }

        internal static void TurnOffVendingMachine(VendingMachine vendingMachine)
        {
            vendingMachine.IsOn = false;
        }

        public static bool UpdateVendingMachineInventory()
        {
            bool success = false;


            return success;
        }
    }
}