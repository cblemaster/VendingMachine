using VendingMachine.Services;

namespace VendingMachine.Models;

internal static class Owner
{
    internal static void TurnVendingMachineOn(Vendomatic vm) => vm.IsOn = true;
    internal static void TurnVendingMachineOff(Vendomatic vm) => vm.IsOn = false;
    internal static IEnumerable<Slot> UpdateVendingMachineInventory(Vendomatic vm) => ProcessInventory.CreateVendingMachineInventory(vm);
}
