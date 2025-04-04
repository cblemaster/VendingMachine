
using VendingMachine.Machine;
VendTron vt = new();
vt.ProcessMakeDepositOrThrow(12.45m); // TODO: try..catch
Console.WriteLine(vt.DisplaySnacks());
