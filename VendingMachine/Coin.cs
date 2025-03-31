
namespace VendingMachine;

internal sealed class Coin
{
    internal decimal Value { get; }
    internal int Count { get; }
    internal static string Singular => "quarter";
    internal static string Plural => $"{Singular}s";
    internal decimal ValueSum => Value * Count;

    internal Coin(decimal amount, decimal coinValue)
    {
        Value = coinValue;
        Count = (int)(amount / coinValue);
    }
}
