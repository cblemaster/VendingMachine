// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

VendingMachine.Models.VendingMachine vm = new();
VendingMachine.Models.VendingMachineOwner.UpdateVendingMachineInventory(vm);

Console.WriteLine(VendingMachine.UI.Helpers.DisplayItems(vm));
