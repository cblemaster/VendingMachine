namespace VendingMachine;

internal static class Helpers
{
    internal static string FormatedDateTimeNow => DateTime.Now.ToString("M-d-yyyy H_mm_ss");

    internal static (int Quarters, int Dimes, int Nickels) CountCoinsForChange(decimal change)
    {
        int numQuarters = CalculateCoinCount(change, Coins.Quarter);
        change -= numQuarters * ((int)Coins.Quarter / 100M);

        int numDimes = CalculateCoinCount(change, Coins.Dime);
        change -= numDimes * ((int)Coins.Dime / 100M);

        int numNickels = CalculateCoinCount(change, Coins.Nickel);
        change -= numNickels * ((int)Coins.Nickel / 100M);

        return change == 0 ? (numQuarters, numDimes, numNickels) : (-1, -1, -1);

        static int CalculateCoinCount(decimal amount, Coins coin)
        {
            return (int)(amount / ((int)coin / 100M));
        }
    }

    internal static bool ChangeDispensedReducesAmountDepositedToZero((int quarters, int dimes, int nickels) change, decimal amountDeposited)
    {
        decimal quarterVal = (int)Coins.Quarter * (decimal)change.quarters / 100;
        decimal dimeVal = (int)Coins.Dime * (decimal)change.dimes / 100;
        decimal nickelVal = (int)Coins.Nickel * (decimal)change.nickels / 100;
        decimal totalChange = quarterVal + dimeVal + nickelVal;

        return totalChange == amountDeposited;
    }
}
internal enum Coins
{
    Quarter = 25,
    Dime = 10,
    Nickel = 5,
}


