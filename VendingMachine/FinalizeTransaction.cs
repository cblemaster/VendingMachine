
namespace VendingMachine;

internal sealed class FinalizeTransaction
{
    private const decimal QUARTER_VALUE = 0.25m;
    private const decimal DIME_VALUE = 0.10m;
    private const decimal NICKEL_VALUE = 0.05m;

    private decimal ChangeDue {  get; }
    private Coin Quarters { get; } = default!;
    private Coin Dimes { get; } = default!;
    private Coin Nickels { get; } = default!;

    internal FinalizeTransaction(decimal changeDue)
    {
        ChangeDue = changeDue;
        if (ChangeDue == 0) { return; }

        decimal amount = ChangeDue;

        Quarters = new Coin(amount, QUARTER_VALUE);
        amount -= Quarters.ValueSum;
        
        Dimes = new Coin(amount, DIME_VALUE);
        amount -= Dimes.ValueSum;

        Nickels = new Coin(amount, NICKEL_VALUE);
        amount += Nickels.ValueSum;

        if (amount != 0) { throw new ArithmeticException("Error calculating change."); }
    }

    internal string ReturnChange()
    {
        if (ChangeDue == 0)
        {
            return "There is no change due.";
        }
        else
        {
            string change = "Your change is ";

            string quarters;
            string dimes;
            string nickels;

            if (Quarters.Count > 0)
            {
                quarters = Quarters.Count == 1 ? Quarters.Singular : Quarters.Plural;
                change = $"{change} {quarters}";
            }
            if (Dimes.Count > 0)
            {
                dimes = Dimes.Count == 1 ? Dimes.Singular : Dimes.Plural;
                if (Quarters.Count > 0)
                {
                    change = $"{change}, ";
                }
                if (Nickels.Count == 0)
                {
                    change = $"{change} and";
                }
                change = $"{change} {dimes}";
            }
            if (Nickels.Count > 0)
            {
                nickels = Nickels.Count == 1 ? Nickels.Singular : Nickels.Plural;
                if (Dimes.Count > 0)
                {
                    change = $"{change}, and";
                }
                change = $"{change} {nickels}";
            }

            return change;
        }
    }

    internal string ToReportString() => $"Change returned: {ChangeDue:C}";
}
