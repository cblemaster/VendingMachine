using VendingMachine.Inventory;

namespace VendingMachine.Reporting;

internal static class AuditFileLineItem
{
    internal static string MoneyDeposited(decimal amountDeposited, decimal totalDeposited) =>
        $"{Helpers.FormatedDateTimeNow}|FEED MONEY|AMOUNT DEPOSITED: {amountDeposited:C}|TOTAL DEPOSITED: {totalDeposited:C}";
    internal static string SnackSold(Snack snack, string slotIdentifier, decimal totalDeposited) =>
        $"{Helpers.FormatedDateTimeNow}|SNACK SOLD: {snack.Label}|SLOT: {slotIdentifier}|SNACK PRICE: {snack.Price:C}|TOTAL DEPOSTED: {totalDeposited:C}";
    internal static string ChangeDispensed(decimal change, decimal totalDeposited) =>
        $"{Helpers.FormatedDateTimeNow}|CHANGE DISPENSED: {change:C}|TOTAL DEPOSITED: {totalDeposited:C}";
}
