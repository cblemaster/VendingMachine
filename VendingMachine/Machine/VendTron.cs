
using VendingMachine.Inventory;
using VendingMachine.Snacks;
using VendingMachine.Transactions;
using _Inventory = VendingMachine.Inventory.Inventory;

namespace VendingMachine.Machine;

internal sealed class VendTron
{
    private const decimal NICKEL = 0.05m;
    
    private readonly List<Transaction> _transactions;

    private _Inventory Inventory { get; }  // TODO: Any refs from outside of this class?
    private decimal Deposit { get; set; }  // TODO: Any refs from outside of this class?

    internal VendTron()
    {
        Inventory = new();
        _transactions = [];
    }

    internal string DisplaySnacks() => Inventory.DisplaySnacks();

    internal void ProcessMakeDepositOrThrow(decimal deposit)
    {
        if (deposit <= 0)
        {
            throw new ArgumentException("Zero or negative deposit not allowed...");
        }
        else if (deposit %NICKEL != 0)
        {
            throw new ArgumentException($"Only deposits in {NICKEL:C} increments are allowed...");
        }
        else if (Inventory.SnackSlots.All(s => s.Snacks.Count == 0))
        {
            throw new InvalidOperationException("Cannot deposit when there are no snacks to purchase...");
        }
        else
        {
            Deposit += deposit;
            Transaction depositTran = new DepositMade(deposit);
            _transactions.Add(depositTran);
        }
    }

    internal void ProcessPurchaseSnackOrThrow(string identifier)
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
                Transaction purchase = new SnackPurchased(snack, identifier);
                _transactions.Add(purchase);
            }
        }
    }

    internal void ProcessReturnChangeOrThrow()
    {
        if (Deposit <= 0)
        {
            throw new InvalidOperationException("There is no change due...");
        }

        try
        {
            Transaction change = new ChangeReturned(Deposit);
            _transactions.Add(change);
            Deposit = 0m;
        }
        catch (Exception)
        {
            throw; // TODO: Error message
        }
        
    }
}
