
using VendingMachine.Machine;
VendTron vt = new();
vt.ProcessDeposit(12.45m);
Console.WriteLine(vt.DisplaySnacks());
