using VendingMachine.Machine;
using VendingMachine.Reporting;

namespace VendingMachine.Customer;

internal static class Customer
{
    internal static void DepositMoney(Vendomatic vm, decimal amount)
    {
        if (amount <= 0) { return; }
        vm.AmountDeposited += amount;
        vm.AuditFile.AppendLine(AuditFileLineItem.MoneyDeposited(amount, vm.AmountDeposited));
    }

    internal static string SelectSnack(Vendomatic vm, Slot slot) => vm.DispenseSnack(slot);
}
