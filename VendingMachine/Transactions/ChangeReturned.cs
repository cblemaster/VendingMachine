
namespace VendingMachine.Transactions;

internal sealed class ChangeReturned : Transaction
{
    private const decimal QUARTER = 0.25m;
    private const decimal DIME = 0.10m;
    private const decimal NICKEL = 0.05m;

    private int _countOfQuarters;
    private int _countOfDimes;
    private int _countOfNickels;

    internal decimal ChangeAmount { get; }

    internal ChangeReturned(decimal changeAmount) : base(DateTimeOffset.UtcNow)
    {
        ChangeAmount = changeAmount;
        CalculateChangeOrThrow(); // TODO: try..catch
    }

    private void CalculateChangeOrThrow()
    {
        // TODO: is there change to return?

        decimal changeToReturn = ChangeAmount;

        _countOfNickels = (int)(changeToReturn / NICKEL);
        _countOfQuarters = _countOfNickels / (int)(QUARTER / NICKEL);
        _countOfNickels -= _countOfQuarters * (int)(QUARTER / NICKEL);
        _countOfDimes = _countOfNickels / (int)(DIME / NICKEL);
        _countOfNickels -= _countOfDimes * (int)(DIME / NICKEL);

        // TODO...
        //if (changeToReturn != 0)
        //{
        //    throw new ArithmeticException("Error calculating change...");
        //}
    }

    internal override string ToDisplayString => $"Your change is: {_countOfQuarters} quarter(s), {_countOfDimes} dime(s), and {_countOfNickels} nickel(s)...";
    internal override string ToReportString => $"{Timestamp:O} Change returned: {ChangeAmount:C}";
}
