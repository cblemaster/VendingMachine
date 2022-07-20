// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using VendingMachine.Models;

Vendomatic vm = new();
Owner.UpdateVendingMachineInventory(vm);

Console.WriteLine(VendingMachine.UI.Helpers.DisplayProducts(vm));
