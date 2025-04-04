
using VendingMachine.Machine;
VendTron vt = new();

try
{
    vt.ProcessMakeDepositOrThrow(12.45m);
}
catch (Exception)
{
    throw; // TODO: Error message
}

Console.WriteLine(vt.DisplaySnacks());
