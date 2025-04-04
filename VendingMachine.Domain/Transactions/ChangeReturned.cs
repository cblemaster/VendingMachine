namespace VendingMachine.Domain.Transactions;

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
        try
        {
            ChangeAmount = changeAmount;
            CalculateChangeOrThrow();
        }
        catch (Exception) { throw; }
    }

    private void CalculateChangeOrThrow()
    {
        _countOfNickels = (int)(ChangeAmount / NICKEL);
        _countOfQuarters = _countOfNickels / (int)(QUARTER / NICKEL);
        _countOfNickels -= _countOfQuarters * (int)(QUARTER / NICKEL);
        _countOfDimes = _countOfNickels / (int)(DIME / NICKEL);
        _countOfNickels -= _countOfDimes * (int)(DIME / NICKEL);

        if (!ChangeIsValid())
        {
            throw new ArithmeticException("Error calculating change...");
        }

        bool ChangeIsValid() => (_countOfQuarters * QUARTER) + (_countOfDimes * DIME) + (_countOfNickels * NICKEL) == ChangeAmount;
    }

    internal override string ToDisplayString => $"Your change is: {_countOfQuarters} quarter(s), {_countOfDimes} dime(s), and {_countOfNickels} nickel(s)...";
    internal override string ToReportString => $"{Timestamp:O} Change returned: {ChangeAmount:C}";
}
