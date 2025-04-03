
using VendingMachine.Inventory;
using VendingMachine.Snacks;
using _Inventory = VendingMachine.Inventory.Inventory;

namespace VendingMachine.Machine;

internal sealed class VendTron
{
    private const decimal QUARTER = 0.25m;
    private const decimal DIME = 0.10m;
    private const decimal NICKEL = 0.05m;

    private readonly List<Transaction> _transactions;

    internal _Inventory Inventory { get; }  // TODO: Any refs from outside of this class?
    internal decimal Deposit { get; private set; }  // TODO: Any refs from outside of this class?

    internal VendTron()
    {
        Inventory = new();
        _transactions = [];
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
            Deposit += deposit;
            
            DateTimeOffset now = DateTimeOffset.UtcNow;
            _transactions.Add(new Transaction(TransactionType.Deposit, deposit, $"Time: {now:O}, Deposit: {deposit:C}", now));
        }
    }

    internal void ProcessPurchaseSnack(string identifier)
    {
        if (Inventory.SnackSlots.SingleOrDefault(s => s.Identifier == identifier) is SnackSlot snackSlot)
        {
            if (snackSlot.Snacks.Count == 0)
            {
                throw new InvalidOperationException("Snack is sold out, and cannot be purchased...");
            }
            else if (Deposit == 0m || snackSlot.Snacks.First().Price > Deposit)
            {
                throw new ArgumentException("Insufficient deposits for snack purchase...");
            }
            else
            {
                Snack snack = snackSlot.Snacks.First();

                snackSlot.RemoveSnack(snack);
                DateTimeOffset now = DateTimeOffset.UtcNow;
                _transactions.Add(new Transaction(TransactionType.Purchase, snack.Price, $"Time: {now:O}, Snack sold: {snackSlot.Identifier} {snack.ToDisplayString}", now));
            }
        }
    }

    internal string ProcessReturnChange()
    {
        if (Deposit == 0)
        {
            throw new InvalidOperationException("There is no change due...");
        }

        decimal changeReturned = Deposit;

        int quarters = (int)(Deposit / QUARTER);
        Deposit -= quarters * QUARTER;
        int dimes = (int)(Deposit / DIME);
        Deposit -= dimes * DIME;
        int nickels = (int)(Deposit / NICKEL);
        Deposit -= nickels * NICKEL;

        if (Deposit != 0)
        {
            throw new ArithmeticException("Error calculating change...");
        }
        else
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            _transactions.Add(new Transaction(TransactionType.Change, changeReturned, $"Time: {now:O}, Return change: {changeReturned:C}", now));
            return $"Your change is {quarters} quarter(s), {dimes} dime(s), and {nickels} nickels...";
        }
    }
}
