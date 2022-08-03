// See https://aka.ms/new-console-template for more information

using VendingMachine.Models;
using VendingMachine.UI;

const string MAIN_MENU_OPTION_DISPLAY_ITEMS = "Display Vending Machine Items";
const string MAIN_MENU_OPTION_PURCHASE = "Purchase";
const string MAIN_MENU_OPTION_EXIT = "Exit";
const string MAIN_MENU_OPTION_SALES_REPORT = "Sales Report";
string[] MAIN_MENU_OPTIONS = { MAIN_MENU_OPTION_DISPLAY_ITEMS, MAIN_MENU_OPTION_PURCHASE, MAIN_MENU_OPTION_EXIT, MAIN_MENU_OPTION_SALES_REPORT }; //const has to be known at compile time, the array initializer is not const in C#

const string PURCHASE_MENU_OPTION_FEED_MONEY = "Feed Money";
const string PURCHASE_MENU_OPTION_SELECT_PRODUCT = "Select product";
const string PURCHASE_MENU_OPTION_FINISH_TRANSACTION = "Finish transaction";
string[] PURCHASE_MENU_OPTIONS = { PURCHASE_MENU_OPTION_FEED_MONEY, PURCHASE_MENU_OPTION_SELECT_PRODUCT, PURCHASE_MENU_OPTION_FINISH_TRANSACTION };

IBasicUserInterface ui = new MenuDrivenCLI();

ui.Output("Welcome to " + Vendomatic.VENDING_MACHINE_MODEL + "!");
ui.Output("Brought to you by " + Vendomatic.VENDING_MACHINE_MANUFACTURER);

Vendomatic vm = new();
Owner.TurnOnVendingMachine(vm);  // also calls UpdateVendingMachineInventory()

AuditFile af = new();

while (vm.IsOn)
{
    string mainMenuSelection = (string)ui.PromptForSelection(MAIN_MENU_OPTIONS);  // main menu prompt for selection
    if (mainMenuSelection == MAIN_MENU_OPTION_DISPLAY_ITEMS)
    {
        ui.Output(OutputHelpers.DisplayProducts(vm));
    }
    if (mainMenuSelection == MAIN_MENU_OPTION_PURCHASE)
    {
        bool isCustomerPurchasing = true;
        while (isCustomerPurchasing)
        {
            string purchaseMenuSelection = (string)ui.PromptForSelection(PURCHASE_MENU_OPTIONS);
            if (purchaseMenuSelection == PURCHASE_MENU_OPTION_FEED_MONEY)
            {
                int amountDeposited = 0;
                while (amountDeposited <= 0)
                {
                    ui.Output("Please enter amount to deposit.");
                    int.TryParse(Console.ReadLine(), out amountDeposited);
                }
                Customer.DepositMoney(vm, amountDeposited);
                ui.Output(OutputHelpers.DisplayCustomerBalance(vm));
                af.WriteDepositToAuditLog(amountDeposited, vm.CustomerBalance);
            }
            if (purchaseMenuSelection == PURCHASE_MENU_OPTION_SELECT_PRODUCT)
            {
                string slotSelection = string.Empty;
                while (slotSelection == string.Empty || !vm.Products.ContainsKey(slotSelection))
                {
                    ui.Output("Enter slot for the product you wish to purchase");
                }
                Customer.PurchaseProduct(vm, slotSelection);
                af.WriteProductSoldToAuditLog(vm.Products[slotSelection][0], vm.CustomerBalance, slotSelection);
                OutputHelpers.DisplayCustomerBalance(vm);
            }
            if (purchaseMenuSelection == PURCHASE_MENU_OPTION_FINISH_TRANSACTION)
            {
                decimal change = vm.CustomerBalance;
                ui.Output(Customer.FinishTransaction(vm));
                af.WriteChangeDispensedToAuditLog(change, vm.CustomerBalance);
                isCustomerPurchasing = false;
            }
        }
    }
    if (mainMenuSelection == MAIN_MENU_OPTION_EXIT)
    {
        //break;
        Owner.TurnOffVendingMachine(vm);
    }
    if (mainMenuSelection == MAIN_MENU_OPTION_SALES_REPORT)
    {
        af.WriteAuditLogToFile();  //TO DO: Audit file functionality needs re-written
        
        SalesReport sr = new();
        sr.CreateSalesLogFromVendingMachineDailySales(vm);
        sr.WriteSalesReportToFile();
    }
    
}

#region displayproductstests
// ** Display products tests
//Vendomatic vm = new();
//Owner.UpdateVendingMachineInventory(vm);

//Console.WriteLine(VendingMachine.UI.OutputHelpers.DisplayProducts(vm));
#endregion
#region auditfiletests
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
#endregion
#region salesreporttests
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
#endregion
