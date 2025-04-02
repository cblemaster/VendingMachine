
namespace VendingMachine;

internal sealed class VendTron
{
    private List<Snack> _purchases;
    private List<decimal> _deposits;
    private List<decimal> _changeReturned;
    
    internal Inventory Inventory { get; }
    internal decimal Deposits { get; private set; }

    internal VendTron()
    {
        Inventory = new();
        _purchases = [];
        _deposits = [];
        _changeReturned = [];
    }

    internal void DisplaySnacks()
    {

    }
    
    internal void ProcessDeposit(decimal deposit)
    {
        _deposits.Add(deposit);
        // TODO: add deposit to reports
    }

    internal void ProcessPurchase(string identifier)
    {

    }

    internal void ProcessReturnChange()
    {

    }
}
