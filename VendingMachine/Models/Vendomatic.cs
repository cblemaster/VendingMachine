using System.Text;
using VendingMachine.Services;

namespace VendingMachine.Models;

internal sealed class Vendomatic
{
    #region constants
    private const string NO_MONEY_DEPOSITED = "Please deposit money before purchasing snack.";
    private const string NO_CHANGE_TO_DISPENSE = "No change to dispense.";
    private const string ALL_SNACKS_SOLD_OUT = "All snacks sold out!";
    public const string VENDING_MACHINE_MANUFACTURER = "Umbrella Corp.";
    public const string VENDING_MACHINE_MODEL = "Vendo-Matic 600";
    public const int SNACKS_PER_SLOT = 5;
    #endregion

    internal bool IsOn { get; set; }
    internal IEnumerable<Slot> Slots { get; set; } = Enumerable.Empty<Slot>();
    internal decimal AmountDeposited { get; set; }
    internal ICollection<Snack> SnacksSoldToday { get; set; } = Enumerable.Empty<Snack>().ToList();
    internal StringBuilder AuditFile { get; set; } = new();
    internal decimal DailySales { get; set; }
    internal bool AreAllSnacksSoldOut => Slots.All(s => s.IsSoldOut);

    internal string DisplaySnacks()
    {
        if (AreAllSnacksSoldOut) { return ALL_SNACKS_SOLD_OUT; }

        StringBuilder sb = new("*** SNACKS ***\n\n");

        foreach (Slot slot in Slots)
        {
            sb.AppendLine(slot.ToString());
        }

        return sb.ToString();
    }
    internal string DispenseSnack(Slot slot)
    {
        if (AmountDeposited <= 0) { return NO_MONEY_DEPOSITED; }
        if (slot.IsSoldOut) { return Slot.SNACK_SOLD_OUT; }

        Snack snack = slot.Snacks.Peek();
        if (snack.Price > AmountDeposited) { return FundsInsufficientForPurchase(snack.Price, snack.Label); }

        snack = slot.Snacks.Pop();

        AmountDeposited -= snack.Price;
        DailySales += snack.Price;

        SnacksSoldToday.Add(snack);
        AuditFile.AppendLine(AuditFileLineItem.SnackSold(snack, slot.Identifier, AmountDeposited));

        return snack.SendMessageWhenSold();
    }
    internal string FinishTransaction()
    {
        if (AmountDeposited <= 0) { return NO_CHANGE_TO_DISPENSE; }

        decimal changeDispensed = AmountDeposited;

        (int Quarters, int Dimes, int Nickels) = Helpers.CountCoinsForChange(AmountDeposited);
        if (Quarters != -1 && Helpers.ChangeDispensedReducesAmountDepositedToZero((Quarters, Dimes, Nickels), AmountDeposited))
        {
            AmountDeposited = 0M;
            AuditFile.AppendLine(AuditFileLineItem.ChangeDispensed(changeDispensed, AmountDeposited));
        }

        return FormatChangeOutput(Quarters, Dimes, Nickels);

        static string FormatChangeOutput(int numQuarters, int numDimes, int numNickels)
        {
            StringBuilder sb = new();
            sb.Append("Your change is: ");
            if (numQuarters > 0)
            {
                if (numQuarters == 1) { sb.Append(string.Format("{0} quarter", numQuarters)); }
                if (numQuarters > 1) { sb.Append(string.Format("{0} quarters", numQuarters)); }
                if (numDimes > 0 || numNickels > 0) { sb.Append(", "); }
            }
            if (numDimes > 0)
            {
                if (numDimes == 1) { sb.Append(string.Format("{0} dime", numDimes)); }
                if (numDimes > 1) { sb.Append(string.Format("{0} dimes", numDimes)); }
                if (numNickels > 0) { sb.Append(", "); }
            }
            if (numNickels > 0)
            {
                if (numNickels == 1) { sb.Append(string.Format("{0} nickel", numNickels)); }
                if (numNickels > 1) { sb.Append(string.Format("{0} nickels", numNickels)); }
            }

            return sb.ToString();
        }
    }

    internal string FundsInsufficientForPurchase(decimal price, string label) => $"You have not deposited enough money to purchase {label} \n The price for {label} is {price:C} and you have deposited {AmountDeposited:C}.";
    internal string DisplayAmountDeposited() => $"Amount deposited: {AmountDeposited:C}";

}
