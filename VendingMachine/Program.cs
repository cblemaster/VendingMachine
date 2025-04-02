
using VendingMachine.Machine;
VendTron vt = new();
vt.ProcessMakeDeposit(12.45m);
Console.WriteLine(vt.DisplaySnacks());
