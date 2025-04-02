
using VendingMachine.Inventory;
using VendingMachine.Snacks;
using _Inventory = VendingMachine.Inventory.Inventory;

namespace VendingMachine.Machine;

internal sealed class VendTron
{
    private const decimal QUARTER = 0.25m;
    private const decimal DIME = 0.10m;
    private const decimal NICKEL = 0.05m;

    private List<(decimal Deposit, DateTimeOffset timestamp)> _deposits;
    private List<(Snack _Snack, DateTimeOffset timestamp)> _purchases;
    private List<(decimal ChangeReturned, DateTimeOffset timestamp)> _changeReturned;
    
    internal _Inventory Inventory { get; }  // TODO: Any refs from outside of this class?
    internal decimal Deposits { get; private set; }  // TODO: Any refs from outside of this class?

    internal VendTron()
    {
        Inventory = new();
        _purchases = [];
        _deposits = [];
        _changeReturned = [];
    }

    internal string DisplaySnacks() => Inventory.DisplaySnacks();

    internal void ProcessMakeDeposit(decimal deposit)
    {
        if (deposit <= 0)
        {
            throw new ArgumentException("Zero or negative deposit not allowed...");
        }
        else if (Inventory.SnackSlots.All(s => s.Snacks.Count == 0))
        {
            throw new InvalidOperationException("Cannot deposit when there are no snacks to purchase...");
        }
        else
        {
            Deposits += deposit;
            _deposits.Add((deposit, DateTimeOffset.UtcNow));
        }
    }

    internal void ProcessPurchase(string identifier)
    {
        if (Inventory.SnackSlots.SingleOrDefault(s => s.Identifier == identifier) is SnackSlot snackSlot)
        {
            if (snackSlot.Snacks.Count == 0)
            {
                throw new InvalidOperationException("Snack is sold out, and cannot be purchased...");
            }
            else if (Deposits == 0m || snackSlot.Snacks.First().Price > Deposits)
            {
                throw new ArgumentException("Insufficient deposits for snack purchase...");
            }
            else
            {
                Snack snack = snackSlot.Snacks.First();

                snackSlot.RemoveSnack(snack);
                _purchases.Add((snack, DateTimeOffset.UtcNow));
            }
        }
    }

    internal string ProcessReturnChange()
    {
        if (Deposits == 0)
        {
            throw new InvalidOperationException("There is no change due...");
        }

        decimal changeReturned = Deposits;

        int quarters = (int)(Deposits / QUARTER);
        Deposits -= quarters * QUARTER;
        int dimes = (int)(Deposits / DIME);
        Deposits -= dimes * DIME;
        int nickels = (int)(Deposits / NICKEL);
        Deposits -= nickels * NICKEL;

        if (Deposits != 0)
        {
            throw new ArithmeticException("Error calculating change...");
        }
        else
        {
            _changeReturned.Add((changeReturned, DateTimeOffset.UtcNow));
            return $"Your change is {quarters} quarter(s), {dimes} dime(s), and {nickels} nickels...";
        }
    }
}
