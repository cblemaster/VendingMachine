
namespace VendingMachine.Domain.Snacks;

internal sealed record Pastry(decimal Price, string Label) : Snack(Price, Label);
