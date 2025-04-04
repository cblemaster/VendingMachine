
namespace VendingMachine.Domain.Snacks;

internal sealed record Drink(decimal Price, string Label) : Snack(Price, Label);
