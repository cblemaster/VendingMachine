// See https://aka.ms/new-console-template for more information

using VendingMachine.Models;

Console.WriteLine("Welcome to " + Vendomatic.VENDING_MACHINE_MODEL + "!");
Console.WriteLine("Brought to you by " + Vendomatic.VENDING_MACHINE_MANUFACTURER);


// ** Display products tests
//Vendomatic vm = new();
//Owner.UpdateVendingMachineInventory(vm);

//Console.WriteLine(VendingMachine.UI.OutputHelpers.DisplayProducts(vm));

// ** Audit file tests
//Vendomatic vm = new();
//Owner.UpdateVendingMachineInventory(vm);

//Customer.DepositMoney(vm, 10);
//AuditFile af = new();
//af.WriteDepositToAuditLog(10, vm.CustomerBalance);

//Customer.PurchaseProduct(vm, "B2");
//Product purchasedProduct = vm.ProductsSoldToday["B2"].FirstOrDefault();
//af.WriteProductSoldToAuditLog(purchasedProduct, vm.CustomerBalance, "B2");

//Customer.FinishTransaction(vm);
//af.WriteChangeDispensedToAuditLog(8.50M, vm.CustomerBalance);

//af.WriteAuditLogToFile();

// ** Sales report tests
//Vendomatic vm = new();
//Owner.UpdateVendingMachineInventory(vm);

//Customer.DepositMoney(vm, 50);
//SalesReport sr = new();

//Customer.PurchaseProduct(vm, "B2");
//Customer.PurchaseProduct(vm, "B2");
//Customer.PurchaseProduct(vm, "A2");
//Customer.PurchaseProduct(vm, "C1");
//Customer.PurchaseProduct(vm, "D4");

//sr.CreateSalesLogFromVendingMachineDailySales(vm);
//sr.WriteSalesReportToFile();
//decimal dailySales = vm.DailySales;   // should be $6.45



