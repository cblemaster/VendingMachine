
using System.Transactions;

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
        CalculateChange();
    }

    private void CalculateChange()
    {
        decimal changeToReturn = ChangeAmount;

        _countOfQuarters = (int)(ChangeAmount / QUARTER);
        changeToReturn -= _countOfQuarters * QUARTER;
        _countOfDimes = (int)(ChangeAmount / DIME);
        changeToReturn -= _countOfDimes * DIME;
        _countOfNickels = (int)(ChangeAmount);

        if (changeToReturn != 0)
        {
            throw new ArithmeticException("Error calculating change...");
        }
    }

    internal override string ToDisplayString => $"Your change is: {_countOfQuarters} quarter(s), {_countOfDimes} dime(s), and {_countOfNickels} nickel(s)...";
    internal override string ToReportString => $"{Timestamp:O} Change returned: {ChangeAmount:C}";
}
