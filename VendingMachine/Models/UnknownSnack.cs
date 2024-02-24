namespace VendingMachine.Models;

internal sealed class UnknownSnack : Snack
{
    internal override string SendMessageWhenSold() => "Unknown snack!";
}
