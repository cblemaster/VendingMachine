using System.Text;
using VendingMachine.Customer;
using VendingMachine.Machine;
using VendingMachine.Owner;
using VendingMachine.Reporting;
using VendingMachine.UI;

#region constants
const string MAIN_MENU_OPTION_DISPLAY_SNACKS = "Display Vending Machine Items";
const string MAIN_MENU_OPTION_PURCHASE_SNACKS = "Purchase Snacks";
const string MAIN_MENU_OPTION_EXIT = "Exit";
const string MAIN_MENU_OPTION_SALES_REPORT = "Sales Report";
string[] MAIN_MENU_OPTIONS = [MAIN_MENU_OPTION_DISPLAY_SNACKS, MAIN_MENU_OPTION_PURCHASE_SNACKS, MAIN_MENU_OPTION_EXIT, MAIN_MENU_OPTION_SALES_REPORT]; //const has to be known at compile time, the array initializer is not const in C#

const string PURCHASE_MENU_OPTION_FEED_MONEY = "Feed Money";
const string PURCHASE_MENU_OPTION_SELECT_SNACK = "Select snack";
const string PURCHASE_MENU_OPTION_FINISH_TRANSACTION = "Finish transaction";
string[] PURCHASE_MENU_OPTIONS = [PURCHASE_MENU_OPTION_FEED_MONEY, PURCHASE_MENU_OPTION_SELECT_SNACK, PURCHASE_MENU_OPTION_FINISH_TRANSACTION];
#endregion

MenuDrivenCLI ui = new();

ui.Output($"Welcome to {Vendomatic.VENDING_MACHINE_MODEL} !");
ui.Output($"Brought to you by {Vendomatic.VENDING_MACHINE_MANUFACTURER}");

Vendomatic vm = new();

Owner.TurnVendingMachineOn(vm);
IEnumerable<Slot> inventory = Owner.UpdateVendingMachineInventory(vm);
vm.Slots = inventory;

while (vm.IsOn)
{
    string mainMenuSelection = (string)ui.PromptForSelection(MAIN_MENU_OPTIONS);
    if (mainMenuSelection == MAIN_MENU_OPTION_DISPLAY_SNACKS)
    {
        ui.Output(vm.DisplaySnacks().ToString());
    }
    if (mainMenuSelection == MAIN_MENU_OPTION_PURCHASE_SNACKS)
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
                    ui.Output("Please enter amount to deposit, whole dollars only.");
                    _ = int.TryParse(Console.ReadLine(), out amountDeposited);
                }
                try
                {
                    Customer.DepositMoney(vm, amountDeposited);
                    ui.Output(vm.DisplayAmountDeposited());
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    ui.Output(ex.Message);
                }
            }
            if (purchaseMenuSelection == PURCHASE_MENU_OPTION_SELECT_SNACK)
            {
                string slotSelection = string.Empty;
                while (slotSelection == string.Empty)
                {
                    ui.Output("Enter slot for the product you wish to purchase");
                    slotSelection = Console.ReadLine() ?? string.Empty;
                }
                try
                {
                    ui.Output(Customer.SelectSnack(vm, vm.Slots.Single(s => s.Identifier.Equals(slotSelection, StringComparison.CurrentCultureIgnoreCase))));
                    ui.Output(vm.DisplayAmountDeposited());
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    ui.Output(ex.Message);
                }
            }
            if (purchaseMenuSelection == PURCHASE_MENU_OPTION_FINISH_TRANSACTION)
            {
                ui.Output(vm.DisplayAmountDeposited());
                try
                {
                    ui.Output(vm.FinishTransaction());
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    ui.Output(ex.Message);
                }
                isCustomerPurchasing = false;
            }
        }
    }
    if (mainMenuSelection == MAIN_MENU_OPTION_EXIT)
    {
        Owner.TurnVendingMachineOff(vm);
        WriteReport.WriteAuditFile(vm.AuditFile);
    }
    if (mainMenuSelection == MAIN_MENU_OPTION_SALES_REPORT)
    {
        try
        {
            StringBuilder salesReport = WriteReport.CreateSalesReport(vm);
            WriteReport.WriteSalesReport(salesReport);
        }
        catch (Exception ex)
        {
            ui.Output(ex.Message);
        }
    }
}