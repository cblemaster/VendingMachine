
namespace VendingMachine.Domain.Snacks;

internal sealed record Chips(decimal Price, string Label) : Snack(Price, Label);
