
namespace VendingMachine.Domain.Snacks;

internal sealed record Crackers(decimal Price, string Label) : Snack(Price, Label);
